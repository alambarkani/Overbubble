using UnityEngine;
using UnityEngine.UI;

public class Bed : Interactable
{
    [Header("Dialog Settings")]
    [SerializeField] Dialog[] bedDialogs;

    [SerializeField] GameObject choiceContainer;

    public override void Interact()
    {
        Debug.Log("Interaksi dengan tempat tidur");
        DialogManager.Instance.StartDialog(bedDialogs, () =>
        {
            choiceContainer.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        });
    }
}