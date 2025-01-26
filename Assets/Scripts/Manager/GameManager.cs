using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Settings")]
    [SerializeField] GameObject secretEndUI;
    [SerializeField] GameObject startUI;
    [SerializeField] GameObject playerUI;

    [Header("Dialog Settings")]
    [SerializeField] Dialog[] secretEndDialogs;

    [SerializeField] AudioSource audioSource;

    public int currentChapter = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        currentChapter = 1;
        audioSource.Play();
        CutsceneManager.Instance.PlayCutscene();
    }

    public void SecretEnd()
    {
        DialogManager.Instance.StartDialog(secretEndDialogs, () =>
        {
            UIManager.instance.ShowUI(secretEndUI);
        });
    }

    public void HideChoice(GameObject choiceContainer)
    {
        choiceContainer.SetActive(false);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        audioSource.Stop();
        UIManager.instance.ShowUI(startUI);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowGameObject(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void HideGameObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        UIManager.instance.ShowUI(playerUI);
    }
}