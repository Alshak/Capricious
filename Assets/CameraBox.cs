using UnityEngine;
using UnityStandardAssets._2D;

public class CameraBox : MonoBehaviour
{
    GameObject player;
    public Transform leftTopBound;
    public Transform rightBottomBound;

    private PlatformerCharacter2D _character;
    private float _lastSpeeds = 0;

    public void Start()
    {
        player = GameObject.Find("Player");
        _character = player.GetComponent<PlatformerCharacter2D>();
        transform.position = player.transform.position;
        GetComponentInChildren<Camera>().orthographicSize = 7;
    }

    public void Update()
    {
        if (player != null)
        {
            var historyLean = 0.7f;
            var currentLean = _character.m_Grounded ? 1 + (1 - historyLean) : 0;
            _lastSpeeds = _character.CurrentSpeed().x*currentLean + _lastSpeeds * historyLean;

            var facingCoef = _character.IsFacingRight() ? 1 : -1;

            var cameraLead = player.transform.position + new Vector3(1, 0) * _lastSpeeds * 9 * facingCoef;
            float distanceX = Mathf.Abs(cameraLead.x - transform.position.x);
            float distanceY = Mathf.Abs(cameraLead.y - transform.position.y);
            if (distanceY > 1.5f)
            {
                transform.position = Vector2.Lerp(transform.position, cameraLead, Time.deltaTime * 3f);
            }
            else if (distanceX > 2.5f)
            {
                transform.position = Vector2.Lerp(transform.position, cameraLead, Time.deltaTime * 3f);
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
}
