using UnityEngine;

public class Laptop : Interactable
{
    [Header("UI Settings")]
    [SerializeField] GameObject laptopUI;


    public override void Interact()
    {
        Debug.Log("Interaksi dengan laptop");
        UIManager.instance.ShowUI(laptopUI);
    }
}