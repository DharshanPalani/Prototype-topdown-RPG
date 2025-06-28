using System.Collections;
using UnityEngine;
using TMPro;
using System;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    public static bool IsDialogueRunning { get; private set; }

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public float textSpeed;

    private bool _isDialogueContinueing;
    private int _index;
    private Action _onDialogueEnd;
    private string[] _currentLines;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (!IsDialogueRunning) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_isDialogueContinueing)
            {
                StopAllCoroutines();
                dialogueText.text = _currentLines[_index];
                _isDialogueContinueing = false;
                return;
            }

            if (dialogueText.text == _currentLines[_index])
            {
                NextLine();
            }
        }
    }

    public void StartDialogue(DialogueScriptableObject dialogue, Action onDialogueEnd = null)
    {
        if (IsDialogueRunning) return;

        IsDialogueRunning = true;
        dialoguePanel.SetActive(true);
        dialogueText.text = "";
        _currentLines = dialogue.lines;
        _index = 0;
        _onDialogueEnd = onDialogueEnd;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        _isDialogueContinueing = true;
        dialogueText.text = "";

        foreach (char c in _currentLines[_index])
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        _isDialogueContinueing = false;
    }

    private void NextLine()
    {
        if (_index < _currentLines.Length - 1)
        {
            _index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialoguePanel.SetActive(false);
            dialogueText.text = "";
            _onDialogueEnd?.Invoke();
            _onDialogueEnd = null;
            IsDialogueRunning = false;
        }
    }
}