using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdLights : MonoBehaviour
{
    [SerializeField] int minimumPoints = 3;
    [SerializeField] int maximumPoints = 25;


    // Used by PointSystem to update the lights according to the points
    public void UpdateLights(int currPoints)
    {
        if (currPoints > minimumPoints && currPoints < maximumPoints)
        {
            Color currColor = GetComponent<SpriteRenderer>().color;
            float alphaRatio = Mathf.Abs(currPoints - minimumPoints) / Mathf.Abs(maximumPoints - minimumPoints);

            GetComponent<SpriteRenderer>().color = new Color(currColor.r, currColor.g, currColor.b, alphaRatio);
        }
    }
}
