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
        public Platformer2DUserControl Player;
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
                "This is the Super Evil Corporation Castle.",

                "Here all the employees worked on finalizing the boss plans for world domination.\n" +
                "They were all named Steve as the boss had trouble remembering what his minions were called.",

                "'Hey Steve!' said Steve. The boss wants his Capricious Cheese. You know how moody he becomes without it.",

                "Oh no! Someone broke the elevator again, probably by practising wall jumping. Now the only way to the boss is by going through the hero route." +
                "The route was reserved for heroes trying to stop the Boss from putting his super evil plans into motion.",

                "Steve was a model employee, or so he thought. They never really met the boss or recieved any feedback on their work.\n" +
                "But he would do his duty as an employee and deliver the cheese to his boss, like any employee would.",

                "Steve went to the front gate of the castle and begun his route up to the boss. This would be the first time he met the boss.\n" +
                "Hopefully he wouldn't be in a capricious mood."
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
