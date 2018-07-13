using System.Collections.Generic;
using UnityEngine;

public class HurricaneController : MonoBehaviour
{
    BossFightController bossFightController;
    GameObject path;
    GameObject player;
    private List<Vector3> allPoints;
    private List<int> randomPointList;
    private int currentIdxFromPath;
    List<Vector3> smoothPath;
    int TotalHealth;
    internal int GetTotalHealth()
    {
        return TotalHealth;
    }

    public void SetTotalHealth(int pTotalHealth)
    {
        TotalHealth = pTotalHealth;
        transform.localScale = new Vector3(0.2f * pTotalHealth + .8f, 0.2f * pTotalHealth + .8f, 0.2f * pTotalHealth + .8f);
    }

    List<Vector3> randomPath;
    bool backToDesk = false;
    public float Speed = 4;
    public bool cancelBackToDesk = false;
    // Use this for initialization
    void Start()
    {
        backToDesk = false;
        path = GameObject.FindGameObjectWithTag("Path");
        player = GameObject.FindGameObjectWithTag("Player");
        Transform[] pointTransformList = path.GetComponentsInChildren<Transform>();
        allPoints = new List<Vector3>();
        foreach (Transform pointTransform in pointTransformList)
        {
            allPoints.Add(pointTransform.position);
        }
        allPoints.RemoveAt(0);
        ChangePath();
        smoothPath.Insert(0, allPoints[0]);
        smoothPath.Insert(1, allPoints[1]);
    }

    internal void GoBackToDesk()
    {
        backToDesk = true;
        currentTime = 0f;
    }

    private void ChangePath()
    {
        currentIdxFromPath = 1;
        randomPointList = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            int randomIndex = Random.Range(0, allPoints.Count);
            while (randomPointList.Contains(randomIndex))
            {
                randomIndex = Random.Range(0, allPoints.Count);
            }
            randomPointList.Add(randomIndex);
        }
        randomPath = new List<Vector3>();

        foreach (int randomPoint in randomPointList)
        {
            randomPath.Add(allPoints[randomPoint]);
        }

        smoothPath = ChaikinPath.Smooth(randomPath);
    }

    // Update is called once per frame
    float currentTime = 0f;
    void Update()
    {
        if (!backToDesk)
        {
            Vector3 start = smoothPath[currentIdxFromPath == 0 ? 0 : currentIdxFromPath - 1];
            Vector3 destination = smoothPath[currentIdxFromPath];
            currentTime += Time.deltaTime / Vector3.Distance(start, destination);
            transform.position = Vector3.Lerp(start, destination, currentTime * Speed);
            if (Vector3.Distance(transform.position, destination) < 0.1f)
            {
                if (currentIdxFromPath + 1 == smoothPath.Count)
                {
                    ChangePath();
                    smoothPath.Insert(0, destination);
                    smoothPath.Insert(2, player.transform.position);
                }
                else
                {
                    currentIdxFromPath = currentIdxFromPath + 1;
                }
                currentTime = 0f;
            }
        }
        else if(backToDesk && !cancelBackToDesk)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, allPoints[0], currentTime * 0.1f);
            if (Vector3.Distance(transform.position, allPoints[0]) < 0.1f)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            bossFightController.LastTornadoPosition = transform.position;
            Destroy(this.gameObject);
        }
    }

    internal void DoNotGoBack(BossFightController fightController)
    {
        bossFightController = fightController;
        cancelBackToDesk = true;
    }
}
