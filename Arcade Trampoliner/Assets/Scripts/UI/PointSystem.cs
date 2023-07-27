using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    [SerializeField] GameObject crowdLights;

    [HideInInspector] public int pointNumber;

    // Start is called before the first frame update
    void Start()
    {
        pointNumber = 0;
    }

    // Increment hit and update display
    public void IncrementPoint()
    {
        pointNumber++;
        GetComponent<TwoLineStatDisplay>().UpdateDisplay(pointNumber);

        crowdLights.GetComponent<CrowdLights>().UpdateLights(pointNumber);
    }
}
