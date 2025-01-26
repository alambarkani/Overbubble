using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SpaceEscapeController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Image stepImage;
    [SerializeField] private List<Sprite> stepSprites;

    [SerializeField] private float totalTime = 10f;

    [Header("UI Settings")]
    [SerializeField] private GameObject failEndUI;
    [SerializeField] private GameObject successEndUI;

    private float timeRemaining;
    private int stepIndex;
    private bool isGameComplete;

    void Start()
    {
        ResetGame();
        PlayerMovement playerMovement = FindFirstObjectByType<PlayerMovement>();
        playerMovement.isInteracting = true;
    }

    void Update()
    {
        if (isGameComplete) return;

        timeRemaining -= Time.deltaTime;
        UpdateTimerDisplay();

        if (timeRemaining <= 0)
        {
            GameOver();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AdvanceStep();
        }
    }

    void AdvanceStep()
    {
        stepIndex++;
        if (stepIndex >= stepSprites.Count)
        {
            WinGame();
            return;
        }

        stepImage.sprite = stepSprites[stepIndex];
    }

    void UpdateTimerDisplay()
    {
        timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
    }

    void WinGame()
    {
        isGameComplete = true;
        UIManager.instance.ShowUI(successEndUI);
        Debug.Log("You Win!");
    }

    void GameOver()
    {
        isGameComplete = true;
        UIManager.instance.ShowUI(failEndUI);
    }

    void ResetGame()
    {
        stepIndex = 0;
        timeRemaining = totalTime;
        isGameComplete = false;
        stepImage.sprite = stepSprites[stepIndex];
    }
}