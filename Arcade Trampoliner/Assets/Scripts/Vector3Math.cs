using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3Math : MonoBehaviour
{
    // Gets two Vector 3 points and returns the midpoint of the two
    public static Vector3 GetMidpoint(Vector3 point1, Vector3 point2)
    {
        Vector3 pointOutput;

        pointOutput = new Vector3((point1.x + point2.x) / 2, (point1.y + point2.y) / 2, (point1.z + point2.z) / 2);
        return pointOutput;
    }

    // Gets two Vector 3 points and returns a float tangent (y difference/x difference) 
    public static float GetTangentYX(Vector3 point1, Vector3 point2)
    {
        float tangent;

        tangent = (point1.y - point2.y) / (point1.x - point2.x);
        return tangent;
    }

    // Get two Vector 3 points and returns a float distance between the two
    public static float GetDistanceYX(Vector3 point1, Vector3 point2)
    {
        float distance;

        distance = Mathf.Sqrt(Mathf.Pow(point2.y - point1.y, 2) + Mathf.Pow(point2.x - point1.x, 2));
        return distance;
    }
}
