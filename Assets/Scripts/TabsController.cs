using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabsController : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject controlsPanel;
    public GameObject audioPanel;

    public void ShowSettings()
    {
        settingsPanel.SetActive(true);
        controlsPanel.SetActive(false);
        audioPanel.SetActive(false);
    }

    public void ShowControls()
    {
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(true);
        audioPanel.SetActive(false);
    }

    public void ShowAudio()
    {
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        audioPanel.SetActive(true);
    }
}
