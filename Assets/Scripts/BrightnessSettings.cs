using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class BrightnessSettings : MonoBehaviour
{
    public static BrightnessSettings instance;

    public Slider brightnessSlider;
    public PostProcessProfile brightnessProfile;
    //public PostProcessLayer layer;

    AutoExposure exposure;
    private void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            brightnessProfile.TryGetSettings(out exposure);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //brightness.TryGetSettings(out exposure);
        ////AdjustBrightness(brightnessSlider.value);
        //float savedBrightness = PlayerPrefs.GetFloat("BrightnessValue", 1f);
        //brightnessSlider.value = savedBrightness;
        //AdjustBrightness(savedBrightness);

        float savedBrightness = PlayerPrefs.GetFloat("BrightnessValue", 1f);
        if (brightnessSlider != null)
        {
            brightnessSlider.value = savedBrightness;
        }
        AdjustBrightness(savedBrightness);
    }

    public void AdjustBrightness(float value)
    {
        //if(value !=0)
        //{
        //    exposure.keyValue.value = value;
        //}
        //else
        //{
        //    exposure.keyValue.value = .05f;
        //}

        if (exposure != null)
        {
            exposure.keyValue.value = value;
            PlayerPrefs.SetFloat("BrightnessValue", value);
        }
    }
}

