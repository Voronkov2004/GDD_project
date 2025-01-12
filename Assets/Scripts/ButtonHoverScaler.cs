using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 defaultScale; 
    public float hoverScaleFactor = 1.1f; 
    public float animationSpeed = 0.1f; 

    void Start()
    {
        defaultScale = transform.localScale; 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines(); 
        StartCoroutine(ScaleTo(defaultScale * hoverScaleFactor));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines(); 
        StartCoroutine(ScaleTo(defaultScale));
    }

    private System.Collections.IEnumerator ScaleTo(Vector3 targetScale)
    {
        while (Vector3.Distance(transform.localScale, targetScale) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, animationSpeed);
            yield return null;
        }
        transform.localScale = targetScale; 
    }
}


