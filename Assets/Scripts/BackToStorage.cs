using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToStorage : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("Storage");
        }
    }
}