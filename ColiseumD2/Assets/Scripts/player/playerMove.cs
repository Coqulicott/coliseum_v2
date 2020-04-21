using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Networking;

namespace Coliseum
{
    public class playerMove : MonoBehaviour
    {
        private PhotonView photonView;
        
        // Jump
        private float verticalVelocity;
        private float jumpForce = 5.0f;
        private float gravity = 14.0f;
        public Animator anim;
        private bool jumpAnim;
        private PlayerAttack knock;

        // Move
        public float speed = 5.0f;
        public Camera cam;
 
        private CharacterController player;

        // Start is called before the first frame update
        void Start()
        {
            photonView = GetComponent<PhotonView>();
            //if (!photonView.IsMine) 
            //    Destroy(camera);
            if (photonView.IsMine)
            {
                ThirdPersonCamera.lookAt = this.transform;
                // Camera.main.transform.position =
                //      this.transform.position - this.transform.forward * 10 + this.transform.up * 3;
                //  Camera.main.transform.LookAt(this.transform.position);
                //  Camera.main.transform.parent = this.transform;
            }
            
            player = GetComponent<CharacterController>();
            cam = Camera.main;
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine)
            {
                jumpAnim = false;

                Vector3 forward = cam.transform.forward;
                Vector3 right = cam.transform.right;

                forward.y = 0;
                right.y = 0;

                forward *= speed;
                right *= speed;

                bool UnlockedRotation = false;

                Vector3 move = new Vector3();

                if (Input.GetKey(KeyCode.D))
                {
                    move = right;
                    UnlockedRotation = true;
                }

                if (Input.GetKey(KeyCode.Q))
                {
                    move = -right;
                    UnlockedRotation = true;
                }

                if (Input.GetKey(KeyCode.Z))
                {
                    if (Input.GetKey(KeyCode.Q))
                        forward = (forward - right) * 0.5f;
                    if (Input.GetKey(KeyCode.D))
                        forward = (forward + right) * 0.5f;
                    move = forward;
                    UnlockedRotation = true;
                }

                if (Input.GetKey(KeyCode.S))
                {
                    if (Input.GetKey(KeyCode.Q))
                        forward = (forward + right) * 0.5f;
                    if (Input.GetKey(KeyCode.D))
                        forward = (forward - right) * 0.5f;
                    move = -forward;
                    UnlockedRotation = true;
                }

                if (Input.GetKey(KeyCode.R))
                    move *= 2f;


                if (player.isGrounded)
                {
                    verticalVelocity = -gravity * Time.deltaTime;
                    if (Input.GetKey(KeyCode.Space))
                    {
                        verticalVelocity = jumpForce;
                        jumpAnim = true;
                    }
                }
                else
                {
                    verticalVelocity -= gravity * Time.deltaTime;
                }

                Vector3 jump = Vector3.zero;
                jump.y = verticalVelocity;

                if (UnlockedRotation)
                {
                    anim.SetBool("isRunning", true);
                    transform.rotation = Quaternion.LookRotation(move);
                }
                else
                {
                    anim.SetBool("isRunning", false);
                }

                move += jump;
                move += knock.knockback; 
                player.Move(move * Time.deltaTime);

                if (jumpAnim)
                    anim.SetBool("isJumping", true);
                else
                    anim.SetBool("isJumping", false);
            }
        }
    }
}