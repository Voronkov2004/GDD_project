using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToOutside : MonoBehaviour
{
    public AudioSource audioSource; // Audio source for playing sounds
    public AudioClip itemsPickup;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Add the key to the inventory
            InventoryManager.Instance.AddItem("KitchenLockerPrefab");

            // Play the sound and start the coroutine to delay the scene load
            audioSource.PlayOneShot(itemsPickup);
            StartCoroutine(LoadSceneWithDelay(itemsPickup.length));
        }
    }

    private System.Collections.IEnumerator LoadSceneWithDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Load the new scene
        SceneManager.LoadScene("GameScene");
    }
}
