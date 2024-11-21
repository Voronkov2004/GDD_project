using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class GlobalBrightnessController : MonoBehaviour
{
    public Slider brightnessSlider;
    public PostProcessVolume postProcessVolume; 

    private ColorGrading colorGrading; 

    void Start()
    {
        postProcessVolume.profile.TryGetSettings(out colorGrading);

        brightnessSlider.onValueChanged.AddListener(SetBrightness);
        SetBrightness(brightnessSlider.value);
    }

    public void SetBrightness(float value)
    {
        float exposureValue = Mathf.Lerp(-2f, 2f, value / 100f);
        colorGrading.postExposure.value = exposureValue;
    }
}
