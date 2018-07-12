using Assets.Code.LevelChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Code.Intro
{
    public class Pageflipper : MonoBehaviour
    {
        private AudioSource pageSound;
        private int imageIndex = 0;
        private bool reachedEnd = false;
        public List<Sprite> ListImages;
        private Image rend;

        void Start()
        {
            pageSound = GetComponent<AudioSource>();
            rend = GetComponent<Image>();
        }
        

        void Update()
        {
            if (reachedEnd == false)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    NextPage();
                }
            }
        }

        private void NextPage()
        {
            pageSound.Play();
            imageIndex++;
            if (imageIndex < ListImages.Count - 1)
            {
                ShowNextPage();
            }
            else
            {
                ReachedEnd();
            }
        }

        private void ShowNextPage()
        {
            rend.sprite = ListImages[imageIndex];
        }

        private void ReachedEnd()
        {
            reachedEnd = true;
            SceneManager.LoadScene(LevelName.Lvl1_Office.ToString(), LoadSceneMode.Single);
        }
    }
}
