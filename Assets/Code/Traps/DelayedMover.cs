using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Traps
{
    public class DelayedMover : MonoBehaviour
    {
        [Range(0.1f, 10f)]
        public float StopTime = 2f;

        [Range(0, 100)]
        public float Speed = 10f;
        public bool IsActive = true;
        public DelayedMoverEndPosition EndPos;  

        private Vector3 startPosition;
        private Vector3 endPosition;
        private Vector3 targetPosition;
        private float timeCurrent = 0;

        void Start()
        {
            startPosition = transform.position;
            
            if (EndPos != null)
            {
                endPosition = EndPos.transform.position;
            }
            targetPosition = endPosition;
        }

        void Update()
        {
            if (IsActive)
            {
                if (timeCurrent > 0)
                {
                    timeCurrent -= Time.deltaTime;
                }
                else
                {
                    Move();
                }
            }
        }

        private void WaitTime()
        {
            var movement = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            timeCurrent = StopTime;

            if (targetPosition == endPosition)
            {
                targetPosition = startPosition;
            }
            else
            {
                targetPosition = endPosition;
            }

        }

        private void Move()
        {
            var movement = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            //X
            if (targetPosition.x < movement.x)
            {
                movement.x -= Speed * Time.deltaTime;
                if (targetPosition.x > movement.x)
                {
                    WaitTime();
                }
            }
            else if (targetPosition.x > movement.x)
            {
                movement.x += Speed * Time.deltaTime;
                if (targetPosition.x < movement.x)
                {
                    WaitTime();
                }
            }

            //Y
            if (targetPosition.y < movement.y)
            {
                movement.y -= Speed * Time.deltaTime;
                if (targetPosition.y >= movement.y)
                {
                    WaitTime();
                }
            }
            else if (targetPosition.y > movement.y)
            {
                movement.y += Speed * Time.deltaTime;
                if (targetPosition.y <= movement.y)
                {
                    WaitTime();
                }
            }

            transform.position = movement;
        }
    }
}
