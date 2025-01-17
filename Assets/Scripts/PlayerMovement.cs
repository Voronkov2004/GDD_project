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

    public AudioSource actionAudioSource;
    public AudioSource walkingAudioSource;
    public AudioMixerGroup soundMixerGroup;

    public AudioClipGroup walkingInsideSounds;
    public AudioClipGroup walkingOnGrassSounds;
    public AudioClipGroup walkingOnSandSounds;

    private float stepCooldown = 0.5f; 
    private float nextStepTime = 0f;


    private string currentSurface = "Grass";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        actionAudioSource.outputAudioMixerGroup = soundMixerGroup;
        walkingAudioSource.outputAudioMixerGroup = soundMixerGroup;
    }

    void Update()
    {
        // info from keyboard
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        bool isWalking = movement.x != 0 || movement.y != 0;
        animator.SetBool("isWalking", isWalking);


        if(isWalking)
        {
            PlayStepSound();
        } else
        {
            if (walkingAudioSource.isPlaying)
            {
                walkingAudioSource.Stop();
            }
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
        if (Time.time >= nextStepTime && !walkingAudioSource.isPlaying)
        {
            nextStepTime = Time.time + stepCooldown;

            if (currentSurface == "Grass")
            {
                AudioClip clip = walkingOnGrassSounds.Clips[Random.Range(0, walkingOnGrassSounds.Clips.Count)];
                walkingAudioSource.PlayOneShot(clip);
            }
            else if (currentSurface == "Wood")
            {
                AudioClip clip = walkingInsideSounds.Clips[Random.Range(0, walkingInsideSounds.Clips.Count)];
                walkingAudioSource.PlayOneShot(clip);
            }
            else if (currentSurface == "Sand")
            {
                AudioClip clip = walkingOnSandSounds.Clips[Random.Range(0, walkingOnSandSounds.Clips.Count)];
                walkingAudioSource.PlayOneShot(clip);
            }
            walkingAudioSource.loop = false; 
            walkingAudioSource.Play();
        }
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
        else if (collision.CompareTag("Sand"))
        {
            currentSurface = "Sand";
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Grass") || collision.CompareTag("Wood") || collision.CompareTag("Sand"))
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
