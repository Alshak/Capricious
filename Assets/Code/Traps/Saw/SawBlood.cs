using Assets.Code.Humanoids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Traps.Saw
{
    public class SawBlood : MonoBehaviour
    {
        private SpriteRenderer rend;
        public Sprite BloodySprite;
        private bool isBloody = false;

        void Start()
        {
            rend = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var killable = other.GetComponent<KillableByTraps>();
            if (killable != null && isBloody == false && other.tag == "Player")
            {
                rend.sprite = BloodySprite;
            }
        }
    }
}
