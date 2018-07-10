using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code.LevelChange
{
    public class ChangeLevel : MonoBehaviour
    {
        private Boolean IsActived = false;
        public LevelName NextLevel;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player" && IsActived == false)
            {
                IsActived = true;
                SceneManager.LoadScene(NextLevel.ToString(), LoadSceneMode.Single);
            }
        }
    }
}
