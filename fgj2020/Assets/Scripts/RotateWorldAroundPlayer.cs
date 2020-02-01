using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWorldAroundPlayer : MonoBehaviour
{
    public Transform worldParent;
    public Transform player;

    public float worldRotateFactor;

    private bool rotateActive = false;

    private void Update()
    {
        if (rotateActive)
        {
            worldParent.rotation = Quaternion.Euler(0f, 0f, player.position.x * -worldRotateFactor);
            worldParent.position = new Vector3(player.position.x, worldParent.position.y, worldParent.position.z);
        }
    }

    public void SetRotationActive(bool activate)
    {
        rotateActive = activate;
    }
}
