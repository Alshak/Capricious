using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.LevelChange
{
    public class MusicManager : MonoBehaviour
    {
        public AudioSource MusicGameplay;
        public AudioSource MusicDeath;

        public void PlayGameplay()
        {
            MusicGameplay.Play();
            MusicDeath.Stop();
        }

        public void PlayDeath()
        {
            MusicGameplay.Stop();
            MusicDeath.Play();
        }
    }
}
