using Assets.Code.Humanoids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Traps
{
    public class Killscript : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            var killable = other.GetComponent<KillableByTraps>();
            if (killable != null)
            {
                killable.Kill();
            }
        }
    }
}
