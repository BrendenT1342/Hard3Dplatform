using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("GUI reference")]
    [Tooltip("Canva with dialogue script attached")]
    [SerializeField]
    private Transform _dialogueGUI;
    [SerializeField]
    private DialogueText currentDialogue;

    private void Start()
    {
        Dialogue dialogue = _dialogueGUI.GetComponent<Dialogue>();
        dialogue.DisplayDialogue(currentDialogue.text);
    }
}
