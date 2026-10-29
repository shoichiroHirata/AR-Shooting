using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [Header("Target Prefabs")]
    [SerializeField] private GameObject yellowPrefab;
    [SerializeField] private GameObject redPrefab;
    [SerializeField] private GameObject greenPrefab;

    [Header("Spawn Probability")]
    [SerializeField] private float yellowRate = 0.6f;
    [SerializeField] private float redRate = 0.2f;
    [SerializeField] private float greenRate = 0.2f;

    [Header("Spawn Settings")]
    [SerializeField] private int maxTargetCount = 5;
    [SerializeField] private float spawnInterval = 2f;

    [Header("Spawn Area")]
    [SerializeField] private Transform player;
    [SerializeField] private float minDistance = 1.5f;
    [SerializeField] private float maxDistance = 3.0f;
    [SerializeField] private float minAngle = -80f;
    [SerializeField] private float maxAngle = 80f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            SpawnTarget();
        }
    }

    private void SpawnTarget()
    {
        int currentCount =
            FindObjectsByType<Target>(
                FindObjectsSortMode.None).Length;

        if (currentCount >= maxTargetCount)
        {
            return;
        }

        GameObject prefab = SelectTargetPrefab();

        Vector3 spawnPosition = GetRandomSpawnPosition();

        Instantiate(
            prefab,
            spawnPosition,
            Quaternion.identity);
    }

    private GameObject SelectTargetPrefab()
    {
        float value = Random.value;

        if (value < yellowRate)
        {
            return yellowPrefab;
        }

        if (value < yellowRate + redRate)
        {
            return redPrefab;
        }

        return greenPrefab;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float angle =
            Random.Range(minAngle, maxAngle);

        float distance =
            Random.Range(minDistance, maxDistance);

        Vector3 direction =
            Quaternion.Euler(0, angle, 0) *
            player.forward;

        Vector3 position =
            player.position +
            direction * distance;

        position.y = player.position.y;

        return position;
    }
}