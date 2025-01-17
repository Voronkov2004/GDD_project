using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToOutside : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Add the key to the inventory
            InventoryManager.Instance.AddItem("KitchenLockerPrefab");

            // Load the new scene
            SceneManager.LoadScene("GameScene");
        }
    }
}