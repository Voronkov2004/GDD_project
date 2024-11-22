using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BrightnessApplier : MonoBehaviour
{
    public PostProcessProfile brightnessProfile;
    private AutoExposure exposure;

    void Start()
    {
        brightnessProfile.TryGetSettings(out exposure);

        float savedBrightness = PlayerPrefs.GetFloat("BrightnessValue", 1f);

        if (exposure != null)
        {
            exposure.keyValue.value = savedBrightness;
        }
    }
}
