using UnityEngine;

public class SpawnerContainer : MonoBehaviour
{
    public SpawnerMarker[] Markers { get; private set; }

    private void Awake()
    {
        Markers = GetComponentsInChildren<SpawnerMarker>();
    }

    public Transform GetRandomMarker()
    {
        if (Markers.Length == 0) return null;
        return Markers[Random.Range(0, Markers.Length)].transform;
    }
}
