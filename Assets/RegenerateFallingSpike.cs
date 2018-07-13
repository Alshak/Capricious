using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerateFallingSpike : MonoBehaviour {
    Vector3 initialPosition;
	// Use this for initialization
	void Start () {
        initialPosition = transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -500)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            transform.position = initialPosition;
        }
	}
}
