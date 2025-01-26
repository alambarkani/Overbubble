using System.Collections;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;

    public bool IsCutscenePlaying { get; set; } = false;

    [Header("TV Static Dialog")]
    [SerializeField] private Dialog[] tvStaticDialogs;

    [Header("After TV Static Dialog")]
    [SerializeField] private Dialog[] afterTVStaticDialogs;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PlayCutscene()
    {
        IsCutscenePlaying = true;
    }

    public void StopCutscene()
    {
        IsCutscenePlaying = false;
    }

    public void StaticTVScene()
    {
        // Play static TV scene
        StartCoroutine(HeadRotate(-120));
        DialogManager.Instance.StartDialog(tvStaticDialogs, () =>
        {
            StartCoroutine(HeadRotate(120));
            DialogManager.Instance.StartDialog(afterTVStaticDialogs, () =>
            {
                StopCutscene();
                PlayerMovement playerMovement = FindFirstObjectByType<PlayerMovement>();
                Camera.main.transform.position = playerMovement.headTransform.position;
            });
        });
    }

    IEnumerator HeadRotate(float rotationAngle)
    {
        // Rotate camera 90 degrees over time
        float duration = 1f; // Rotation duration in seconds
        Quaternion startRotation = Camera.main.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, rotationAngle, 0);

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            Camera.main.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final rotation is exactly the target rotation
        Camera.main.transform.rotation = endRotation;
    }

}
