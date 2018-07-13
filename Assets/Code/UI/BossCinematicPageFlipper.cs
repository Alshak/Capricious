using Assets.Code.LevelChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Code.UI
{
    public class BossCinematicPageFlipper : MonoBehaviour
    {
        public List<Sprite> ListImages;
        private List<String> ListStory;

        private AudioSource pageSound;
        private int imageIndex = 0;
        private Image rend;
        private bool reachedEnd = false;
        public Text storyText;

        void Start()
        {
            pageSound = GetComponent<AudioSource>();
            rend = GetComponent<Image>();
            InitTexts();
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

        private void InitTexts()
        {
            ListStory = new List<string>
            {
                "Boss: I have been expecting you.. Wait, you're not Captain McSuperface. What are you doing here?",

                "Steve: I brought you your favourite Capricious Cheese boss, the elevator was broken so I had to-" +
                "Boss: YOU BROUGHT ME WHAT?! AT THIS LATE HOUR?! I'M ON A DIET, YOU INSENSITIVE *#@^*$^#*!!! " +
                "Steve had just been through an experience that changed him. He no longer felt pride in his job, or loyalty towards his boss. " +
                "So, for the first time ever, his personality broke loose - He threw the cheese at his boss.",

                "First half of panel: YOU INSOLENT FOOL! YOU WORTHLESS WASTE OF A CLONE! YOU'RE GOING TO REGRET THIS!\n" +
                "I’m going to rock you…\n\n" +
                "..like a hurricane."
            };
            storyText.text = ListStory[0];
        }

        private void NextPage()
        {
            if (pageSound.isPlaying == false)
            {
                pageSound.Play();
            }
            imageIndex++;
            if (imageIndex < ListImages.Count)
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
            storyText.text = ListStory[imageIndex];
        }

        private void ReachedEnd()
        {
            reachedEnd = true;
            SceneManager.LoadScene(LevelName.BossFight.ToString(), LoadSceneMode.Single);
        }
    }
}