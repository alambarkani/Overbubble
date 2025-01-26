using UnityEngine;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;

public class DialogManager : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private TMP_Text characterName;

    [Header("Typing Effect")]
    [SerializeField] private float typingSpeed = 0.05f;

    private Queue<Dialog> dialogQueue;
    private bool isTyping;
    private Dialog currentDialog;

    // Event that can be dynamically assigned
    public event Action OnDialogEnd;

    public static DialogManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        dialogQueue = new Queue<Dialog>();
    }

    public void StartDialog(Dialog[] dialogs, Action onDialogEndCallback = null)
    {
        // Clear previous end event and assign new one if provided
        OnDialogEnd = null;
        if (onDialogEndCallback != null)
        {
            OnDialogEnd += onDialogEndCallback;
        }

        dialogQueue.Clear();

        foreach (Dialog dialog in dialogs)
        {
            dialogQueue.Enqueue(dialog);
        }

        dialogPanel.SetActive(true);
        DisplayNextDialog();
    }

    public void DisplayNextDialog()
    {
        if (isTyping)
        {
            CompleteCurrentDialog();
            return;
        }

        if (dialogQueue.Count == 0)
        {
            EndDialog();
            return;
        }

        currentDialog = dialogQueue.Dequeue();
        characterName.text = currentDialog.characterName;
        StartCoroutine(TypeText(currentDialog.text));
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogText.text = "";

        foreach (char c in text)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    private void CompleteCurrentDialog()
    {
        StopAllCoroutines();
        dialogText.text = currentDialog.text;
        isTyping = false;
    }

    private void EndDialog()
    {
        dialogPanel.SetActive(false);

        // Invoke the end event
        OnDialogEnd?.Invoke();
        PlayerMovement playerMovement = FindFirstObjectByType<PlayerMovement>();
        playerMovement.isInteracting = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                CompleteCurrentDialog();
            }
            else
            {
                DisplayNextDialog();
            }
        }
    }
}

[System.Serializable]
public class Dialog
{
    public string characterName;
    public string text;
}