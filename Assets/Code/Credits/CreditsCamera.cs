using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Credits
{
    public class CreditsCamera : MonoBehaviour
    {
        private CreditsBox creditsBox;
        private EnemyController player;

        private float cameraSet = 1f;

        void Start()
        {
            creditsBox = GameObject.FindObjectOfType<CreditsBox>();
            player = GameObject.FindObjectOfType<EnemyController>();
        }

        void Update()
        {
            if (cameraSet > 0)
            {
                cameraSet -= Time.deltaTime;
                if (creditsBox != null && player != null)
                {
                    creditsBox.SetPlayer(player.gameObject);
                }
            }
        }
    }
}
