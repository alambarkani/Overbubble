using UnityEngine;

public class BookController : MonoBehaviour
{
    [SerializeField] GameObject[] bookPages;
    private int currentPage = 0;

    public void NextPage()
    {
        if (currentPage < bookPages.Length - 1)
        {
            bookPages[currentPage].SetActive(false);
            currentPage++;
            bookPages[currentPage].SetActive(true);
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            bookPages[currentPage].SetActive(false);
            currentPage--;
            bookPages[currentPage].SetActive(true);
        }
    }

}