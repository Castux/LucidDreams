using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookCover : MonoBehaviour
{
    public GameObject OpenNotebook;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenBook()
    {
        Debug.Log("Openbook");
        OpenNotebook.SetActive(true);
        gameObject.SetActive(false);
    }
}
