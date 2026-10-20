using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Tile Prefabs")]
    [SerializeField] private GameObject startTile;
    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private GameObject questionTile;

    [Header("Settings")]
    [SerializeField] private int startTileCount = 5;
    [SerializeField] private int poolSize = 12;
    [SerializeField] private float tileLength = 8f;
    [SerializeField] private float moveSpeed = 4f;

    [Header("Waves")]
    [SerializeField] private int minObstacleTiles = 5;
    [SerializeField] private int maxObstacleTiles = 8;
    [SerializeField] private int safeTileCount = 4;

    private readonly Queue<GameObject> activeTiles = new();
    private float nextSpawnX;

    private int tilesRemainingInWave;
    private bool inSafeGap;
    private int safeTilesSpawned;

    private void Start()
    {
        nextSpawnX = 0f;

        for (int i = 0; i < startTileCount; i++)
        {
            SpawnTile(startTile);
        }

        while (activeTiles.Count < poolSize)
        {
            SpawnTile(GetRandomPrefab());
        }

        StartNewObstacleWave();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsPlaying())
            return;

        MoveTiles();
        RecycleTiles();
    }

    private void MoveTiles()
    {
        foreach (GameObject tile in activeTiles)
        {
            tile.transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }

    private void RecycleTiles()
    {
        GameObject firstTile = activeTiles.Peek();

        if (firstTile.transform.position.x > player.position.x + tileLength)
        {
            firstTile = activeTiles.Dequeue();
            Destroy(firstTile);

            if (inSafeGap)
            {
                if (safeTilesSpawned == safeTileCount / 2)
                    SpawnTile(questionTile);
                else
                    SpawnTile(startTile);

                safeTilesSpawned++;

                if (safeTilesSpawned >= safeTileCount)
                    StartNewObstacleWave();
            }
            else
            {
                SpawnTile(GetRandomPrefab());

                tilesRemainingInWave--;

                if (tilesRemainingInWave <= 0)
                    StartSafeGap();
            }
        }
    }

    private void SpawnTile(GameObject prefab)
    {
        GameObject tile = Instantiate(prefab, transform);

        tile.transform.position = new Vector3(nextSpawnX, 0f, 0f);

        activeTiles.Enqueue(tile);

        nextSpawnX -= tileLength;
    }

    private GameObject GetRandomPrefab()
    {
        return tilePrefabs[Random.Range(0, tilePrefabs.Length)];
    }

    private void StartNewObstacleWave()
    {
        inSafeGap = false;
        tilesRemainingInWave = Random.Range(minObstacleTiles, maxObstacleTiles + 1);
    }

    private void StartSafeGap()
    {
        inSafeGap = true;
        safeTilesSpawned = 0;
    }
}