using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    public TextMeshProUGUI Text1;
    public TextMeshProUGUI Text2;
    public TextMeshProUGUI PressF;
    public float fadeDuration = 1f;

    private bool isFirstTextShown = true;

    void Start()
    {
        Text2.alpha = 0f;
        PressF.alpha = 0f;
        StartCoroutine(FadeIn(Text1));
        StartCoroutine(FadeIn(PressF));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isFirstTextShown)
            {
                StartCoroutine(SwitchText());
                isFirstTextShown = false;
            }
            else
            {
                StartCoroutine(FadeOut(Text2));
                StartCoroutine(FadeOut(PressF));
                StartCoroutine(LoadNextScene());
            }
        }
    }

    IEnumerator SwitchText()
    {
        yield return StartCoroutine(FadeOut(Text1));
        yield return StartCoroutine(FadeIn(Text2));
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadScene("GameScene");
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
}