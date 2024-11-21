using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlsListGenerator : MonoBehaviour
{
    [System.Serializable]
    public struct ControlKey
    {
        public string action;
        public string key;
    }

    public ControlKey[] controls; 
    public GameObject rowPrefab; 

    void Start()
    {
        foreach (var control in controls)
        {
            GameObject row = Instantiate(rowPrefab, transform);
            TextMeshProUGUI[] texts = row.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = control.action;
            texts[1].text = control.key;
        }
    }
}

