using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBox : MonoBehaviour
{
    Vector3 exitPosition;
    GameObject player;
    // Use this for initialization
    void Start()
    {
        exitPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;
        GetComponentInChildren<Camera>().orthographicSize = 7;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float distanceX = Mathf.Abs(player.transform.position.x - transform.position.x);
            float distanceY = Mathf.Abs(player.transform.position.y - transform.position.y);
            if (distanceY > 1)
            {
                transform.position = Vector2.Lerp(transform.position, player.transform.position, Time.deltaTime * 3f);
            }
            else if (distanceX > 2.5)
            {
                transform.position = Vector2.Lerp(transform.position, player.transform.position, Time.deltaTime * 2f);
            }
            else if (distanceX > 1.5)
            {
                transform.position = Vector2.Lerp(transform.position, player.transform.position, Time.deltaTime * 1.2f);
            }
        }
    }
}
