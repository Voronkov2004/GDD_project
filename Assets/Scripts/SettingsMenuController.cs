using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    public CanvasGroup[] panels;
    public ButtonTextController buttonTextController;
    private int activePanelIndex = 0; 

    public void SwitchPanel(int panelIndex)
    {
        if (panelIndex == activePanelIndex) return;

        StartCoroutine(FadeOut(panels[activePanelIndex]));

        activePanelIndex = panelIndex;
        StartCoroutine(FadeIn(panels[panelIndex]));

        buttonTextController.SelectButton(panelIndex);
    }

    private System.Collections.IEnumerator FadeOut(CanvasGroup panel)
    {
        float duration = 0.1f; 
        float startAlpha = panel.alpha;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            panel.alpha = Mathf.Lerp(startAlpha, 1, time / duration);
            yield return null;
        }

        panel.alpha = 0;
        panel.interactable = false;
        panel.blocksRaycasts = false;
        panel.gameObject.SetActive(false);
    }

    private System.Collections.IEnumerator FadeIn(CanvasGroup panel)
    {
        float duration = 0.1f; 
        float startAlpha = panel.alpha;
        panel.gameObject.SetActive(true);
        panel.interactable = true;
        panel.blocksRaycasts = true;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            panel.alpha = Mathf.Lerp(startAlpha, 1, time / duration);
            yield return null;
        }

        panel.alpha = 1;
    }
}

