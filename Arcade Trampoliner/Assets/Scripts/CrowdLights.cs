using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdLights : MonoBehaviour
{
    [SerializeField] int minimumPoints = 3;
    [SerializeField] int maximumPoints = 25;

    Color currColor;
    float alphaRatio;

    private void Start()
    {
        currColor = GetComponent<SpriteRenderer>().color;

        GetComponent<SpriteRenderer>().color = new Color(currColor.r, currColor.g, currColor.b, 0);
    }


    // Used by PointSystem to update the lights according to the points
    public void UpdateLights(int currPoints)
    {


        if (currPoints > minimumPoints && currPoints <= maximumPoints)
        {
            alphaRatio = ((float)currPoints - (float)minimumPoints) / ((float)maximumPoints - (float)minimumPoints);
            currColor.a = alphaRatio;

            GetComponent<SpriteRenderer>().color = currColor;
        }
    }
}
