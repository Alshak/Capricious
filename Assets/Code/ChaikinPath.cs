using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaikinPath : MonoBehaviour
{
    public static List<Vector3> Smooth(List<Vector3> pathToSmooth)
    {
        Vector3[] pts = pathToSmooth.ToArray();
        Vector3[] newPts = new Vector3[(pts.Length - 2) * 2 + 2];
        newPts[0] = pts[0];
        newPts[newPts.Length - 1] = pts[pts.Length - 1];

        int j = 1;
        for (int i = 0; i < pts.Length - 2; i++)
        {
            newPts[j] = pts[i] + (pts[i + 1] - pts[i]) * 0.75f;
            newPts[j + 1] = pts[i + 1] + (pts[i + 2] - pts[i + 1]) * 0.25f;
            j += 2;
        }
        return new List<Vector3>(newPts);

    }
}