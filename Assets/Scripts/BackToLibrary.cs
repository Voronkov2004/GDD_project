using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToLibrary : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("Library");
        }
    }
}