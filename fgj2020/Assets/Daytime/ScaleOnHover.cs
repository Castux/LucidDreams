using UnityEngine;

public class ScaleOnHover : MonoBehaviour
{
    public void OnMouseOver()
    {
        transform.localScale = Vector3.one * 1.1f;
    }

    public void OnMouseExit()
    {
        transform.localScale = Vector3.one;
    }
}
