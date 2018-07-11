using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.AI
{
    public class AITurnAround : MonoBehaviour
    {
        public AIDirection Direction;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Enemy")
            {
                var enemy = other.GetComponent<EnemyController>();
                enemy.SetDirection(Direction);
            }
        }
    }

    public enum AIDirection
    {
        Left,
        Right
    }
}
