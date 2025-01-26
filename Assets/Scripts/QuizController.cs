using UnityEngine;
using TMPro;

public class QuizController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TMP_InputField answerInput;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("UI Settings")]
    [SerializeField] private GameObject failEndUI;
    [SerializeField] private GameObject successEndUI;

    private readonly string[] questions = {
        "Berapa 5 + 5?",
        "Ibu kota Indonesia?",
        "Nama presiden pertama Indonesia?"
    };

    private readonly string[] correctAnswers = {
        "10",
        "jakarta",
        "soekarno"
    };

    private int currentQuestionIndex = 0;
    private float timeRemaining = 10f;

    void Start()
    {
        ResetQuiz();
    }

    void Update()
    {
        if (currentQuestionIndex < questions.Length)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();

            if (timeRemaining <= 0)
            {
                // Alternate Ending
                UIManager.instance.ShowUI(failEndUI);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                CheckAnswer();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
    }

    void CheckAnswer()
    {
        string playerAnswer = answerInput.text.ToLower().Trim();

        if (playerAnswer == correctAnswers[currentQuestionIndex])
        {
            NextQuestion();
        }
        else
        {
            Debug.Log("Wrong answer!");
            answerInput.text = "";
        }
        answerInput.text = "";
    }

    void NextQuestion()
    {
        currentQuestionIndex++;

        if (currentQuestionIndex < questions.Length)
        {
            questionText.text = questions[currentQuestionIndex];
            timeRemaining = 10f;
            answerInput.ActivateInputField();
        }
        else
        {
            EndQuiz();
        }
    }

    void EndQuiz()
    {
        answerInput.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        questionText.gameObject.SetActive(false);
        GameManager.Instance.currentChapter++;
        UIManager.instance.ShowUI(successEndUI);
    }

    void ResetQuiz()
    {
        currentQuestionIndex = 0;
        timeRemaining = 10f;
        questionText.text = questions[currentQuestionIndex];
        answerInput.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        answerInput.ActivateInputField();
    }
}