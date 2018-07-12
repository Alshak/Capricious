using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Humanoids
{
    public class CharacterSounds : MonoBehaviour
    {
        public List<AudioClip> ListDeath;
        public List<AudioClip> ListJump;
        public List<AudioClip> ListSlide;
        public Sound2DLocation SoundPrefab;

        public void PlayJump()
        {
            int i = Random.Range(0, ListJump.Count - 1);
            PlaySound(ListJump[i]);
        }

        public void PlaySlide()
        {
            int i = Random.Range(0, ListSlide.Count - 1);
            PlaySound(ListSlide[i]);
        }

        public void PlayDeath()
        {
            int i = Random.Range(0, ListDeath.Count - 1);
            PlaySound(ListDeath[i]);
        }

        private void PlaySound(AudioClip clip)
        {
            var soundPlayer = Instantiate(SoundPrefab);
            soundPlayer.PlaySound(clip);
        }
    }
}
