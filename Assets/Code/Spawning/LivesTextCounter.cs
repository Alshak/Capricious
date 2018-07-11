using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Spawning
{
    public class LivesTextCounter : MonoBehaviour
    {
        private PlayerLives lives;
        private TextMesh[] textMeshes;

        void Start()
        {
            lives = GameObject.FindObjectOfType<PlayerLives>();
            textMeshes = GetComponentsInChildren<TextMesh>();
            foreach (TextMesh textMesh in textMeshes)
            {
                textMesh.text = lives.GetLives().ToString();
            }

            UpdateLives();
        }

        public void UpdateLives()
        {
            string text = lives.GetLives() > 0 ? "Employees Left: " + lives.GetLives().ToString() : "NO MORE EMPLOYEES";
            foreach (TextMesh textMesh in textMeshes)
            {
                textMesh.text = text;
            }
        }

        public void Hide()
        {
            foreach (TextMesh textMesh in textMeshes)
            {
                textMesh.text = "";
            }
        }
    }
}
