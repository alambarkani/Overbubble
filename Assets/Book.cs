using UnityEngine;

public class Book : Interactable
{
    [Header("UI Settings")]
    [SerializeField] GameObject bookUI;

    public override void Interact()
    {
        Debug.Log("Interaksi dengan buku");
        UIManager.instance.ShowUI(bookUI);
    }
}
