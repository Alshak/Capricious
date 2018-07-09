using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Traps
{
    /// <summary>
    /// Disables the spriteRenderer ingame.
    /// </summary>
    public class DebugSprite : MonoBehaviour
    {
        public bool ShowInGame = false;
        void Start()
        {
            if (ShowInGame == false)
            {
                var spriteRend = GetComponent<SpriteRenderer>();
                if (spriteRend != null)
                {
                    spriteRend.enabled = false;
                }
            }
        }
    }
}
