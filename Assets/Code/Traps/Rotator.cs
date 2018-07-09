using System;
using UnityEngine;

namespace Assets.Code.Traps
{
    public class Rotator : MonoBehaviour
    {
        [Range(-500f, 500f)]
        public float Rotation = 0f;
        public Space Space = Space.Self;

        private Vector3 vector;

        void Start()
        {
            vector = new Vector3(0, 0, Rotation);
        }

        // Update is called once per frame
        private void Update()
        {
            transform.Rotate(vector * Time.deltaTime, Space);
        }
    }
}
