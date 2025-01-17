using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // Singleton instance
    public static GameStateManager Instance;

    // Tracks the state of puzzles and items
    public bool isTowelPuzzleSolved = false;
    public bool isChestPuzzleSolved = false;
    public bool isTentLOpened = false;

    // Locations
    public bool isKitchenUnlocked = false; // Tracks if the kitchen has been unlocked
    public bool isOriginallyLockerOpened = false;
    public bool isLockerOpened = false;

    // Dictionary to track picked up items
    private Dictionary<string, bool> pickedUpItems = new Dictionary<string, bool>();

    // Tracks player positions by scene
    private Dictionary<string, Vector3> playerPositions = new Dictionary<string, Vector3>();

    private string currentScene;

    void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes

            // Load saved progress
            LoadProgress();
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public void MarkItemPickedUp(string itemId)
    {
        if (!pickedUpItems.ContainsKey(itemId))
        {
            pickedUpItems.Add(itemId, true);
        }
        else
        {
            pickedUpItems[itemId] = true;
        }
        SaveProgress();
    }

    public bool IsItemPickedUp(string itemId)
    {
        return pickedUpItems.ContainsKey(itemId) && pickedUpItems[itemId];
    }

    public void SaveProgress()
    {
        // Save puzzle states
        PlayerPrefs.SetInt("TowelPuzzleSolved", isTowelPuzzleSolved ? 1 : 0);
        PlayerPrefs.SetInt("ChestPuzzleSolved", isChestPuzzleSolved ? 1 : 0);
        PlayerPrefs.SetInt("KitchenUnlocked", isKitchenUnlocked ? 1 : 0);
        PlayerPrefs.SetInt("OriginallyLockerOpened", isOriginallyLockerOpened ? 1 : 0);
        PlayerPrefs.SetInt("LockerOpened", isLockerOpened ? 1 : 0);
        PlayerPrefs.SetInt("TentLOpened", isTentLOpened ? 1 : 0); // Save TentL state

        // Save picked up items
        List<string> savedKeys = new List<string>();
        foreach (var item in pickedUpItems)
        {
            PlayerPrefs.SetInt(item.Key, item.Value ? 1 : 0);
            savedKeys.Add(item.Key);
        }

        // Save the list of keys
        PlayerPrefs.SetString("SavedKeys", string.Join(",", savedKeys));

        // Save current scene and player location
        PlayerPrefs.SetString("CurrentScene", currentScene);

        // Save player positions
        foreach (var kvp in playerPositions)
        {
            string key = $"PlayerPos_{kvp.Key}";
            PlayerPrefs.SetString(key, $"{kvp.Value.x},{kvp.Value.y},{kvp.Value.z}");
        }

        PlayerPrefs.Save();
        Debug.Log("Progress saved.");
    }

    public void LoadProgress()
    {
        // Load puzzle states
        isTowelPuzzleSolved = PlayerPrefs.GetInt("TowelPuzzleSolved", 0) == 1;
        isChestPuzzleSolved = PlayerPrefs.GetInt("ChestPuzzleSolved", 0) == 1;
        isKitchenUnlocked = PlayerPrefs.GetInt("KitchenUnlocked", 0) == 1;
        isOriginallyLockerOpened = PlayerPrefs.GetInt("OriginallyLockerOpened", 0) == 1;
        isLockerOpened = PlayerPrefs.GetInt("LockerOpened", 0) == 1;
        isTentLOpened = PlayerPrefs.GetInt("TentLOpened", 0) == 1; // Load TentL state

        // Load picked up items
        pickedUpItems = new Dictionary<string, bool>();
        string savedKeysString = PlayerPrefs.GetString("SavedKeys", "");
        if (!string.IsNullOrEmpty(savedKeysString))
        {
            string[] savedKeys = savedKeysString.Split(',');
            foreach (string key in savedKeys)
            {
                pickedUpItems[key] = PlayerPrefs.GetInt(key, 0) == 1;
            }
        }

        // Load scene and player position
        currentScene = PlayerPrefs.GetString("CurrentScene", "GameScene");

        // Load player positions
        playerPositions.Clear();
        foreach (string key in PlayerPrefs.GetString("SavedKeys", "").Split(','))
        {
            if (!string.IsNullOrEmpty(key) && PlayerPrefs.HasKey($"PlayerPos_{key}"))
            {
                string[] positionData = PlayerPrefs.GetString($"PlayerPos_{key}").Split(',');
                if (positionData.Length == 3)
                {
                    playerPositions[key] = new Vector3(
                        float.Parse(positionData[0]),
                        float.Parse(positionData[1]),
                        float.Parse(positionData[2])
                    );
                }
            }
        }

        Debug.Log("Progress loaded.");
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        // puzzles
        isTowelPuzzleSolved = false;
        isChestPuzzleSolved = false;
        //locations
        isKitchenUnlocked = false;
        isOriginallyLockerOpened = false;
        isLockerOpened = false;
        isTentLOpened = false;
        //items
        pickedUpItems.Clear();
        //scenes and player locations
        playerPositions.Clear();
        currentScene = "GameScene";
        Debug.Log("Progress reset.");
    }

    public void SavePlayerLocation(string sceneName, Vector3 position)
    {
        playerPositions[sceneName] = position;
        currentScene = sceneName;

        SaveProgress();
        Debug.Log($"Player position saved: Scene = {sceneName}, Position = {position}");
    }

    public Vector3? GetPlayerPosition(string sceneName)
    {
        if (playerPositions.ContainsKey(sceneName))
        {
            return playerPositions[sceneName];
        }
        return null;
    }

    public void LoadPlayerLocation(out string sceneName, out Vector3 position)
    {
        // load location
        sceneName = PlayerPrefs.GetString("CurrentScene", "GameScene"); 
        float x = PlayerPrefs.GetFloat("PlayerPosX", 0f);
        float y = PlayerPrefs.GetFloat("PlayerPosY", 0f);
        float z = PlayerPrefs.GetFloat("PlayerPosZ", 0f);
        position = new Vector3(x, y, z);

        Debug.Log($"Player location loaded: Scene = {sceneName}, Position = {position}");
    }

    public string GetSavedScene()
    {
        return currentScene;
    }
}

