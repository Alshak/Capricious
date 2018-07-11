using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Spawning
{
    public class PlayerLives : MonoBehaviour
    {
        public static int LivesLeft = 30;

        public int GetLives()
        {
            return LivesLeft;
        }

        public void Reduce()
        {
            LivesLeft--;
        }
    }
}
