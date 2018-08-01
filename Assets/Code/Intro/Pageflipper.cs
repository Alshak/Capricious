using Assets.Code.LevelChange;
using Assets.Code.Spawning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets._2D;

namespace Assets.Code.Intro
{
    public class Pageflipper : MonoBehaviour
    {
        public GameObject Player;
        public Text storyText;
        public GameObject bottomBorder;

        public List<Sprite> ListImages;
        private List<String> ListStory;

        private AudioSource pageSound;
        private int imageIndex = 0;
        private bool reachedEnd = false;
        
        private Image rend;
        private IntroControls introControls;
        private Checkpoint checkpoint;
        private CameraBox cameraBox;
        private MusicManager musicManager;
        
        void Start()
        {
            pageSound = GetComponent<AudioSource>();
            rend = GetComponent<Image>();
            introControls = GameObject.FindObjectOfType<IntroControls>();
            checkpoint = GameObject.FindObjectOfType<Checkpoint>();
            cameraBox = GameObject.FindObjectOfType<CameraBox>();
            musicManager = GameObject.FindObjectOfType<MusicManager>();
            DisplayControls(false);
            musicManager.StopAllMusic();

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
                "A typical medieval castle, home of an Evil Mastermind bent on world domination.\n" +
                "Have you ever wondered what goes on behind the scenes of such an establishment? No?\n" +
                "Well, I’ll tell you anyhow. You signed up for this.",

                "Welcome to Super Evil Corp where dozens of happy clones named Steve keep the place up and running." +
                " I mean, somebody has to do the paperwork, after all. Evil schemes require money, and doing the paperwork is just depressing." +
                " I mean, have you seen the tax paperwork these days? It’s insane. " +
                "Anyhow, our story begins one busy Monday morning at the office, when one little inconvenience will set in motion a series of events..",

                "'Hey Steve!' said Steve. The boss wants his Capricious Cheese. You know how moody he becomes without it.",

                "Steve walked up to his usual path, planning to take the elevator to the upper offices. He was worried when he saw it wasn't working." +
                " That had never happened before, and his worry turned into panic. " +
                "The boss really needed to get his cheese, otherwise all hell would break loose in their offices.",

                "Steve No. 1 was a model employee, or so he thought. " +
                "So he decided to do what needs to be done- he would take the Hero Path, an obstacle course designed to keep annoying heroes from bothering the boss." +
                " ‘’I can do this’’ thought Steve and sucked up the courage to enter the door.",

                "As he pushed the door open, he had a feeling his life would change… And thus his journey began."
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
            introControls.enabled = true;
            rend.enabled = false;
            
            var player = Instantiate(Player);
            player.transform.position = checkpoint.transform.position;
            cameraBox.SetPlayer(player.gameObject);
            introControls.enabled = true;
            DisplayControls(true);
            musicManager.PlayMainMenu();

            foreach (var images in bottomBorder.GetComponentsInChildren<Image>())
            {
                images.enabled = false;
            }

            foreach (var text in bottomBorder.GetComponentsInChildren<Text>())
            {
                text.enabled = false;
            }

            foreach (var images in GetComponentsInChildren<Image>())
            {
                images.enabled = false;
            }

            foreach (var text in GetComponentsInChildren<Text>())
            {
                text.enabled = false;
            }
        }

        private void DisplayControls(bool value)
        {
            foreach (var images in introControls.GetComponentsInChildren<Image>())
            {
                images.enabled = value;
            }

            foreach (var text in introControls.GetComponentsInChildren<Text>())
            {
                text.enabled = value;
            }
        }
    }
}
