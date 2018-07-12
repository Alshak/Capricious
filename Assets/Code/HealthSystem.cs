using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {

    public int Health = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collider2d)
    {
       if (collider2d.tag == "Throwable")
        {
            Health--;
            transform.parent.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
            Debug.Log(Health);
            if(Health <= 0)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
