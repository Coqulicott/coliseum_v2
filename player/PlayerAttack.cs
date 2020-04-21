using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Coliseum
{
    public class PlayerAttack : MonoBehaviour
    {
        private PhotonView photonView;
        
        public GameObject Hand1;
        public GameObject Hand2;
        public Weapon myWeapon;
        public Animator anim;
        public Vector3 knockback;
        private int c;

        void Start()
        {
            photonView = GetComponent<PhotonView>();
            myWeapon = Hand1.GetComponentInChildren<Weapon>();
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            knockback = Vector3.zero;
            if (photonView.IsMine)
            {
                Debug.DrawRay(Hand1.transform.position, transform.forward * myWeapon.attackRange);
                Debug.DrawRay(Hand2.transform.position, transform.forward * myWeapon.attackRange);
                if (Input.GetMouseButtonUp(0))
                {
                    DoAttack();
                    c++;
                }

                anim.SetBool("leftClick", Input.GetMouseButton(0));
            }
        }

        private void DoAttack()
        {
            float damage = myWeapon.attackDamage;

            if (c > 2)
            {
                damage *= 1.5f;
                c = 0;
            }

            Ray ray1 = new Ray(Hand1.transform.position, transform.forward);
            Ray ray2 = new Ray(Hand2.transform.position, transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray1, out hit, myWeapon.attackRange) || Physics.Raycast(ray2, out hit, myWeapon.attackRange))
            {
                if(hit.collider.CompareTag("Player"))
                {
                    PlayerHealth eHealth = hit.collider.GetComponent<PlayerHealth>();
                    CharacterController adversaire = hit.collider.GetComponent<CharacterController>();
                    knockback = Hand1.transform.forward;
                    knockback.y = 0;
                    if (!eHealth.shield)
                    {
                        eHealth.TakeDamage(damage);
                        adversaire.Move(knockback * 2);
                    }
                }
            }
        }
    }
}
