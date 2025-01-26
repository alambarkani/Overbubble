using UnityEngine;

public class Smartphone : Interactable
{
    [Header("UI Settings")]
    [SerializeField] GameObject smartphoneUI;


    [Header("Dialog Settings")]
    [SerializeField] Dialog[] smartphoneDialogs;

    public override void Interact()
    {
        if (GameManager.Instance.currentChapter == 1)
        {
            DialogManager.Instance.StartDialog(smartphoneDialogs);
            Debug.Log("Interaksi dengan smartphone");
        }
        else
        {
            UIManager.instance.ShowUI(smartphoneUI);
        }
    }
}
