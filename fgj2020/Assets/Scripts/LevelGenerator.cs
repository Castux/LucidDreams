using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public float cubeWidth;

    public int squareLevelWidth;

    public int cylinderCubesPerCircle;
    public int cylinderRadius;

    public float randomGapChance;

    public int levelLenght;

    public Transform levelParent;

    void Start()
    {
        GenerateCylinderLevel();
        FindObjectOfType<RotateWorldAroundPlayer>().SetRotationActive(true);
    }

    void GenerateSquareLevel()
    {
        for (int y = -levelLenght; y <= levelLenght; y++)
        {
            for (int x = -squareLevelWidth / 2; x <= squareLevelWidth / 2; x++)
            {
                GameObject cube = Instantiate(cubePrefab, levelParent);
                cube.transform.position = new Vector3(x * cubeWidth, 0f, y * cubeWidth);
                Vector3 scale = cube.transform.localScale;
                scale.y = Random.Range(1f, 8f);
                cube.transform.localScale = scale;
            }
        }
    }

    void GenerateCylinderLevel()
    {
        levelParent.position = new Vector3(0f, cylinderRadius);
        if (cylinderCubesPerCircle == 0)
        {
            Debug.LogError("0 cylinder cubes per circle, set in inspector");
        }

        for (int z = -levelLenght; z <= levelLenght; z++)
        {
            for (int x = 0; x <= 360; x += 360 / cylinderCubesPerCircle)
            {
                if (Random.Range(0, 1f) < randomGapChance)
                {
                    continue;
                }

                Vector3 position = new Vector3(cylinderRadius * Mathf.Cos(Mathf.Deg2Rad * x), cylinderRadius + cylinderRadius * Mathf.Sin(Mathf.Deg2Rad * x), z * cubeWidth);

                GameObject cube = Instantiate(cubePrefab, levelParent);

                cube.transform.position = position;
                cube.transform.rotation = Quaternion.Euler(0f, 0f, x + 90);

                Vector3 scale = cube.transform.localScale;
                scale.y = Random.Range(1f, 8f);
                cube.transform.localScale = scale;
            }
        }
    }
}
