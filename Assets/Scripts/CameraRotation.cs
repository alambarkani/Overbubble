using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    [SerializeField] private InputActionReference lookAction;


    void Update()
    {
        var lookInput = lookAction.action.ReadValue<Vector2>();

        if (Cursor.lockState == CursorLockMode.Locked && !CutsceneManager.Instance.IsCutscenePlaying)
        {
            rotationX -= lookInput.y * mouseSensitivity; // Atas-Bawah (Pitch)
            rotationY += lookInput.x * mouseSensitivity; // Kiri-Kanan (Yaw)

            // 4. (Opsional) Batasi rotasi vertikal agar tidak jungkir balik
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            // 5. Terapkan hasil akumulasi
            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
        }
    }
}
