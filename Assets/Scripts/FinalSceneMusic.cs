using System.Diagnostics;
using UnityEngine;

public class FinalSceneMusicManager : MonoBehaviour
{
    public AudioClip finalSceneMusic;
    public AudioSource audioSource;

    void Start()
    {
        if (finalSceneMusic != null && audioSource != null)
        {
            audioSource.clip = finalSceneMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

}
