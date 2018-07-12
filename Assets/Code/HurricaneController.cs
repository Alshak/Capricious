using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurricaneController : MonoBehaviour
{

    GameObject path;
    GameObject player;
    private List<Vector3> allPoints;
    private List<int> randomPointList;
    private int currentIdxFromPath;
    List<Vector3> smoothPath;
    List<Vector3> randomPath;

    public float Speed = 4;
    // Use this for initialization
    void Start()
    {
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
}
