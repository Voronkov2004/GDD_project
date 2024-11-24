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
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(string tag)
    {
        collectedTags.Add(tag);
    }

    public void RemoveItem(string tag)
    {
        collectedTags.Remove(tag);
    }

    public bool HasItem(string tag)
    {
        return collectedTags.Contains(tag);
    }
}
