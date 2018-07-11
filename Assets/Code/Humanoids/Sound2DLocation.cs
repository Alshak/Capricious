using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Humanoids
{
    public class Sound2DLocation : MonoBehaviour
    {
        private AudioSource audioSource;
        private bool isActivated = false;

        void Update()
        {
            if (isActivated && audioSource.isPlaying == false)
            {
                Destroy(gameObject);
            }
        }

        public void PlaySound(AudioClip clip)
        {
            isActivated = true;
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
