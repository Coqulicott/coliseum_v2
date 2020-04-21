using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Coliseum
{

    public class PlayerHealth : MonoBehaviour
    {
        private PhotonView photonView;
        public float health;
        private bool alive;
        public bool shield;
        
        // Dmitry
        public TMPro.TMP_Text healthText;
        private float MinHealth = 0;
        private float MaxHealth = 100;
        //

        private void Start()
        {
            photonView = GetComponent<PhotonView>();
        }

        private void Update()
        {
            if (photonView.IsMine)
                shield = Input.GetKey(KeyCode.H);
            
            
            healthText.text = health.ToString();  //Afficher les hp
            if (photonView.IsMine)
            {
                if (health > MaxHealth)
                {
                    health = MaxHealth;
                }

                if (health < MinHealth)
                {
                    health = MinHealth;
                }
            }
        }

        public void TakeDamage(float damage)
        {
            alive = health > 0;
            health -= damage;
            if (health <= 0)
            {
                print("Enemy has died");
                alive = false;
                transform.Translate(0,-20,0);
            }
        
            if (alive)
                print("Enemy has taken damage");
        }
        
        
        // Dmitry
        // Les HP
        public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)     // Mon personnage
            {
                stream.SendNext(health);
            }
            else if (stream.IsReading)       // Tous les autres
            {
                health = (float)stream.ReceiveNext();
            }
        }
    }
}