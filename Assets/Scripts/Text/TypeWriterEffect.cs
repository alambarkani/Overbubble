using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.UI;

[Serializable]
public class TextEvent
{
    public string text;
    public UnityEngine.Events.UnityEvent onTextComplete;
}

public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.1f;
    [SerializeField] private float pauseBetweenTexts = 1f;
    [SerializeField] private TextEvent[] textEvents;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip typingSound;

    [SerializeField] private GameObject button;

    private TMP_Text textComponent;
    private int currentTextIndex = 0;
    private bool isTyping;

    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        if (textComponent != null)
        {
            StartTypeText();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isTyping)
        {
            SkipCurrentText();
        }
    }

    public void StartTypeText()
    {
        StartCoroutine(TypeTextCoroutine());
    }

    private IEnumerator TypeTextCoroutine()
    {
        while (currentTextIndex < textEvents.Length)
        {
            isTyping = true;
            TextEvent currentTextEvent = textEvents[currentTextIndex];
            yield return StartCoroutine(TypeSingleText(currentTextEvent.text));

            // Invoke event for this specific text
            currentTextEvent.onTextComplete?.Invoke();

            yield return new WaitForSeconds(pauseBetweenTexts);
            currentTextIndex++;
            textComponent.text = string.Empty;
        }

        isTyping = false;
        button.SetActive(true);
    }

    private IEnumerator TypeSingleText(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            textComponent.text += text[i];
            PlayTypingSound();
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void PlayTypingSound()
    {
        if (typingSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(typingSound);
        }
    }

    private void SkipCurrentText()
    {
        StopAllCoroutines();
        textComponent.text = textEvents[currentTextIndex].text;
        isTyping = false;
        StartCoroutine(ContinueToNextText());
    }

    private IEnumerator ContinueToNextText()
    {
        yield return new WaitForSeconds(pauseBetweenTexts);
        textComponent.text = string.Empty;
        currentTextIndex++;
        StartTypeText();
    }
}