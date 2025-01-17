using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirrorInteraction : MonoBehaviour
{
    public GameObject mirrorCanvas; // Canvas displaying the mirror UI
    public Image mirrorImage; // Image for the zoomed-in mirror
    public Image steamImage; // Image for the steam effect
    public Image dimBackground; // Dimmed background
    public float steamAnimationDuration = 2f; // Duration of the steam animation

    private bool isNearMirror = false; // Tracks if the player is near the mirror
    private bool hasSeenSteam = false; // Tracks if the steam animation has been shown

    void Start()
    {
        // Ensure the UI is initially disabled
        if (mirrorCanvas != null)
            mirrorCanvas.SetActive(false);

        if (steamImage != null)
            steamImage.gameObject.SetActive(false);

        if (dimBackground != null)
            dimBackground.gameObject.SetActive(false);
    }

    void Update()
    {
        // Check for interaction when near the mirror
        if (isNearMirror && Input.GetKeyDown(KeyCode.F))
        {
            ShowMirrorView();
        }
    }

    private void ShowMirrorView()
    {
        if (mirrorCanvas != null)
            mirrorCanvas.SetActive(true);

        if (dimBackground != null)
            dimBackground.gameObject.SetActive(true);

        if (hasSeenSteam)
        {
            // If steam has already been shown, display it immediately
            if (steamImage != null)
                steamImage.gameObject.SetActive(true);
        }
        else
        {
            // If steam hasn't been shown, start the animation
            StartCoroutine(AnimateSteamEffect());
        }

        // Disable player movement
        DisablePlayerMovement();
    }

    private IEnumerator AnimateSteamEffect()
    {
        if (steamImage != null)
        {
            steamImage.gameObject.SetActive(true);

            // Animate the steam image from bottom to top
            RectTransform steamRect = steamImage.GetComponent<RectTransform>();
            Vector2 originalSize = steamRect.sizeDelta;
            steamRect.sizeDelta = new Vector2(originalSize.x, 0);

            float elapsedTime = 0f;

            while (elapsedTime < steamAnimationDuration)
            {
                elapsedTime += Time.deltaTime;
                float progress = Mathf.Clamp01(elapsedTime / steamAnimationDuration);
                steamRect.sizeDelta = new Vector2(originalSize.x, originalSize.y * progress);
                yield return null;
            }

            steamRect.sizeDelta = originalSize;

            // Mark that the steam animation has been shown
            hasSeenSteam = true;

            // Save the state in GameStateManager
            GameStateManager.Instance.hasSeenMirrorSteam = true;
        }
    }

    private void DisablePlayerMovement()
    {
        // Example code to disable player movement
        PlayerMovement movement = GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.enabled = false;
        }
    }

    private void EnablePlayerMovement()
    {
        // Example code to enable player movement
        PlayerMovement movement = GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.enabled = true;
        }
    }

    public void CloseMirrorView()
    {
        if (mirrorCanvas != null)
            mirrorCanvas.SetActive(false);

        if (dimBackground != null)
            dimBackground.gameObject.SetActive(false);

        if (!hasSeenSteam && steamImage != null)
            steamImage.gameObject.SetActive(false);

        EnablePlayerMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mirror"))
        {
            isNearMirror = true;
            Debug.Log("Press F to interact.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Mirror"))
        {
            isNearMirror = false;
            Debug.Log("Player left the mirror.");
        }
    }
}

