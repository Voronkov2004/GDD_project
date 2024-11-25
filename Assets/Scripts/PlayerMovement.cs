using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // movement speed

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public AudioSource audioSource;
    public AudioClip grassStepSound;
    public AudioClip woodStepSound;
    public AudioMixerGroup soundMixerGroup;

    private string currentSurface = "Grass";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        audioSource.outputAudioMixerGroup = soundMixerGroup;
    }

    void Update()
    {
        // info from keyboard
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        bool isWalking = movement.x != 0 || movement.y != 0;
        animator.SetBool("isWalking", isWalking);


        if (isWalking && !audioSource.isPlaying) 
        {
            PlayStepSound();
        }

        if (movement.x != 0)
        {
            spriteRenderer.flipX = movement.x < 0;
        }

        if (Time.timeScale == 0f)
            return;
    }

    void PlayStepSound()
    {
        if (currentSurface == "Grass")
        {
            audioSource.clip = grassStepSound;
        }
        else if (currentSurface == "Wood")
        {
            audioSource.clip = woodStepSound;
        }
        audioSource.Play();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Grass"))
        {
            currentSurface = "Grass";
        }
        else if (collision.CompareTag("Wood"))
        {
            currentSurface = "Wood";
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Grass") || collision.CompareTag("Wood"))
        {
            currentSurface = "Grass";
        }
    }


    void FixedUpdate()
    {
        // move the character
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
