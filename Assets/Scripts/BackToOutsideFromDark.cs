using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToOutsideFromDark : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}