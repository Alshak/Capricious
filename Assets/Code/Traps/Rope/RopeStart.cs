using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Traps.Rope
{
    /// <summary>
    /// The point from which the rope starts.
    /// </summary>
    public class RopeStart : MonoBehaviour
    {
        void Start()
        {
            var spriteRend = GetComponent<SpriteRenderer>();
            if (spriteRend != null)
            {
                spriteRend.enabled = false;
            }
        }
    }
}
