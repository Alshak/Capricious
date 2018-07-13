using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurricaneEyeController : MonoBehaviour {
    int Health;
    int TotalHealth;

    // Use this for initialization
    void Start()
    {
        TotalHealth = transform.parent.GetComponent<HurricaneController>().GetTotalHealth();
        Health = TotalHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (collider2d.tag == "Throwable" && Health > 0)
        {
            Health--;
            
            transform.parent.GetComponent<HurricaneController>().Hit();
            if (Health <= 0)
            {
                transform.parent.GetComponent<HurricaneController>().GoBackToDesk();
            }
        }
    }
}
