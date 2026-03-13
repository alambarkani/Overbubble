using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 12f;
    private CharacterController characterController;

    [Header("Interaction Settings")]
    public float interactionRange = 3f; // Jarak interaksi
    public LayerMask interactableLayer; // Layer objek yang bisa diinteraksi

    public bool isInteracting = false;
    public bool isSitting = false;

    [Header("UI Settings")]
    [SerializeField] private GameObject pauseUI;

    [SerializeField]
    private InputActionReference moveAction;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        var moveInput = moveAction.action.ReadValue<Vector2>();
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        if (!CutsceneManager.Instance.IsCutscenePlaying && !isInteracting && !isSitting)
        {
            // rb.MovePosition(rb.position + speed * Time.deltaTime * move);
            characterController.Move(move * speed * Time.deltaTime);
            // Memanggil fungsi interaksi
            HandleInteraction();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.instance.ShowUI(pauseUI);
            Time.timeScale = 0;
        }
    }

    void HandleInteraction()
    {
        // Jika tombol interaksi ditekan (default: "E")
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hit, interactionRange, interactableLayer))
            {
                Debug.Log("Interaksi");
                // Panggil fungsi interaksi pada objek
                if (hit.collider.TryGetComponent<Interactable>(out var interactable))
                {
                    isInteracting = true;
                    interactable.Interact();
                }
            }
        }
    }

    public void QuitInteraction()
    {
        isInteracting = false;
    }
}
