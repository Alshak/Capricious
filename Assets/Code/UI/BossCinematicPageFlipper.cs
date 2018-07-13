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
                "'I've been expecting you..' Steves boss says quitly while looking sinister.",

                "The boss turns around and looks at Steve: 'Who the hell are you!? And why do you bring cheese!? I'm lactose intolerant damnit!' Steves boss screams.",

                "'I'm the boss now! We are legion! All your base are belong to us!!' Steve yells back at his boss who is becoming more and more red in his face.",
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