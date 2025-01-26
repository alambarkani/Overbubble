using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string tooltipMessage; // Pesan tooltip
    public float interactionRange = 3f; // Jarak interaksi

    private Transform playerTransform;
    private bool isHovering = false;

    private Renderer objectRenderer;

    private void Start()
    {
        playerTransform = Camera.main.transform;
        objectRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (isHovering && playerTransform != null)
        {
            CheckDistance();
        }
    }

    private void CheckDistance()
    {
        // Menghitung jarak antara player dan objek
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        // Jika jarak kurang dari jarak interaksi, tampilkan tooltip
        GameObject crosshairCanvas = GameObject.Find("CrosshairCanvas");
        if (distance < interactionRange && crosshairCanvas != null)
        {
            ShowTooltip();
        }
        else
        {
            HideTooltip();
        }
    }

    public abstract void Interact(); // Fungsi yang dipanggil saat interaksi

    private void OnMouseEnter()
    {
        isHovering = true;

    }

    private void OnMouseExit()
    {
        isHovering = false;
        HideTooltip();
    }

    private void ShowTooltip()
    {
        objectRenderer.material.color = Color.yellow;
        TooltipManager.instance.ShowTooltip(tooltipMessage);
    }

    private void HideTooltip()
    {
        objectRenderer.material.color = Color.white;
        TooltipManager.instance.HideTooltip();
    }
}
