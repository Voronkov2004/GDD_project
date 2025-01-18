using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [TextArea]
    public string noteText; // notes text

    [TextArea]
    public string closeNoteMessage; // message to display after closing the note

    [TextArea]
    public string followUpMessage; // second message to display after the first message

    public bool isFinalNote = false;
}

