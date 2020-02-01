using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenNotebook : MonoBehaviour
{
    public GameObject LeftPage;
    public GameObject RightPage;

    public GameObject LeftArrow;
    public GameObject RightArrow;

    public GameObject NotebookCover;

    private int currentPage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        gameObject.SetActive(true);
        currentPage = 0;
    }

    public void FlipPage(int direction)
    {
        currentPage += direction;

        if (currentPage < 0)
        {
            Close();
        }
    }

    private void Close()
    {
        NotebookCover.SetActive(true);
        gameObject.SetActive(false);
    }
}
