using UnityEngine;
using UnityEngine.UI;

public class SubtitlesToggle : MonoBehaviour
{
    public Button buttonOn; //ON button
    public Button buttonOff; // OFF button

    public Color activeColor = Color.gray; 
    public Color inactiveColor = Color.white; 

    private bool subtitlesEnabled = true; 

    void Start()
    {
        buttonOn.onClick.AddListener(SetSubtitlesOn);
        buttonOff.onClick.AddListener(SetSubtitlesOff);

        UpdateButtonColors();
    }

    public void SetSubtitlesOn()
    {
        subtitlesEnabled = true; 
        UpdateButtonColors();
    }

    public void SetSubtitlesOff()
    {
        subtitlesEnabled = false; 
        UpdateButtonColors();
    }

    private void UpdateButtonColors()
    {
        if (subtitlesEnabled)
        {
            // ON is active
            buttonOn.GetComponent<Image>().color = activeColor;
            buttonOff.GetComponent<Image>().color = inactiveColor;
        }
        else
        {
            // OFF is active
            buttonOn.GetComponent<Image>().color = inactiveColor;
            buttonOff.GetComponent<Image>().color = activeColor;
        }
    }
}
