using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToKitchen : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("Kitchen");
        }
    }
}