using UnityEngine;

public class TowelPuzzle : MonoBehaviour
{
    public Hook[] hooks; // References to the hooks
    public string[] correctOrder; // Correct towel order
    public GameObject keyPrefab; // Prefab of the key
    public Transform keySpawnPoint; // Where the key will spawn

    private bool puzzleSolved = false; // Prevents re-checking once solved

    void Update()
    {
        // Only check if the puzzle is not yet solved
        if (!puzzleSolved && IsPuzzleSolved())
        {
            Debug.Log("Puzzle solved!");

            GameStateManager.Instance.isPuzzleSolved = true;

            // Spawn the key
            SpawnKey();

            // Disable further puzzle checking
            puzzleSolved = true;

            // Disable all hooks
            DisableHooks();
        }
    }

    private bool IsPuzzleSolved()
    {
        Debug.Log("Checking the puzzle solution...");

        for (int i = 0; i < correctOrder.Length; i++)
        {
            Debug.Log($"Checking hook {i}: expected {correctOrder[i]}, current towel: {hooks[i].currentTowel}");

            if (hooks[i].currentTowel != correctOrder[i])
            {
                Debug.Log($"Mismatch on hook {i}: found {hooks[i].currentTowel}, expected {correctOrder[i]}.");
                return false;
            }
        }

        Debug.Log("All towels are correctly placed!");
        return true;
    }

    private void SpawnKey()
    {
        if (keyPrefab != null && keySpawnPoint != null)
        {
            // Instantiate the key at the specified spawn point
            Instantiate(keyPrefab, keySpawnPoint.position, Quaternion.identity);
            Debug.Log("Key spawned at the specified location.");
        }
        else
        {
            Debug.LogError("Key prefab or spawn point is not set!");
        }
    }

    private void DisableHooks()
    {
        foreach (Hook hook in hooks)
        {
            hook.enabled = false; // Disable the hook script
        }
        Debug.Log("All hooks are now disabled.");
    }

}