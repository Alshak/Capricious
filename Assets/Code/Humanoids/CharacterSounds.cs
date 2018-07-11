using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Humanoids
{
    public class CharacterSounds : MonoBehaviour
    {
        public List<AudioClip> ListDeath;
        public Sound2DLocation SoundPrefab;
     
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
