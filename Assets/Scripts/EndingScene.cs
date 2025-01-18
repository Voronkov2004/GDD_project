using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryScene : MonoBehaviour
{
    public TextMeshProUGUI Text1;
    public TextMeshProUGUI Text2;
    public TextMeshProUGUI Text3;
    public TextMeshProUGUI Text4;
    public TextMeshProUGUI Text5;
    public TextMeshProUGUI Text6;
    public TextMeshProUGUI Text7;
    public TextMeshProUGUI Text8;
    public TextMeshProUGUI Text9;
    public TextMeshProUGUI PressF;
    public Image backgroundForText;
    public float fadeDuration = 1f;

    public Image Anna_1;
    public Image Anna_2;

    private int currentTextIndex = 0;

    void Start()
    {
        Text1.alpha = 0f;
        Text2.alpha = 0f;
        Text3.alpha = 0f;
        Text4.alpha = 0f;
        Text5.alpha = 0f;
        Text6.alpha = 0f;
        Text7.alpha = 0f;
        Text8.alpha = 0f;
        Text9.alpha = 0f;
        PressF.alpha = 0f;
        Anna_1.color = new Color(1f, 1f, 1f, 0f);
        Anna_2.color = new Color(1f, 1f, 1f, 0f);

        backgroundForText.color = new Color32(42, 61, 76, 175);

        Color c = backgroundForText.color;
        c.a = 0f;
        backgroundForText.color = c;

        StartCoroutine(ShowFirstImageAndText());
    }

    IEnumerator ShowFirstImageAndText()
    {
        yield return new WaitForSeconds(fadeDuration);

        StartCoroutine(FadeIn(Anna_1));

        yield return new WaitForSeconds(fadeDuration + 2f);

        StartCoroutine(FadeIn(Text1));
        StartCoroutine(FadeIn(PressF));
        StartCoroutine(FadeIn(backgroundForText));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(SwitchText());
        }
    }

    IEnumerator SwitchText()
    {
        TextMeshProUGUI textToHide = null;
        switch (currentTextIndex)
        {
            case 0:
                textToHide = Text1;
                break;
            case 1: textToHide = Text2; break;
            case 2: textToHide = Text3; break;
            case 3: textToHide = Text4; break;
            case 4:
                textToHide = Text5;
                StartCoroutine(SwitchBackground());
                StartCoroutine(FadeOut(PressF));
                StartCoroutine(FadeOut(backgroundForText));
                break;
            case 5: textToHide = Text6; break;
            case 6: textToHide = Text7; break;
            case 7: textToHide = Text8; break;
            case 8:
                textToHide = Text9;
                StartCoroutine(FadeOut(Anna_2));
                break;
            case 9:
                textToHide = PressF;
                break;
        }

        if (textToHide != null)
        {
            yield return StartCoroutine(FadeOut(textToHide));
        }

        currentTextIndex++;

        TextMeshProUGUI textToShow = null;

        switch (currentTextIndex)
        {
            case 1: textToShow = Text2; break;
            case 2: textToShow = Text3; break;
            case 3: textToShow = Text4; break;
            case 4: textToShow = Text5; break;
            case 5:
                yield return new WaitForSeconds(2f);
                StartCoroutine(FadeIn(PressF));
                StartCoroutine(FadeIn(backgroundForText));
                textToShow = Text6;
                break;
            case 6: textToShow = Text7; break;
            case 7: textToShow = Text8; break;
            case 8: textToShow = Text9; break;
            case 9:
                textToHide = PressF;
                StartCoroutine(FadeOut(backgroundForText));
                SceneManager.LoadScene("MainMenu");
                break;
        }

        if (textToShow != null)
        {
            yield return StartCoroutine(FadeIn(textToShow));
        }

    }

    IEnumerator SwitchBackground()
    {
        StartCoroutine(FadeOut(Anna_1));
        StartCoroutine(FadeIn(Anna_2));
        yield return null;
    }

    IEnumerator FadeIn(TextMeshProUGUI text)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            text.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            yield return null;
        }
        text.alpha = 1f;

    }

    IEnumerator FadeOut(TextMeshProUGUI text)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            text.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;

        }
        text.alpha = 0f;

    }

    IEnumerator FadeIn(Image image)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            Color c = image.color;
            c.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            image.color = c;
            yield return null;
        }
    }

    IEnumerator FadeOut(Image image)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            Color c = image.color;
            c.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            image.color = c;
            yield return null;
        }
    }

}