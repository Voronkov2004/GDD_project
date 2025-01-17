using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    // Dictionary to store item quantities
    public Dictionary<string, int> collectedItems = new Dictionary<string, int>();

    //// Dictionary to store player positions by scene name
    //private Dictionary<string, Vector3> playerPositions = new Dictionary<string, Vector3>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
            LoadInventory();
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

        SaveInventory();
        Debug.Log($"Item added to inventory: {tag}. Total: {collectedItems[tag]}");
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

        SaveInventory();
        Debug.Log($"Item removed from inventory: {tag}. Remaining: {collectedItems.GetValueOrDefault(tag, 0)}");
    }

    // Check if item exists in inventory
    public bool HasItem(string tag)
    {
        return collectedItems.ContainsKey(tag) && collectedItems[tag] > 0;
    }

    public int GetItemCount(string tag)
    {
        return collectedItems.ContainsKey(tag) ? collectedItems[tag] : 0;
    }

    // Save inventory to PlayerPrefs
    private void SaveInventory()
    {
        foreach (var item in collectedItems)
        {
            PlayerPrefs.SetInt($"Inventory_{item.Key}", item.Value);
        }

        PlayerPrefs.SetString("SavedInventoryKeys", string.Join(",", collectedItems.Keys));
        PlayerPrefs.Save();
        Debug.Log("Inventory saved.");
    }

    // Load inventory from PlayerPrefs
    private void LoadInventory()
    {
        collectedItems.Clear();

        string savedKeys = PlayerPrefs.GetString("SavedInventoryKeys", "");
        if (!string.IsNullOrEmpty(savedKeys))
        {
            string[] keys = savedKeys.Split(',');
            foreach (string key in keys)
            {
                int count = PlayerPrefs.GetInt($"Inventory_{key}", 0);
                if (count > 0)
                {
                    collectedItems[key] = count;
                }
            }
        }

        Debug.Log("Inventory loaded.");
    }

    // Position management methods
    //public void SavePlayerPosition(string sceneName, Vector3 position)
    //{
    //    if (playerPositions.ContainsKey(sceneName))
    //    {
    //        playerPositions[sceneName] = position;
    //    }
    //    else
    //    {
    //        playerPositions.Add(sceneName, position);
    //    }
    //}

    //public Vector3? GetPlayerPosition(string sceneName)
    //{
    //    if (playerPositions.ContainsKey(sceneName))
    //    {
    //        return playerPositions[sceneName];
    //    }
    //    return null;
    //}
}
