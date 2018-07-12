using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightController : MonoBehaviour
{

    public GameObject hurricaneTemplate;
    int nbHurricanes = 0;
    float currentTimer;
    // Use this for initialization
    void Start()
    {
        currentTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer += Time.deltaTime;
        if (nbHurricanes == 0 && currentTimer < 5f)
        {
            AddHurricane();
        }
    }

    private void AddHurricane()
    {
        Instantiate(hurricaneTemplate, transform);
        nbHurricanes++;
    }
}
