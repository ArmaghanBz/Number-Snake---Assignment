using System.Collections.Generic;
using UnityEngine;
public class PathRecorder : MonoBehaviour
{
    public static PathRecorder Instance { get; private set; }
    public int maxPositions = 2000; // keep enough history
    public float recordInterval = 0.02f; // seconds between recorded samples

    private List<Vector3> positions = new List<Vector3>();
    private float timer = 0f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= recordInterval)
        {
            timer = 0f;
            Record(transform.position);
        }
    }

    public void Record(Vector3 pos)
    {
        positions.Insert(0, pos); // newest at index 0
        if (positions.Count > maxPositions) positions.RemoveAt(positions.Count - 1);
    }

    // Get recorded position at a given sample index (0 newest)
    public Vector3 GetPositionAtIndex(int index)
    {
        if (positions.Count == 0) return transform.position;
        if (index < 0) index = 0;
        if (index >= positions.Count) index = positions.Count - 1;
        return positions[index];
    }
    public int AvailableSamples => positions.Count;
}


