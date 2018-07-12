using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightController : MonoBehaviour
{

    public GameObject hurricaneTemplate;
    int nbHurricanes = 0;
    float currentTimer;
    BOSS_PHASE currentBossPhase;
    SpriteRenderer spriteRenderer;
    GameObject firstTornado = null;
    GameObject secondTornado = null;
    Animator animator;
    public enum BOSS_PHASE
    {
        INTRO,
        FIRST_TORNADO,
        FIRST_TORNADO_FIGHT,
        AFTER_FIRST_TORNADO,
        SECOND_TORNADO,
        SECOND_TORNADO_FIGHT,
        AFTER_SECOND_TORNADO,
        TWIN_TORNADO,
        TWIN_TORNADO_FIGHT,
        OUTRO
    }
    // Use this for initialization
    void Start()
    {
        currentTimer = 0;
        currentBossPhase = BOSS_PHASE.INTRO;
        spriteRenderer = GetComponent<SpriteRenderer>();
       animator = GetComponent<Animator>();
    }

    bool hasPlayedAnimation = false;
    // Update is called once per frame
    void Update()
    {
        currentTimer += Time.deltaTime;
        switch (currentBossPhase)
        {
            case BOSS_PHASE.INTRO:
                if(!hasPlayedAnimation && currentTimer > 2f)
                {
                    animator.SetTrigger("Happy");
                    hasPlayedAnimation = true;
                }
                if (currentTimer > 5f)
                {
                    currentBossPhase++;
                }
                break;
            case BOSS_PHASE.FIRST_TORNADO:
                spriteRenderer.enabled = false;
                firstTornado = Instantiate(hurricaneTemplate, transform);
                firstTornado.GetComponent<HurricaneController>().SetTotalHealth(4);
                currentBossPhase++;
                break;
            case BOSS_PHASE.FIRST_TORNADO_FIGHT:
                if (firstTornado == null)
                {
                    currentBossPhase++;
                    currentTimer = 0f;
                    spriteRenderer.enabled = true;
                    hasPlayedAnimation = false;
                }
                break;
            case BOSS_PHASE.AFTER_FIRST_TORNADO:
                if (!hasPlayedAnimation && currentTimer > 2f)
                {
                    animator.SetTrigger("Angry");
                    hasPlayedAnimation = true;
                }
                if (currentTimer > 5f)
                {
                    currentBossPhase++;
                }
                break;
            case BOSS_PHASE.SECOND_TORNADO:
                spriteRenderer.enabled = true;
                spriteRenderer.enabled = false;
                firstTornado = Instantiate(hurricaneTemplate, transform);
                firstTornado.GetComponent<HurricaneController>().SetTotalHealth(9);
                currentBossPhase++;
                break;
            case BOSS_PHASE.SECOND_TORNADO_FIGHT:

                if (firstTornado == null)
                {
                    currentBossPhase++;
                    currentTimer = 0f;
                    hasPlayedAnimation = false;
                    spriteRenderer.enabled = true;
                }
                break;
            case BOSS_PHASE.AFTER_SECOND_TORNADO:
                if (!hasPlayedAnimation && currentTimer > 2f)
                {
                    animator.SetTrigger("Dab");
                    hasPlayedAnimation = true;
                }
                if (currentTimer > 5f)
                {
                    currentBossPhase++;
                }
                break;
            case BOSS_PHASE.TWIN_TORNADO:
                spriteRenderer.enabled = false;
                firstTornado = Instantiate(hurricaneTemplate, transform);
                secondTornado = Instantiate(hurricaneTemplate, transform);
                firstTornado.GetComponent<HurricaneController>().SetTotalHealth(7);
                secondTornado.GetComponent<HurricaneController>().SetTotalHealth(7);
                currentBossPhase++;
                break;
            case BOSS_PHASE.TWIN_TORNADO_FIGHT:
                if (firstTornado == null && secondTornado == null)
                {
                    currentBossPhase++;
                    currentTimer = 0f;
                }
                break;
            case BOSS_PHASE.OUTRO:
                Debug.Log("THE END");
                break;
        }

    }
}
