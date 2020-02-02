using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatToRandomDirection : MonoBehaviour
{
    public float maxSpeed;
    public float minSpeed;

    Vector3 randomDirection;
    float randomSpeed;

    private void Start()
    {
        randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        randomSpeed = Random.Range(minSpeed, maxSpeed);

        GetComponentInChildren<Animator>().speed = randomSpeed;
    }

    private void Update()
    {
        transform.position += randomDirection * Time.deltaTime * randomSpeed;
    }
}
