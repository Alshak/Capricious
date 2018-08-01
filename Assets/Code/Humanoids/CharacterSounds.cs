using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Humanoids
{
    public class CharacterSounds : MonoBehaviour
    {
        public List<AudioClip> ListDeath;
        public List<AudioClip> ListJumpVocal;
        public List<AudioClip> ListJumpSfx;
        public List<AudioClip> ListSlideVocal;
        public List<AudioClip> ListSlideSfx;
        public List<AudioClip> ListLand;
        public List<AudioClip> ListThrow;
        public List<AudioClip> ListSpawn;
        public AudioSource OneShotSoundPlayer;
        public AudioSource WallSlidePlayer;
        public AudioSource RunPlayer;
        public bool IsPlayingWallSlide;
        public bool IsPlayingRun;

        private float slideCooldown = 0f;

        public void PlayJump()
        {
            Reset();
            AdjustPitch(0.3f);
            PlaySound(GetRandom(ListJumpSfx));
            PlaySound(GetRandom(ListJumpVocal));
        }

        public void PlaySlide()
        {
            if (slideCooldown <= 0)
            {
                Reset();
                PlaySound(GetRandom(ListSlideVocal));
                PlaySound(GetRandom(ListSlideSfx));
                slideCooldown = 1f;
            }
        }

        public void PlayDeath()
        {
            Reset();
            PlaySound(GetRandom(ListDeath));
        }

        public void PlayLand()
        {
            Reset();
            AdjustPitch(0.3f);
            PlaySound(GetRandom(ListLand));
        }

        public void PlayThrow()
        {
            Reset();
            AdjustPitch(0.1f);
            PlaySound(GetRandom(ListThrow));
        }

        public void PlaySpawn()
        {
            Reset();
            PlaySound(GetRandom(ListSpawn));
        }

        public void PlayWallSlide()
        {
            WallSlidePlayer.Play();
            IsPlayingWallSlide = true;
        }

        public void StopWallSlide()
        {
            WallSlidePlayer.Stop();
            IsPlayingWallSlide = false;
        }

        public void PlayRun()
        {
            IsPlayingRun = true;
        }

        public void StopRun()
        {
            IsPlayingRun = false;
        }

        public void Update()
        {
            if (IsPlayingRun && !RunPlayer.isPlaying)
            {
                var diff = 0.3f;
                RunPlayer.pitch = 1 + (Random.value * diff - diff / 2);
                RunPlayer.Play();
            }

            if (slideCooldown > 0)
            {
                slideCooldown -= Time.deltaTime;
            }
        }

        private void Reset()
        {
            if (OneShotSoundPlayer != null)
            {
                OneShotSoundPlayer.pitch = 1;
                OneShotSoundPlayer.loop = false;
            }
        }

        private AudioClip GetRandom(List<AudioClip> sounds)
        {
            var i = Random.Range(0, sounds.Count - 1);
            return sounds[i];
        }

        private void AdjustPitch(float diff)
        {
            OneShotSoundPlayer.pitch = 1 + (Random.value * diff - diff/2);
        }

        private void PlaySound(AudioClip clip)
        {
            OneShotSoundPlayer.PlayOneShot(clip);
        }
    }
}
