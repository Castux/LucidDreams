using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject cubePrefab;

    public float cubeWidth;

    public int squareLevelWidth;

    private int cylinderCubesPerCircle;
    public int cylinderRadius;

    public float randomGapChance;

    public int levelLenght;

    public Transform levelParent;

    public GameObject cluePrefab;
    public int clueCount;

    public int minRealCluesPerLevel;
    public List<Clue> clueList;
    public List<Clue> fakeClueList;

    void Start()
    {
        GenerateCylinderLevel();
        FindObjectOfType<RotateWorldAroundPlayer>().SetRotationActive(true);
    }

    void GenerateCylinderLevel()
    {
        if (cylinderRadius <= 0)
        {
            Debug.LogError("cylinder radius must be over 0, set in inspector");
        }

        cylinderCubesPerCircle = Mathf.RoundToInt((2 * Mathf.PI * cylinderRadius) / cubeWidth);

        levelParent.position = new Vector3(0f, cylinderRadius);
        if (cylinderCubesPerCircle == 0)
        {
            Debug.LogError("Cylinder radius too small/ cubes too wide for radius so no cubes spawned");
        }

        List<GameObject> cubeList = new List<GameObject>();

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

                cubeList.Add(cube);
            }
        }
        
        int realCluesSpawned = 0;
        for (int i = 0; i < minRealCluesPerLevel && i < clueCount && cubeList.Count > 0 && clueList.Count > 0; i++)
        {
            if (cubeList.Count == 0 || clueList.Count == 0)
            {
                Debug.LogError("Can't generate all clues");
                break;
            }

            GameObject randomCube = cubeList[Random.Range(0, cubeList.Count)];

            Clue randomClue = clueList[Random.Range(0, clueList.Count)];

            GameObject clueObject = Instantiate(cluePrefab, levelParent);
            clueObject.transform.position = randomCube.transform.position;
            clueObject.transform.rotation = randomCube.transform.rotation;
            clueObject.transform.position += randomCube.transform.up * (randomCube.transform.localScale.y / 2 + 1);

            clueObject.GetComponent<DreamClue>().clueData = randomClue;

            clueList.Remove(randomClue);
            cubeList.Remove(randomCube);

            realCluesSpawned++;
        }

        List<Clue> allClues = new List<Clue>();
        allClues.AddRange(clueList);
        allClues.AddRange(fakeClueList);

        for (int i = 0; i < clueCount - realCluesSpawned; i++)
        {
            GameObject randomCube = cubeList[Random.Range(0, cubeList.Count)];

            Clue randomClue = allClues[Random.Range(0, allClues.Count)];

            GameObject clueObject = Instantiate(cluePrefab, levelParent);
            clueObject.transform.position = randomCube.transform.position;
            clueObject.transform.rotation = randomCube.transform.rotation;
            clueObject.transform.position += randomCube.transform.up * (randomCube.transform.localScale.y / 2 + 1);

            clueObject.GetComponent<DreamClue>().clueData = randomClue;

            if (clueList.Contains(randomClue))
            {
                clueList.Remove(randomClue);
            }
            if (fakeClueList.Contains(randomClue))
            {
                fakeClueList.Remove(randomClue);
            }

            allClues.Remove(randomClue);
            cubeList.Remove(randomCube);
        }

        // Create a cube under player start
        GameObject startCube = Instantiate(cubePrefab, levelParent);

        startCube.transform.position = Vector3.zero;

        Vector3 startCubeScale = startCube.transform.localScale;
        startCubeScale.y = 8f;
        startCube.transform.localScale = startCubeScale;
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
}
