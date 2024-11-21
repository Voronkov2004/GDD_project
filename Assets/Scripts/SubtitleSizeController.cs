using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleSizeController : MonoBehaviour
{
    public Button smallButton;  
    public Button mediumButton; 
    public Button largeButton;  

    public Color activeColor = Color.gray; 
    public Color inactiveColor = Color.white; 

    private string subtitleSize = "MEDIUM"; 

    void Start()
    {
        smallButton.onClick.AddListener(SetSmall);
        mediumButton.onClick.AddListener(SetMedium);
        largeButton.onClick.AddListener(SetLarge);

        UpdateButtonColors();
    }

    public void SetSmall()
    {
        subtitleSize = "SMALL";
        UpdateButtonColors();
    }

    public void SetMedium()
    {
        subtitleSize = "MEDIUM";
        UpdateButtonColors();
    }

    public void SetLarge()
    {
        subtitleSize = "LARGE";
        UpdateButtonColors();
    }

    private void UpdateButtonColors()
    {
        smallButton.GetComponent<Image>().color = subtitleSize == "SMALL" ? activeColor : inactiveColor;
        mediumButton.GetComponent<Image>().color = subtitleSize == "MEDIUM" ? activeColor : inactiveColor;
        largeButton.GetComponent<Image>().color = subtitleSize == "LARGE" ? activeColor : inactiveColor;
    }
}
