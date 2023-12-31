using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMaker : MonoBehaviour
{
    [Header("Draw Settings")]
    [SerializeField] float minXRange = -5.5f;
    [SerializeField] float maxXRange = 5.5f;
    [SerializeField] float minYRange = -10;
    [SerializeField] float maxYRange = 10;

    [Header("Line Settings")]
    [SerializeField] int launcherCountMax;
    [SerializeField] float launcherPreviewWidthProportion;
    [SerializeField] GameObject launcherPrefab;

    public List<GameObject> launchers;

    int launcherCount;
    bool isInputPressed;
    LineRenderer previewLine;

    bool inputDown;
    bool inputPressed;
    bool inputUp;
    Vector2 inputPosition;

    private void Start()
    {
        launcherCount = 0;
        isInputPressed = false;
        previewLine = GetComponent<LineRenderer>();
        launchers = new List<GameObject>();
        previewLine.startWidth = launcherPrefab.transform.localScale.y * launcherPreviewWidthProportion;
        previewLine.endWidth = launcherPrefab.transform.localScale.y * launcherPreviewWidthProportion;
        ResetLine();
    }

    private void Update()
    {
        // convert mouse controls to generalised input
        inputPosition = MouseControl();

        // Set first point of line to where input method was pressed down
        if (inputDown && !isInputPressed)
        {
            isInputPressed = true;
            previewLine.startColor = Color.green;
            previewLine.endColor = Color.green;

            previewLine.SetPosition(0, ClampVectorXY(new Vector3(inputPosition.x, inputPosition.y, 0)));
        }

        // Set second point where input method is still being pressed
        if (inputPressed)
        {
            previewLine.SetPosition(1, ClampVectorXY(new Vector3(inputPosition.x, inputPosition.y, 0)));
        }

        // Create launcher and reset line when input method would be released, and if line distance is more than 0
        if (inputUp && isInputPressed)
        {
            isInputPressed = false;
            if (Vector3Math.GetDistanceYX(previewLine.GetPosition(0), previewLine.GetPosition(1)) > 0)
            {
                SpawnLauncher();
            }
            ResetLine();
        }
    }

    // Returns coordinates of mouse position converted to world space Also converts different mouse inputs to program's input bools.
    Vector2 MouseControl()
    {
        inputDown = Input.GetMouseButtonDown(0);
        inputPressed = Input.GetMouseButton(0);
        inputUp = Input.GetMouseButtonUp(0);

        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Make line invisible and set point positions to 0, 0
    void ResetLine()
    {
        previewLine.startColor = Color.clear;
        previewLine.endColor = Color.clear;

        previewLine.SetPosition(0, new Vector3(0, 0, 0));
        previewLine.SetPosition(1, new Vector3(0, 0, 0));
    }

    // Spawn launcher and match with line
    void SpawnLauncher()
    {
        Vector3 spawnOrigin = Vector3Math.GetMidpoint(previewLine.GetPosition(0), previewLine.GetPosition(1));
        float spawnRot = Mathf.Rad2Deg * Mathf.Atan(Vector3Math.GetTangentYX(previewLine.GetPosition(0), previewLine.GetPosition(1)));
        float spawnScale = Vector3Math.GetDistanceYX(previewLine.GetPosition(0), previewLine.GetPosition(1));

        GameObject newLauncher = Instantiate(launcherPrefab, spawnOrigin, Quaternion.Euler(0, 0, spawnRot)) as GameObject;
        newLauncher.transform.localScale = new Vector3(spawnScale, newLauncher.transform.localScale.y, 1);
        launcherCount++;
        launchers.Add(newLauncher);

        // Destroy oldest launcher if above launcher countw
        if (launchers.Count > launcherCountMax)
        {
            launchers[0].GetComponent<Rebound>().DespawnRebound();
            launchers.RemoveAt(0);
            launcherCount--;
        }
    }

    // Clamp X and Y vectors while preserving the Z vector
    Vector3 ClampVectorXY(Vector3 vector)
    {
        Vector3 clampedVector = new Vector3();

        if (vector.x > maxXRange) { clampedVector.x = maxXRange; }
        else if (vector.x < minXRange) { clampedVector.x = minXRange; }
        else { clampedVector.x = vector.x; }

        if (vector.y > maxYRange) { clampedVector.y = maxYRange; }
        else if (vector.y < minYRange) { clampedVector.y = minYRange; }
        else { clampedVector.y = vector.y; }

        clampedVector.z = vector.z;

        return clampedVector;
    }
}