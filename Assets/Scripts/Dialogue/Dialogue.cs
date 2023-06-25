using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

[Serializable]
public struct DialogueData
{
    public string name;
    [TextArea(3,12)]
    public List<string> body;
}

public class Dialogue : MonoBehaviour
{
    public int pageNumber = 0;
    public DialogueData _dialogueData;
    public Animator animator;

    public TMP_Text nameText;
    public TMP_Text bodyText;
    public Button continueButton;
    [SerializeField] AudioSource _buttonSound;

    public void DisplayDialogue(DialogueData dialogueData)
    {
        animator.SetBool("isOpen", true);
        _dialogueData = dialogueData;
        pageNumber = 0;
        nameText.text = dialogueData.name;
        bodyText.text = dialogueData.body[pageNumber];
    }

    // If a player press the button the dialogue continues the next page.
    public void NextPage()
    {
        if (pageNumber + 1 < _dialogueData.body.Count)
        {
            pageNumber++;
            _buttonSound.Play();
        }
        else
        {
            if (continueButton != null)
            {
                continueButton.interactable = false;
                animator.SetBool("isOpen", false);
            }
        }
        bodyText.text = _dialogueData.body[pageNumber];
    }
}
