using Assets.Code.LevelChange;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusicLauncher : MonoBehaviour {

    public MusicManager musicManager;
    bool firstInstruction;
    // Use this for initialization
    void Start () {
        firstInstruction = true;


    }
	
	// Update is called once per frame
	void Update () {
		if(firstInstruction)
        {
            musicManager.PlayBoss1();
            firstInstruction = false;
        }
	}
}
