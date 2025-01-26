using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager instance;

    public TextMeshProUGUI tooltipText; // Referensi ke Text
    public RectTransform tooltipBackground; // Referensi ke background tooltip (opsional)

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        HideTooltip(); // Sembunyikan tooltip saat start
    }

    private void Update()
    {
        // Pindahkan tooltip ke posisi mouse
        Vector2 mousePosition = Input.mousePosition;
        transform.position = mousePosition;
    }

    public void ShowTooltip(string message)
    {
        tooltipText.text = message;
        tooltipText.gameObject.SetActive(true);

        // Update ukuran background berdasarkan teks
        if (tooltipBackground != null)
        {
            Vector2 textSize = tooltipText.GetPreferredValues(message);
            tooltipBackground.sizeDelta = textSize + new Vector2(10, 10); // Tambahkan padding
            tooltipBackground.gameObject.SetActive(true);
        }
    }

    public void HideTooltip()
    {
        tooltipText.gameObject.SetActive(false);

        if (tooltipBackground != null)
        {
            tooltipBackground.gameObject.SetActive(false);
        }
    }
}
