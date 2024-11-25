using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    // Dictionary to store item quantities
    public Dictionary<string, int> collectedItems = new Dictionary<string, int>();

    // Dictionary to store player positions by scene name
    private Dictionary<string, Vector3> playerPositions = new Dictionary<string, Vector3>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Inventory methods
    public void AddItem(string tag)
    {
        if (collectedItems.ContainsKey(tag))
        {
            collectedItems[tag]++;
        }
        else
        {
            collectedItems[tag] = 1;
        }
    }

    public void RemoveItem(string tag)
    {
        if (collectedItems.ContainsKey(tag))
        {
            collectedItems[tag]--;
            if (collectedItems[tag] <= 0)
            {
                collectedItems.Remove(tag);
            }
        }
    }

    public bool HasItem(string tag)
    {
        return collectedItems.ContainsKey(tag) && collectedItems[tag] > 0;
    }

    public int GetItemCount(string tag)
    {
        return collectedItems.ContainsKey(tag) ? collectedItems[tag] : 0;
    }

    // Position management methods
    public void SavePlayerPosition(string sceneName, Vector3 position)
    {
        if (playerPositions.ContainsKey(sceneName))
        {
            playerPositions[sceneName] = position;
        }
        else
        {
            playerPositions.Add(sceneName, position);
        }
    }

    public Vector3? GetPlayerPosition(string sceneName)
    {
        if (playerPositions.ContainsKey(sceneName))
        {
            return playerPositions[sceneName];
        }
        return null;
    }
}
