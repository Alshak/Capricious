using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Traps
{
    public class Rotator : MonoBehaviour
    {
        [Range(-100, 100)]
        public float RotationSpeed;
        
        void Update()
        {
            var p = this.transform.position;
            this.transform.RotateAroundLocal(new Vector3(0,0, p.z), RotationSpeed * Time.deltaTime);
            //this.transform.RotateAround(this.transform.position, RotationSpeed * Time.deltaTime);
;        }
    }
}
