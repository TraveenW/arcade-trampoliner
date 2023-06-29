using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMaker : MonoBehaviour
{
    public int launcherCountMax;
    [SerializeField] GameObject Launcher;

    int launcherCount;
    bool isMousePressed;
    LineRenderer previewLine;
    Vector2 mousePosition;

    private void Start()
    {
        launcherCount = 0;
        isMousePressed = false;
        previewLine = GetComponent<LineRenderer>();
        ResetLine();
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (launcherCount < launcherCountMax)
        {
            // Set first point of line to where mouse was pressed down
            if (Input.GetMouseButtonDown(0) && !isMousePressed)
            {
                isMousePressed = true;
                previewLine.startColor = Color.green;
                previewLine.endColor = Color.green;

                previewLine.SetPosition(0, new Vector3(mousePosition.x, mousePosition.y, 0));
            }

            // Set second point where mouse is still being pressed
            if (Input.GetMouseButton(0))
            {
                previewLine.SetPosition(1, new Vector3(mousePosition.x, mousePosition.y, 0));
            }

            // Create launcher and reset line when mouse would be released
            if (Input.GetMouseButtonUp(0) && isMousePressed)
            {
                isMousePressed = false;
                Vector3 spawnOrigin = GetVector3Midpoint(previewLine.GetPosition(0), previewLine.GetPosition(1));

            }
        }
    }

    // Make line invisible and set point positions to 0, 0
    void ResetLine()
    {
        previewLine.startColor = Color.clear;
        previewLine.endColor = Color.clear;

        previewLine.SetPosition(0, new Vector3(0, 0, 0));
        previewLine.SetPosition(1, new Vector3(0, 0, 0));
    }

    // Gets two Vector 3 points and returns the midpoint of the two
    Vector3 GetVector3Midpoint(Vector3 point1, Vector3 point2)
    {
        Vector3 pointOutput;

        pointOutput = new Vector3((point1.x + point2.x) / 2, (point1.y + point2.y) / 2, (point1.z + point2.z) / 2);

        return pointOutput;
    }
}

/*
 Create int launcherCountMaxand set it to 3
Create LineRenderer line with 2 points and make it invisible
Create int launcherCount and set it to 0

Update:
wait until launcherCount is below launcherCountMax
find mouse cursor position and set it to mousePosition
when pressing down mouse:
    make line visible
    set point 1 of line to mousePosition
while mouse is pressed down:
    set point 2 to mousePosition
when mouse is released:
    instantiate Launcher exactly inbetween points 1 and 2
    rotate and scale Launcher to reach both points of line
    launcherCount += 1
    make line invisible again
 */