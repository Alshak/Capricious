using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableController : MonoBehaviour {
    [SerializeField] private Sprite[] throwables = new Sprite[163];

    // Use this for initialization
    void Start () {
        int randomIndex = Random.Range(0,162);
        GetComponent<SpriteRenderer>().sprite = throwables[randomIndex];
    }
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 0, 360*Time.deltaTime);
	}
}
