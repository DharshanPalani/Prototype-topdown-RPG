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
    private bool _isDialogueContinueing;

    private int _index;

    private Action _onDialogueEnd;

    

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
            if (_isDialogueContinueing)
            {
                StopAllCoroutines();
                dialogueText.text = "";
                dialogueText.text = _currentLines[_index];
                _isDialogueContinueing = false;
                return;
            }
            if (dialogueText.text == _currentLines[_index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                _dialoguestarted = false;
                dialogueText.text = _currentLines[_index];

            }
        }
    }

    public void StartDialogue(DialogueScriptableObject dialogue, Action onDialogueEnd)
    {
        _dialoguestarted = true;
        dialoguePanel.SetActive(true);
        _currentLines = dialogue.lines;
        _index = 0;
        _onDialogueEnd = onDialogueEnd;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        foreach (char c in _currentLines[_index].ToCharArray())
        {
            dialogueText.text += c;
            _isDialogueContinueing = true;
            yield return new WaitForSeconds(textSpeed);
        }

        _isDialogueContinueing = false;

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
            dialogueText.text = "";
            dialoguePanel.SetActive(false);
            _onDialogueEnd?.Invoke();
            _onDialogueEnd = null;
        }
    }
}
