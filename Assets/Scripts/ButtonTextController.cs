using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonTextController : MonoBehaviour
{
    public Button[] buttons; 
    private Color defaultTextColor = Color.black; 
    private Color selectedTextColor = Color.white;
    private Color selectedButtonColor = new Color(72 / 255f, 71 / 255f, 71 / 255f); // color (#484747)

    private Button selectedButton;

    private void Start()
    {
        SelectButton(0);
    }

    public void SelectButton(int buttonIndex)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            TextMeshProUGUI buttonText = buttons[i].GetComponentInChildren<TextMeshProUGUI>();

            if (i == buttonIndex)
            {
                buttonText.color = selectedTextColor;
                SetButtonColor(buttons[i], selectedButtonColor);
                selectedButton = buttons[i];
            }
            else
            {
                buttonText.color = defaultTextColor;
                ResetButtonColor(buttons[i]);
            }
        }
    }

    private void SetButtonColor(Button button, Color color)
    {
        var colors = button.colors;
        colors.normalColor = color; 
        button.colors = colors;
    }

    private void ResetButtonColor(Button button)
    {
        var colors = button.colors;
        colors.normalColor = Color.white; 
        button.colors = colors;
    }
}

