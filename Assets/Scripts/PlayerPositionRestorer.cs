using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositionRestorer : MonoBehaviour
{
    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Vector3? savedPosition = GameStateManager.Instance.GetPlayerPosition(currentScene);

        if (savedPosition.HasValue)
        {
            Transform playerTransform = FindObjectOfType<PlayerInteraction>().transform;
            playerTransform.position = savedPosition.Value;
            Debug.Log($"Player position restored in {currentScene}: {savedPosition.Value}");
        }
        else
        {
            Debug.Log($"No saved position for scene {currentScene}. Using default position.");
        }
    }
}

