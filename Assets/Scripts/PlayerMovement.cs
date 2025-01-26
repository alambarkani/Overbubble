using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform cameraTransform;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 12f;
    Rigidbody rb;

    [Header("Interaction Settings")]
    public float interactionRange = 3f; // Jarak interaksi
    public LayerMask interactableLayer; // Layer objek yang bisa diinteraksi

    public bool isInteracting = false;
    public bool isSitting = false;

    public Transform headTransform;
    public Transform sitTransform;

    [Header("UI Settings")]
    [SerializeField] private GameObject pauseUI;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Input movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (!CutsceneManager.Instance.IsCutscenePlaying && !isInteracting && !isSitting)
        {
            rb.MovePosition(rb.position + speed * Time.deltaTime * move);
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
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, interactionRange, interactableLayer))
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

    void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(cameraTransform.position, cameraTransform.forward * interactionRange);
    }

    public void QuitInteraction()
    {
        isInteracting = false;
    }
}
