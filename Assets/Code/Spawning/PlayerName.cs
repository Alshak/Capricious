using Assets.Code.Humanoids;
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
    }
}
