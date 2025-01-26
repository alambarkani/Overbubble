using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UITo3DTransition : MonoBehaviour
{
    [Header("Kamera dan Posisi")]
    [SerializeField] private Transform uiViewPosition; // Posisi kamera untuk UI
    [SerializeField] private Transform worldViewPosition; // Posisi kamera untuk 3D view
    [SerializeField] private Camera mainCamera; // Kamera utama

    [Header("Fade UI")]
    [SerializeField] private CanvasGroup uiCanvas; // Canvas Group untuk UI
    [SerializeField] private float fadeDuration = 0.5f; // Durasi fade

    [Header("Transisi Kamera")]
    [SerializeField] private float cameraMoveSpeed = 2f; // Kecepatan kamera bergerak
    [SerializeField] private Button closeUIButton; // Tombol untuk menutup UI

    private bool isTransitioning = false; // Status transisi

    private void Start()
    {
        // Set posisi kamera ke UI view saat memulai
        mainCamera.transform.SetPositionAndRotation(uiViewPosition.position, uiViewPosition.rotation);

        // Tambahkan listener ke tombol close
        closeUIButton.onClick.AddListener(StartTransition);
    }

    private void StartTransition()
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionTo3D());
        }
    }

    private IEnumerator TransitionTo3D()
    {
        isTransitioning = true;

        // Fade out UI
        yield return StartCoroutine(FadeOutUI());

        // Pindahkan kamera ke posisi world view
        yield return StartCoroutine(MoveCamera(worldViewPosition));

        // Nonaktifkan UI canvas setelah selesai
        UIManager.instance.HideUI();

        isTransitioning = false;
    }

    private IEnumerator FadeOutUI()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            uiCanvas.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        uiCanvas.alpha = 0f;
        uiCanvas.interactable = false;
    }

    private IEnumerator MoveCamera(Transform targetPosition)
    {
        while (Vector3.Distance(mainCamera.transform.position, targetPosition.position) > 0.01f)
        {
            // Lerp posisi dan rotasi kamera

            mainCamera.transform.SetPositionAndRotation(Vector3.Lerp(
                mainCamera.transform.position,
                targetPosition.position,
                Time.deltaTime * cameraMoveSpeed
            ), Quaternion.Lerp(
                mainCamera.transform.rotation,
                targetPosition.rotation,
                Time.deltaTime * cameraMoveSpeed
            ));
            yield return null;
        }

        // Pastikan posisi kamera sesuai target
        mainCamera.transform.SetPositionAndRotation(targetPosition.position, targetPosition.rotation);
    }
}
