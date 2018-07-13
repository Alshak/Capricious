using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeathSound : MonoBehaviour
{
    public GameObject DeathSoundPlayer;

    public void PlayDeath()
    {
        Instantiate(DeathSoundPlayer);
    }
}
