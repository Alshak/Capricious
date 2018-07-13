using Assets.Code.LevelChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.Intro
{
    public class GameOverScreen : MonoBehaviour
    {
        private bool isActivated = false;
        void Update()
        {
            if (Input.GetButtonDown("Jump") && isActivated == false)
            {
                isActivated = true;
                SceneManager.LoadScene(LevelName.MainMenu.ToString(), LoadSceneMode.Single);
            }
        }
    }
}
