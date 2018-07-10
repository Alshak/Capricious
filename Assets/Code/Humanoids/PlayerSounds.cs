using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Humanoids
{
    public class PlayerSounds : MonoBehaviour
    {
        public List<AudioClip> ListDeath;
        public AudioSource DeathSource;

        public void PlayDeath()
        {
            if (DeathSource.isPlaying == false)
            {
                int i = Random.Range(0, ListDeath.Count - 1);
                DeathSource.clip = ListDeath[i];
                DeathSource.Play();
            }
        }
    }
}
