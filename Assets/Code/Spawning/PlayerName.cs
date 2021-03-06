﻿using Assets.Code.Humanoids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Spawning
{
    public class PlayerName : MonoBehaviour
    {
        private TextMesh[] textMeshes;
        private SteveNames SteveNames;
        private bool IsActivated = false;

        void Start()
        {
            textMeshes = GetComponentsInChildren<TextMesh>();
            SteveNames = GameObject.FindObjectOfType<SteveNames>();
            string name = SteveNames.GetCurrentName();

            foreach (TextMesh textMesh in textMeshes)
            {
                textMesh.text = name;
            }
        }

        void Update()
        {
            if (IsActivated == false)
            {
                IsActivated = true;
                string name = SteveNames.GetCurrentName();

                foreach (TextMesh textMesh in textMeshes)
                {
                    if (textMesh != null)
                        textMesh.text = name;
                }
            }
        }

        public void SetNextSteveName()
        {
            string name = SteveNames.GetNextName();
            foreach (TextMesh textMesh in textMeshes)
            {
                textMesh.text = name;
            }
        }

        public void Hide()
        {
            foreach (TextMesh textMesh in textMeshes)
            {
                textMesh.text = "";
            }
        }

        public void Fadeout(float alpha)
        {
            foreach (TextMesh textMesh in textMeshes)
            {
                textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, alpha);
            }
        }
    }
}
