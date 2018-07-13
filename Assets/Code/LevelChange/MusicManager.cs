using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.LevelChange
{
    public class MusicManager : MonoBehaviour
    {
        public AudioClip MusicGameplay;
        public AudioClip MusicGameplayLoop;
        public AudioClip MusicMainMenu;
        public AudioClip MusicDeath;

        private AudioSource music;

        void Start()
        {
            music = GetComponent<AudioSource>();
        }

        public void PlayMainMenu()
        {
            music.Stop();
            music.clip = MusicMainMenu;
            music.Play();
        }

        public void PlayGameplay()
        {
            music.Stop();
            music.clip = MusicGameplay;
            music.Play();
        }

        public void PlayDeath()
        {
            music.Stop();
            music.clip = MusicDeath;
            music.Play();
        }

        public void StopAllMusic()
        {
            if (music == null)
                music = GetComponent<AudioSource>();
            music.Stop();
        }

        public void Update()
        {
            if (music.isPlaying)
                return;

            if (music.clip == MusicGameplay)
            {
                music.clip = MusicGameplayLoop;
            }

            music.Play();
        }
    }
}
