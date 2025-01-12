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

    public void SelectButton(int buttonIndex)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            TextMeshProUGUI buttonText = buttons[i].GetComponentInChildren<TextMeshProUGUI>();

            if (i == buttonIndex)
            {
                buttonText.color = selectedTextColor;
            }
            else
            {
                buttonText.color = defaultTextColor;
            }
        }
    }
}

