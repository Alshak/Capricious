using Assets.Code.Spawning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.LevelChange
{
    public class PlayerLivesReseter : MonoBehaviour
    {
        void Start()
        {
            Debug.Log("Reseting lives");
            PlayerLives lives = GameObject.FindObjectOfType<PlayerLives>();
            if (lives != null)
            {
                lives.ResetAllLives();
            }
            else
            {
                Debug.Log("PLAYER LIVES COMPONENT NOT FOUND AAH EVERYBODY GO CRAZY!");
            }

            PlayerNameTextMover textMover = GameObject.FindObjectOfType<PlayerNameTextMover>();
            if (textMover != null)
            {
                textMover.HideAll();
            }
        }
    }
}
