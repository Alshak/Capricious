using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightController : MonoBehaviour
{
    public GameObject path;
    private List<Vector3> allPoints;
    private List<int> randomPointList;
    public GameObject tempHurricane;
    private int currentIdxFromPath;
    List<Vector3> smoothPath;
    List<Vector3> randomPath;
    // Use this for initialization
    void Start()
    {
        Transform[] pointTransformList = path.GetComponentsInChildren<Transform>();
        allPoints = new List<Vector3>();
        foreach (Transform pointTransform in pointTransformList)
        {
            allPoints.Add(pointTransform.position);
        }
        currentIdxFromPath = 1;
        randomPointList = new List<int>();
        randomPointList.Add(0);
        randomPointList.Add(1);
        randomPointList.Add(2);
        randomPointList.Add(3);
        randomPointList.Add(4);
        randomPointList.Add(5);
        randomPointList.Add(6);
        randomPointList.Add(7);
        randomPointList.Add(8);
        randomPointList.Add(9);
        randomPointList.Add(10);
        randomPointList.Add(11);
        randomPointList.Add(12);
        randomPointList.Add(13);
        randomPointList.Add(14);

        randomPath = new List<Vector3>();

        foreach (int randomPoint in randomPointList)
        {
            randomPath.Add(allPoints[randomPoint]);
        }

        smoothPath = ChaikinPath.Smoother(randomPath);
    }

    // Update is called once per frame
    float currentTime = 0f;
    void Update()
    {
        Vector3 start = smoothPath[currentIdxFromPath == 0 ? 0 : currentIdxFromPath - 1];
        Vector3 destination = smoothPath[currentIdxFromPath];
        currentTime += Time.deltaTime;
        tempHurricane.transform.position = Vector3.Lerp(start, destination, currentTime);
        if (Vector3.Distance(tempHurricane.transform.position, destination) < 0.1f)
        {
            currentIdxFromPath = (currentIdxFromPath + 1) % smoothPath.Count;
            currentTime = 0f;
        }
    }
}
