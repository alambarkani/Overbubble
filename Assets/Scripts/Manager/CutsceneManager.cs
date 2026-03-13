using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;

    public bool IsCutscenePlaying { get; set; } = false;

    [Header("List of Dialog")]
    [SerializeField] private List<DialogSO> dialogs;


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

}
