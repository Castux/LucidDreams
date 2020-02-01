using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject FallThreshold;

    public RotateWorldAroundPlayer worldRotater;
    public PlayerProgression playerProg;

    private GameObject currentPlayer;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        if(currentPlayer != null)
        {
            DestroyImmediate(currentPlayer);
        }

        currentPlayer = Instantiate(PlayerPrefab);
        currentPlayer.transform.position = transform.position;

        worldRotater.player = currentPlayer.transform;
    }

    private void Update()
    {
        if (currentPlayer != null && currentPlayer.transform.position.y <= FallThreshold.transform.position.y)
        {
            //Spawn();
            playerProg.SwitchToDayTime();
        }
    }
}
