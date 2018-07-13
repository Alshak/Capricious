using Assets.Code.LevelChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.Credits
{
    public class CreditsDoor : MonoBehaviour
    {
        private bool isActivated = false;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Enemy" && isActivated == false)
            {
                isActivated = true;
                SceneManager.LoadScene(LevelName.MainMenu.ToString(), LoadSceneMode.Single);
            }
        }
    }
}
