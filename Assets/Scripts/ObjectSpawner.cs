using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Prefab of the object to spawn
    public Transform spawnPoint; // Location where the object will appear

    void Start()
    {
        if (GameStateManager.Instance != null && GameStateManager.Instance.isPuzzleSolved)
        {
            // Spawn the object
            Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("Object spawned because the puzzle was solved.");
        }
        else
        {
            Debug.Log("Puzzle not solved yet. Object will not spawn.");
        }
    }
}

