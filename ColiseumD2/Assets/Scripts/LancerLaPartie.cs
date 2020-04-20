using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LancerLaPartie : MonoBehaviour
{
    public void Lancer_La_Partie()
    {
        PhotonNetwork.LoadLevel("Jeu justoin");
    }
}
