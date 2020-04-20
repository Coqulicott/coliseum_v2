using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{

    public GameObject SansPantalon;
    public GameObject AvecPantalon;
    public GameObject claymore;
    public GameObject double_lames;
    public GameObject lance;
    public GameObject marteau;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject weapon = marteau;
        if (LobbyManager.selectedWeapon == "0")
            weapon = marteau;
        else if (LobbyManager.selectedWeapon == "1")
            weapon = claymore;
        else if (LobbyManager.selectedWeapon == "2")
            weapon = lance;
        else if (LobbyManager.selectedWeapon == "3")
            weapon = double_lames;
        Vector3 pos = new Vector3(10, 2, 10);
        PhotonNetwork.Instantiate(weapon.name, pos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("menu"); //à modifier dans le prochain build
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Player {0} entered room", newPlayer.NickName);
    }
    
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("Player {0} left room", otherPlayer.NickName);
    }

}
