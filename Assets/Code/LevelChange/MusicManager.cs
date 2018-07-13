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
        public AudioClip MusicBoss;
        public AudioClip MusicBossLoop;
        public AudioClip MusicVictory;
        public AudioClip MusicVictoryLoop;

        private AudioSource music;

        void Start()
        {
            InitMusicComponent();
        }

        public void PlayMainMenu()
        {
            InitMusicComponent();
            music.Stop();
            music.clip = MusicMainMenu;
            music.Play();
        }

        public void PlayGameplay()
        {
            InitMusicComponent();
            music.Stop();
            music.clip = MusicGameplay;
            music.Play();
        }

        public void PlayDeath()
        {
            InitMusicComponent();
            music.Stop();
            music.clip = MusicDeath;
            music.Play();
        }

        public void PlayBoss1()
        {
            InitMusicComponent();
            music.Stop();
            music.clip = MusicBoss;
            music.Play();
        }

        public void PlayVictory()
        {
            InitMusicComponent();
            music.Stop();
            music.clip = MusicVictory;
            music.Play();
        }

        public void PlayVictoryLoop()
        {
            InitMusicComponent();
            music.Stop();
            music.clip = MusicVictoryLoop;
            music.Play();
        }

        public void StopAllMusic()
        {
            InitMusicComponent();
            music.Stop();
        }

        private void InitMusicComponent()
        {
            if (music == null)
                music = GetComponent<AudioSource>();
        }

        public void Update()
        {
            if (music.isPlaying)
                return;

            if (music.clip == MusicGameplay)
            {
                music.clip = MusicGameplayLoop;
            }

            if (music.clip == MusicBoss)
            {
                music.clip = MusicBossLoop;
            }

            if (music.clip == MusicVictory)
            {
                music.clip = MusicVictoryLoop;
            }

            music.Play();
        }
    }
}
