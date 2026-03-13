using UnityEngine;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject currentUI;

    [SerializeField] private GameObject mainMenuUI;
    [Header("Dialog Initial")]
    // [SerializeField] Dialog[] initialDialogs;

    private bool isFirstTime = true;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        currentUI = mainMenuUI;
    }

    public void ShowUI(GameObject ui)
    {
        if (currentUI != null)
        {
            HideUI(currentUI);
        }
        currentUI = ui;

        if (ui.CompareTag("PlayerUI"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            PlayerMovement playerMovement = FindFirstObjectByType<PlayerMovement>();
            playerMovement.isInteracting = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
        ui.SetActive(true);

        if (isFirstTime && ui.CompareTag("PlayerUI"))
        {
            isFirstTime = false;
            // DialogManager.Instance.StartDialog(initialDialogs, () =>
            // {
            //     CutsceneManager.Instance.StaticTVScene();
            // });
        }
    }

    public void HideUI(GameObject ui)
    {

        ui.SetActive(false);
    }

    public void HideUI()
    {
        if (currentUI != null)
        {
            currentUI.SetActive(false);
        }
    }
}