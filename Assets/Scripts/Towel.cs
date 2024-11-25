using UnityEngine;

public class Towel : MonoBehaviour
{
    public string towelColor;

    void Start()
    {
        if (string.IsNullOrEmpty(towelColor))
        {
            Debug.LogWarning("Towel color not set for " + gameObject.name);
        }
    }
}
