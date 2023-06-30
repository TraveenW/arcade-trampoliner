using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMaker : MonoBehaviour
{
    [SerializeField] int launcherCountMax;
    [SerializeField] float launcherPreviewWidthProportion;
    [SerializeField] GameObject launcherPrefab;

    int launcherCount;
    bool isMousePressed;
    LineRenderer previewLine;
    List<GameObject> launchers;
    Vector2 mousePosition;

    private void Start()
    {
        launcherCount = 0;
        isMousePressed = false;
        previewLine = GetComponent<LineRenderer>();
        launchers = new List<GameObject>();
        previewLine.startWidth = launcherPrefab.transform.localScale.y * launcherPreviewWidthProportion;
        previewLine.endWidth = launcherPrefab.transform.localScale.y * launcherPreviewWidthProportion;
        ResetLine();
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
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

        // Create launcher and reset line when mouse would be released and if line distance is more than 0
        if (Input.GetMouseButtonUp(0) && isMousePressed)
        {
            isMousePressed = false;
            if (Vector3Math.GetDistance(previewLine.GetPosition(0), previewLine.GetPosition(1)) > 0)
            {
                SpawnLauncher();
            }
            ResetLine();
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

    // Spawn launcher and match with line
    void SpawnLauncher()
    {
        Vector3 spawnOrigin = Vector3Math.GetMidpoint(previewLine.GetPosition(0), previewLine.GetPosition(1));
        float spawnRot = Mathf.Rad2Deg * Mathf.Atan(Vector3Math.GetTangentYX(previewLine.GetPosition(0), previewLine.GetPosition(1)));
        float spawnScale = Vector3Math.GetDistance(previewLine.GetPosition(0), previewLine.GetPosition(1));

        GameObject newLauncher = Instantiate(launcherPrefab, spawnOrigin, Quaternion.Euler(0, 0, spawnRot)) as GameObject;
        newLauncher.transform.localScale = new Vector3(spawnScale, newLauncher.transform.localScale.y, 1);
        launcherCount++;
        launchers.Add(newLauncher);

        // Destroy oldest launcher if above launcher count
        if (launchers.Count > launcherCountMax)
        {
            // Replace destroy procedure with activating coroutine for animation
            Destroy(launchers[0]);
            launchers.RemoveAt(0);
            launcherCount--;
        }
    }
}