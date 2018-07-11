using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBox : MonoBehaviour
{
    GameObject player;
    public Transform leftTopBound;
    public Transform rightBottomBound;
    // Use this for initialization
    void Start()
    {
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
            if (distanceY > 1.5f)
            {
                transform.position = Vector2.Lerp(transform.position, player.transform.position, Time.deltaTime * 3f);
            }
            else if (distanceX > 2.5f)
            {
                transform.position = Vector2.Lerp(transform.position, player.transform.position, Time.deltaTime * 2f);
            }
            else if (distanceX > 1.5f)
            {
                transform.position = Vector2.Lerp(transform.position, player.transform.position, Time.deltaTime * 1.2f);
            }
            if(leftTopBound != null  && rightBottomBound !=null)
            {
                transform.position = new Vector2(Mathf.Clamp(transform.position.x, leftTopBound.position.x, rightBottomBound.position.x),
                    Mathf.Clamp(transform.position.y, rightBottomBound.position.y, leftTopBound.position.y));
            }
        }
    }
}
