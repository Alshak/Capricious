using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Traps
{
    public class RotatorCenter : MonoBehaviour
    {
        [Range(-100, 100)]
        public float RotationSpeed;

        void Update()
        {
            var p = this.transform.position;
            this.transform.Rotate(new Vector3(0, 0, p.z), RotationSpeed * Time.deltaTime);
        }
    }
}
