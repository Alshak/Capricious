using Assets.Code.LevelChange;
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
        private MusicManager musicManager;
        private bool isActivated = false;

        private float cameraSet = 1f;

        void Start()
        {
            creditsBox = GameObject.FindObjectOfType<CreditsBox>();
            player = GameObject.FindObjectOfType<EnemyController>();
            musicManager = GameObject.FindObjectOfType<MusicManager>();
        }

        void Update()
        {
            if (cameraSet > 0)
            {
                cameraSet -= Time.deltaTime;
                if (creditsBox != null && player != null && isActivated == false)
                {
                    isActivated = true;
                    creditsBox.SetPlayer(player.gameObject);
                    musicManager.PlayVictoryLoop();
                    Debug.Log("happen 1");
                }
            }
        }
    }
}
