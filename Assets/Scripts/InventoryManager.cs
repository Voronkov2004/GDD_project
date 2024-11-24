using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<string> collectedTags = new List<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddItem(string tag)
    {
        if (!collectedTags.Contains(tag))
        {
            collectedTags.Add(tag);
        }
    }

    public void RemoveItem(string tag)
    {
        if (collectedTags.Contains(tag))
        {
            collectedTags.Remove(tag);
        }
    }

    public bool HasItem(string tag)
    {
        return collectedTags.Contains(tag);
    }
}
