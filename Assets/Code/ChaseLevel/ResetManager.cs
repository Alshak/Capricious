using Assets.Code.LevelChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.ChaseLevel
{
    public class ResetManager :  MonoBehaviour
    {
        private bool isActivated = false;
        public void ResetLevel()
        {
            if (isActivated == false)
            {
                isActivated = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
