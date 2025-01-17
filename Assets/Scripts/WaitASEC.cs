using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitASEC : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("CupboardOpened");
        }
    }
}