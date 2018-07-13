using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Credits
{
    public class CreditsCamera : MonoBehaviour
    {
        private CameraBox cameraBox;
        private EnemyController player;

        void Start()
        {
            cameraBox = GameObject.FindObjectOfType<CameraBox>();
            player = GameObject.FindObjectOfType<EnemyController>();

            if (cameraBox != null && player != null)
            {
                cameraBox.SetPlayer(player.gameObject);
            }
        }
    }
}
