using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TwoLineStatDisplay : MonoBehaviour
{
    [SerializeField] string statName;
    TMP_Text textDisplay;

    private void Start()
    {
        textDisplay = gameObject.GetComponent<TMP_Text>();
        UpdateDisplay(0);
    }

    // Update the Stat Display text with a whole number
    // Input wholeNumber: Integer number to display
    // Input separator: String that separates statName and wholeNumber
    public void UpdateDisplay(int wholeNumber)
    {
        textDisplay.text = wholeNumber.ToString();
    }
}
