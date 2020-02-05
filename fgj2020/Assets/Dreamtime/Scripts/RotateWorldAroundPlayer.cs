using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWorldAroundPlayer : MonoBehaviour
{
    public Transform worldParent;
    public Transform player;

    public float cylinderRotateFactor;
    public float torusRotateFactor;

    private bool cylinderRotateActive = false;
    private bool torusRotateActive = false;
    private List<GameObject> torusRings;

    private Vector3 previousPlayerPosition;

    private void Update()
    {
        if (cylinderRotateActive)
        {
            RotateCylinder();
        }
        else if (torusRotateActive)
        {
            RotateTorus();
        }

        previousPlayerPosition = player.position;
    }

    public void SetCylinderRotationActive(bool activate)
    {
        cylinderRotateActive = activate;
    }

    public void SetTorusRotationActive(bool activate, List<GameObject> torusRings)
    {
        torusRotateActive = activate;
        this.torusRings = torusRings;
    }

    void RotateCylinder()
    {
        worldParent.rotation = Quaternion.Euler(0f, 0f, player.position.x * -cylinderRotateFactor);
        worldParent.position = new Vector3(player.position.x, worldParent.position.y, worldParent.position.z);
    }

    void RotateTorus()
    {
        worldParent.position = new Vector3(player.position.x, worldParent.position.y, player.position.z);
        worldParent.rotation = Quaternion.Euler(0f, 0f, player.position.x * -torusRotateFactor);
        foreach(GameObject torusRing in torusRings)
        {
            torusRing.transform.localRotation *= Quaternion.Euler(0f, (player.position.z - previousPlayerPosition.z) * cylinderRotateFactor, 0f);
        }
    }
}
