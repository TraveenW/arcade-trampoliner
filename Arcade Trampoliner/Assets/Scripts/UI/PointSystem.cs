using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    [HideInInspector] public int pointNumber;

    // Start is called before the first frame update
    void Start()
    {
        pointNumber = 0;
    }

    // Increment hit and update display
    public void IncrementHit()
    {
        pointNumber++;
        GetComponent<TwoLineStatDisplay>().UpdateDisplay(pointNumber);
    }
}
