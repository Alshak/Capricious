using UnityEngine;
using UnityStandardAssets._2D;

public class CreditsBox : MonoBehaviour
{
    GameObject player;
    public Transform leftTopBound;
    public Transform rightBottomBound;

    private PlatformerCharacter2D _character;
    private float _lastSpeeds = 0;

    public void Start()
    {
        
        GetComponentInChildren<Camera>().orthographicSize = 7;
    }

    public void Update()
    {
        if (player != null)
        {
            _lastSpeeds = 2f;
            var facingCoef = 1;

            var cameraLead = player.transform.position + new Vector3(1, 0) * _lastSpeeds * 2.5f * facingCoef;
            float distanceX = Mathf.Abs(cameraLead.x - transform.position.x);
            float distanceY = Mathf.Abs(cameraLead.y - transform.position.y);
            if (distanceY > 1.5f)
            {
                transform.position = Vector2.Lerp(transform.position, cameraLead, Time.deltaTime * 3f);
            }
            else if (distanceX > 2.5f)
            {
                transform.position = Vector2.Lerp(transform.position, cameraLead, Time.deltaTime * 4f);
            }
            else if (distanceX > 1.5f)
            {
                transform.position = Vector2.Lerp(transform.position, cameraLead, Time.deltaTime * 1.2f);
            }
            if(leftTopBound != null  && rightBottomBound !=null)
            {
                transform.position = new Vector2(Mathf.Clamp(transform.position.x, leftTopBound.position.x, rightBottomBound.position.x),
                    Mathf.Clamp(transform.position.y, rightBottomBound.position.y, leftTopBound.position.y));
            }
        }
    }

    public void SetPlayer(GameObject newPlayer)
    {
        player = newPlayer;
        var character = player.GetComponent<PlatformerCharacter2D>();
        if (character != null)
        {
            _character = character;
        }
        //transform.position = player.transform.position;
    }
}
