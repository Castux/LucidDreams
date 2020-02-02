using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFloatyCubes : MonoBehaviour
{
    public Transform levelParent;
    public GameObject floatyPrefab;
    public int minFloaties;
    public int maxFloaties;

    public float minScale;
    public float maxScale;

    public void SpawnFloaties(float cylinderRadius, float levelLenght)
    {
        int randomCount = Random.Range(minFloaties, maxFloaties + 1);

        for (int i = 0; i < randomCount; i++)
        {
            GameObject floaty = Instantiate(floatyPrefab, levelParent);
            floaty.transform.position = new Vector3(Random.Range(-1f, cylinderRadius * 2f + 1f), Random.Range(-1f, cylinderRadius * 2f + 1f), Random.Range(-levelLenght, levelLenght));

            float randomScale = Random.Range(minScale, maxScale);
            floaty.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        }
    }
}
