using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Audio/AudioClipGroup")]
public class AudioClipGroup : ScriptableObject
{

    public float VolumeMin = 1;
    public float VolumeMax = 1;
    public float PitchMin = 1;
    public float PitchMax = 1;
    public float Cooldown = 0.1f;

    public List<AudioClip> Clips;
}

