using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToToilet : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("Toilet");
        }
    }
}