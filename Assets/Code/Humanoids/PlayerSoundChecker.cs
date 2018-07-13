using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityStandardAssets._2D;

namespace Assets.Code.Humanoids
{
    public class PlayerSoundChecker : MonoBehaviour
    {
        private PlatformerCharacter2D player;
        private CharacterSounds playerSounds;

        void Start()
        {
            player = GetComponentInParent<PlatformerCharacter2D>();
            playerSounds = GetComponent<CharacterSounds>();
        }

        void Update()
        {
            if (player.PlayJumpSound)
            {
                player.PlayJumpSound = false;
                playerSounds.PlayJump();
            }

            if (player.PlaySlideSound)
            {
                player.PlaySlideSound = false;
                playerSounds.PlaySlide();
            }

            if (player.PlayLandSound)
            {
                player.PlayLandSound = false;
                playerSounds.PlayLand();
            }

            if (player.PlayThrowSound)
            {
                player.PlayThrowSound = false;
                playerSounds.PlayThrow();
            }

            if (player.PlaySpawnSound)
            {
                player.PlaySpawnSound = false;
                playerSounds.PlaySpawn();
            }

            var isWallSliding = player.touchingWall && !player.m_Grounded;

            if (isWallSliding && !playerSounds.IsPlayingWallSlide)
                playerSounds.PlayWallSlide();
            else if(!isWallSliding && playerSounds.IsPlayingWallSlide)
                playerSounds.StopWallSlide();

            if (player.IsRunning && !playerSounds.IsPlayingRun)
                playerSounds.PlayRun();
            else if (!player.IsRunning && playerSounds.IsPlayingRun)
                playerSounds.StopRun();
        }
    }
}
