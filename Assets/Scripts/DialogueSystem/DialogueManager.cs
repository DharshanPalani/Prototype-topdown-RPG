using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public float textSpeed;

    private bool _dialoguestarted;

    private int _index;


    public DialogueScriptableObject dialogue;

    private string[] _currentLines;

    void Start()
    {
        dialogueText.text = string.Empty;
        // StartDialogue(dialogue);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _dialoguestarted) {
            if (dialogueText.text == _currentLines[_index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                _dialoguestarted = false;
                dialogueText.text = _currentLines[_index];
                _currentLines = null;
            }
        }
    }

    public void StartDialogue(DialogueScriptableObject dialogue)
    {
        _dialoguestarted = true;
        dialoguePanel.SetActive(true);
        _currentLines = dialogue.lines;
        _index = 0;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        foreach (char c in _currentLines[_index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        yield return null;
    }

    private void NextLine()
    {
        if (_index < _currentLines.Length - 1)
        {
            _index += 1;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }
}
