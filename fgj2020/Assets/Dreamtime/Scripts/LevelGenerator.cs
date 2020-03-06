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

    public float torusRadius;
    public float cylinderAngleStep;
    public float torusAngleStep;

    void Start()
    {
        List<GameObject> torusRings = GenerateTorusLevel();
        FindObjectOfType<RotateWorldAroundPlayer>().SetTorusRotationActive(true, torusRings);

        // GenerateCylinderLevel();
        // FindObjectOfType<RotateWorldAroundPlayer>().SetCylinderRotationActive(true);

        // if (GetComponent<SpawnFloatyCubes>())
        // {
        //     GetComponent<SpawnFloatyCubes>().SpawnFloaties(cylinderRadius, levelLenght);
        // }
    }

    List<GameObject> GenerateTorusLevel()
    {
        levelParent.position = new Vector3(0f, torusRadius + cylinderRadius);

        List<GameObject> cubeList = new List<GameObject>();
        List<GameObject> torusRings = new List<GameObject>();

        float cylinderSteps = ((2 * Mathf.PI * cylinderRadius) / cubeWidth);
        float torusSteps = ((2 * Mathf.PI * (torusRadius + cylinderRadius)) / cubeWidth);
        cylinderAngleStep = 360f / cylinderSteps;
        torusAngleStep = 360f / torusSteps;

        for (float angle2 = 0; angle2 < 360; angle2 += torusAngleStep)
        {
            float angle2Radians = angle2 * Mathf.Deg2Rad;
            GameObject torusRing = new GameObject("TorusRing");
            torusRing.transform.parent = levelParent;
            torusRing.transform.position = new Vector3(torusRadius * Mathf.Cos(angle2Radians), torusRadius * Mathf.Sin(angle2Radians), 0f) + Vector3.up * (torusRadius + cylinderRadius);
            torusRing.transform.rotation = Quaternion.Euler(0f, 0f, angle2);

            for (float angle = 0; angle < 360; angle += cylinderAngleStep)
            {
                float angleRadians = angle * Mathf.Deg2Rad;
                float x = (torusRadius + cylinderRadius * Mathf.Cos(angleRadians)) * Mathf.Cos(angle2Radians);
                float y = (torusRadius + cylinderRadius * Mathf.Cos(angleRadians)) * Mathf.Sin(angle2Radians);
                float z = cylinderRadius * Mathf.Sin(angleRadians);

                if (Random.Range(0, 1f) < randomGapChance)
                {
                    continue;
                }

                Vector3 position = new Vector3(x, y, z) + Vector3.up * (torusRadius + cylinderRadius);

                GameObject cube = Instantiate(cubePrefab, torusRing.transform);

                cube.transform.position = position;
                cube.transform.localRotation = Quaternion.Euler(0f, -angle, 90f);

                Vector3 scale = cube.transform.localScale;
                scale.y = Random.Range(1f, 8f);
                cube.transform.localScale = scale;

                cubeList.Add(cube);
            }
            torusRings.Add(torusRing);
        }

        GenerateCluesTorus(cubeList);

        // Create a cube under player start

        int playerTorusRingIndex = Mathf.RoundToInt(torusSteps / 4);
        GameObject torusRingUnderPlayer = torusRings[playerTorusRingIndex];
        GameObject startCube = Instantiate(cubePrefab, torusRingUnderPlayer.transform);

        startCube.transform.position = Vector3.zero;

        Vector3 startCubeScale = startCube.transform.localScale;
        startCubeScale.y = 8f;
        startCube.transform.localScale = startCubeScale;

        return torusRings;
    }

    void GenerateCluesTorus(List<GameObject> cubeList)
    {
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

            GameObject clueObject = Instantiate(cluePrefab, randomCube.transform.parent);
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

            GameObject clueObject = Instantiate(cluePrefab, randomCube.transform.parent);
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

        GenerateCluesCylinder(cubeList);

        // Create a cube under player start
        GameObject startCube = Instantiate(cubePrefab, levelParent);

        startCube.transform.position = Vector3.zero;

        Vector3 startCubeScale = startCube.transform.localScale;
        startCubeScale.y = 8f;
        startCube.transform.localScale = startCubeScale;
    }

    void GenerateCluesCylinder(List<GameObject> cubeList)
    {
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
