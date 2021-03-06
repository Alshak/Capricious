﻿using Assets.Code.LevelChange;
using Assets.Code.Spawning;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets._2D;
using UnityStandardAssets.CrossPlatformInput;

public class BossFightController : MonoBehaviour
{

    public GameObject hurricaneTemplate;
    public GameObject endCloneTemplate;
    public Text congratsText;
    public SpriteRenderer background;
    public Image logo;
    public Image blackFade;
    public List<AudioClip> audioClips;
    int nbHurricanes = 0;
    float currentTimer;
    BOSS_PHASE currentBossPhase;
    SpriteRenderer spriteRenderer;
    GameObject firstTornado = null;
    GameObject secondTornado = null;
    Animator animator;
    GameObject player;
    GameObject gameController;
    List<Decimal> previousXpositions;
    private MusicManager musicManager;
    Vector3 lastTornadoPosition;
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
        BOSS_DYING,
        SPAWN_STEVES,
        END
    }
    // Use this for initialization
    void Start()
    {
        currentTimer = 0;
        currentBossPhase = BOSS_PHASE.INTRO;
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController");
        animator = GetComponent<Animator>();
        previousXpositions = new List<Decimal>();
        musicManager = GameObject.FindObjectOfType<MusicManager>();
    }

    void PlaySound(String name)
    {
        if (audioClips.Count > 0)
        {
            int idxAudio = 0;
            switch (name)
            {
                case "Dab":
                    idxAudio = 0;
                    break;
                case "Angry":
                    idxAudio = 1;
                    break;
                case "Happy":
                    idxAudio = 2;
                    break;
            }
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().PlayOneShot(audioClips[idxAudio]);
            }
        }
    }
    bool hasPlayedAnimation = false;

    public Vector3 LastTornadoPosition
    {
        get
        {
            return lastTornadoPosition;
        }

        set
        {
            lastTornadoPosition = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer += Time.deltaTime;
        
        switch (currentBossPhase)
        {
            case BOSS_PHASE.INTRO:
                if(!spriteRenderer.enabled)
                {
                    spriteRenderer.enabled = true;
                }
                if (!hasPlayedAnimation && currentTimer > 2f)
                {
                    animator.SetTrigger("Happy");
                    PlaySound("Happy");
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
                    animator.SetTrigger("Dab");
                    PlaySound("Dab");
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
                    animator.SetTrigger("Angry");
                    PlaySound("Angry");
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
                firstTornado.GetComponent<HurricaneController>().DoNotGoBack(this);
                secondTornado.GetComponent<HurricaneController>().SetTotalHealth(7);
                secondTornado.GetComponent<HurricaneController>().DoNotGoBack(this);
                currentBossPhase++;
                break;
            case BOSS_PHASE.TWIN_TORNADO_FIGHT:
                if (firstTornado == null && secondTornado == null)
                {
                    transform.position = lastTornadoPosition;
                    currentBossPhase++;
                    currentTimer = 0f;
                    spriteRenderer.enabled = true;
                    animator.SetTrigger("Die");
                    if (musicManager != null)
                    {
                        Debug.Log("PLAY MUSCI");
                        musicManager.PlayVictory();
                    }
                }
                break;
            case BOSS_PHASE.BOSS_DYING:
                Timer.PauseTimer();
                transform.position -= new Vector3(0, 0.1f, 0);
                if(currentTimer > 2f)
                {
                    transform.position = new Vector3(0, -2.5f, 0);
                    currentBossPhase++;
                    currentTimer = 0f;
                    spriteRenderer.enabled = false;
                    logo.enabled = true;
                    congratsText.enabled = true;
                    congratsText.text += (31 - gameController.GetComponentInChildren<PlayerLives>().GetLives());
                }
                break;
            case BOSS_PHASE.SPAWN_STEVES:
                background.color = Color.Lerp(background.color, Color.white, currentTimer);
                int currentLives = gameController.GetComponentInChildren<PlayerLives>().GetLives();
                if (currentLives > 1)
                {
                    GameObject steveClone = Instantiate(endCloneTemplate, new Vector3(-10, -3, 0), Quaternion.identity);
                    Decimal roundedValue = 0;
                    float xPosition;
                    do
                    {
                        if (UnityEngine.Random.Range(1, 3) == 2)
                        {
                            xPosition = UnityEngine.Random.Range(-8f, -1.7f);
                        }
                        else
                        {
                            xPosition = UnityEngine.Random.Range(1.8f, 8f);
                        }
                        roundedValue = Math.Round((Decimal)xPosition, 1, MidpointRounding.AwayFromZero);
                    } while (previousXpositions.Contains(roundedValue));
                    previousXpositions.Add(roundedValue);
                    steveClone.GetComponent<CloneController>().SetDirection(Assets.Code.AI.AIDirection.Right, new Vector3(xPosition, -3, 0));
                    steveClone.GetComponent<SpriteRenderer>().sortingOrder = 10 - currentLives;
                    gameController.GetComponentInChildren<PlayerLives>().Reduce();
                }
                else
                {
                    currentTimer = 0f;
                    currentBossPhase++;
                }
                break;
            case BOSS_PHASE.END:
                if (currentTimer > 15f)
                {
                    blackFade.color = Color.Lerp(blackFade.color, Color.black, currentTimer * 0.001f);
                }
                if (currentTimer > 20f)
                {
                        SceneManager.LoadScene(LevelName.CreditsScene.ToString(), LoadSceneMode.Single);

                }
                break;
        }

    }
}
