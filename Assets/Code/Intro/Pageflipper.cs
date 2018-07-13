﻿using Assets.Code.LevelChange;
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
        private AudioSource pageSound;
        private int imageIndex = 0;
        private bool reachedEnd = false;
        public List<Sprite> ListImages;
        private Image rend;
        private IntroControls introControls;
        public Platformer2DUserControl Player;
        private Checkpoint checkpoint;
        private CameraBox cameraBox;
        public Text textPrototype;

        void Start()
        {
            pageSound = GetComponent<AudioSource>();
            rend = GetComponent<Image>();
            introControls = GameObject.FindObjectOfType<IntroControls>();
            checkpoint = GameObject.FindObjectOfType<Checkpoint>();
            cameraBox = GameObject.FindObjectOfType<CameraBox>();
            DisplayControls(false);
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
                ReachedEnd();
                //ShowNextPage();
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
            introControls.enabled = true;
            rend.enabled = false;
            
            var player = Instantiate(Player);
            player.transform.position = checkpoint.transform.position;
            cameraBox.SetPlayer(player.gameObject);
            introControls.enabled = true;
            DisplayControls(true);

            if (textPrototype != null)
            {
                textPrototype.enabled = false;
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
