using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;


    void Update()
    {
        // Ambil input mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (Cursor.lockState == CursorLockMode.Locked && !CutsceneManager.Instance.IsCutscenePlaying)
        {
            // Rotasi vertikal kamera
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Batas rotasi agar tidak 360 derajat
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Rotasi horizontal karakter
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
