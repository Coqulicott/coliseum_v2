using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Coliseum
{
    public class playerMove : MonoBehaviour
    {
        public Camera camera;
        private PhotonView photonView;
        
        public TMPro.TMP_Text healthText;
        private float Health = 100;
        private float MinHealth = 0;
        private float MaxHealth = 100;
        
        
        public float walkSpeed;
        //public float rotateSpeed;

        public string inputForward;
        public string inputBackward;
        public string inputLeft;
        public string inputRight;
        public string inputJump;
        public string inputRun;

        public CapsuleCollider playerCollider;
        public Vector3 jumpPower;
        private float distToGround;

        // Start is called before the first frame update
        void Start()
        {
            photonView = GetComponent<PhotonView>();
            playerCollider = gameObject.GetComponent<CapsuleCollider>();
            distToGround = playerCollider.bounds.extents.y;
            if (photonView.IsMine)
            {
                ThirdPersonCamera.lookAt = this.transform;
            }
        }

        bool onGround()
        {
            return (Physics.CheckCapsule(playerCollider.bounds.center,
                end: new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y - 0.1f,
                    playerCollider.bounds.center.z), playerCollider.radius));
        }

        private bool IsGrounded()
        {
            return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
        }

        // Update is called once per frame
        void Update()
        {
            healthText.text = Health.ToString();  //Afficher les hp
            if (photonView.IsMine)
            {
                if (Health > MaxHealth)
                {
                    Health = MaxHealth;
                }

                if (Health < MinHealth)
                {
                    Health = MinHealth;
                }

                if (Input.GetKey(inputForward))
                {
                    if (Input.GetKey(inputRun))
                    {
                        Vector3 dir;
                        dir = Camera.main.transform.forward;
                        dir.y = 0;
                        transform.Translate(dir * (walkSpeed * 1.30f), Space.Self);

                    }
                    else
                    {
                        Vector3 dir;
                        dir = Camera.main.transform.forward;
                        dir.y = 0;
                        transform.Translate(dir * walkSpeed, Space.Self);

                    }
                }

                if (Input.GetKey(inputBackward))
                {
                    Vector3 dir;
                    dir = -Camera.main.transform.forward;
                    dir.y = 0;
                    transform.Translate(dir * walkSpeed, Space.Self);

                }

                if (Input.GetKey(inputRight))
                {
                    Vector3 dir;
                    dir = Camera.main.transform.right;
                    dir.y = 0;
                    transform.Translate(dir * walkSpeed, Space.Self);

                }

                if (Input.GetKey(inputLeft))
                {
                    Vector3 dir;
                    dir = -Camera.main.transform.right;
                    dir.y = 0;
                    transform.Translate(dir * walkSpeed, Space.Self);

                }

                if (IsGrounded() && Input.GetKeyDown(inputJump))
                {
                    Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;
                    v.y = jumpPower.y;

                    gameObject.GetComponent<Rigidbody>().velocity = jumpPower;

                }
            }
        }
        
        //Les HP
        public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)     //Mon personnage
            {
                stream.SendNext(Health);
            }
            else if (stream.IsReading)       //Tous les autres
            {
                Health = (float)stream.ReceiveNext();
            }
        }
        
        //Tester si les hp marchent bien avec les degats de collision
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (photonView.IsMine)
                {
                    photonView.RPC("Damage", RpcTarget.All);
                }
            }
        }

        [PunRPC]
        void Damage()
        {
            Health -= 1;
        }
    }
}