using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenNotebook : MonoBehaviour
{
    public GameObject LeftPage;
    public GameObject RightPage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlipPage(int direction)
    {
        Debug.Log("Flip page " + direction);
    }
}
