using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Coliseum
{
    public class MainMenu : MonoBehaviour
    {

        public void QuitGame() // Quitter le jeu
        {
            Debug.Log("Vous avez quitte le jeu !");
            Application.Quit();
        }

        public void PlayGame()
        {
            if (DropDownArena.arenaID ==0)
            {
                SceneManager.LoadScene("Arene 1");
            }
            
            else
            {
                SceneManager.LoadScene("Arene 2");
            }
        }

    }
}
