using UnityEngine;

public class Bag : Interactable
{
    public override void Interact()
    {
        gameObject.SetActive(false);
    }
}
