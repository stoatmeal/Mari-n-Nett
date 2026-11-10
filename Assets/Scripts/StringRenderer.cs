using System.Collections.Generic;
using UnityEngine;

public class StringRenderer : MonoBehaviour
{
    public Transform originPoint;
    public GameObject linePrefab;

    public List<Transform> targetObjects = new List<Transform>(); 

    private List<LineRenderer> activeLines = new List<LineRenderer>();

    void Start()
    {
        if (originPoint == null)
        {
            originPoint = this.transform;
        }

        StartLines();
    }

    void Update()
    {
        UpdateLinePositions();
    }

    void StartLines()
    {
        foreach (var line in activeLines)
        {
            if (line != null) Destroy(line.gameObject);
        }
        activeLines.Clear();

        for (int i = 0; i < targetObjects.Count; i++)
        {
            GameObject lineObj = Instantiate(linePrefab, this.transform);
            LineRenderer lr = lineObj.GetComponent<LineRenderer>();
            
            if (lr != null)
            {
                lr.positionCount = 2; 
                activeLines.Add(lr);
            }
        }
    }

    void UpdateLinePositions()
    {
        int lineIndex = 0;

        for (int i = 0; i < targetObjects.Count; i++)
        {
            // Skip if the target was destroyed or missing
            if (targetObjects[i] == null) continue; 
            if (lineIndex >= activeLines.Count) break;

            LineRenderer lr = activeLines[lineIndex];
            
            // Set Start Point (Index 0) and End Point (Index 1)
            lr.SetPosition(0, originPoint.position);
            lr.SetPosition(1, targetObjects[i].position);

            lineIndex++;
        }
    }

    // Call this if your target list changes dynamically during runtime
    public void RefreshTargets(List<Transform> newTargets)
    {
        targetObjects = newTargets;
        StartLines();
    }
}
