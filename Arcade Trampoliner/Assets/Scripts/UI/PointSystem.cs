using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    [SerializeField] GameObject crowdLights;
    [SerializeField] GameObject basket;

    [HideInInspector] public int pointNumber;

    // Start is called before the first frame update
    void Start()
    {
        pointNumber = 0;
        GetComponent<TMP_Text>().text = "0";
    }

    // Increment hit and update display
    public void IncrementPoint()
    {
        pointNumber++;
        GetComponent<TMP_Text>().text = pointNumber.ToString();

        crowdLights.GetComponent<CrowdLights>().UpdateLights(pointNumber);
        basket.GetComponent<BasketMovement>().BasketReact(pointNumber);
    }
}
