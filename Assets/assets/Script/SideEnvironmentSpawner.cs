using UnityEngine;

public class SideEnvironmentSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] batuPrefabs;
    public GameObject[] pohonPrefabs;

    [Header("Spawn Area")]
    public float minZ = -8f;
    public float maxZ = 8f;

    public float leftX = -6f;
    public float rightX = 6f;

    [Header("Batu Settings")]
    public int batuCount = 3;
    public float minBatuDistance = 3f;

    [Header("Pohon Cluster Settings")]
    public int clusterCount = 2;
    public int minTreePerCluster = 4;
    public int maxTreePerCluster = 5;
    public float clusterRadius = 2f;

    void Start()
    {
        SpawnBatu();
        SpawnTreeClusters();
    }

    // ================= BATU =================
    void SpawnBatu()
    {
        Vector3 lastPos = Vector3.zero;

        for (int i = 0; i < batuCount; i++)
        {
            Vector3 spawnPos;
            int attempt = 0;

            do
            {
                float x = Random.value > 0.5f ? leftX : rightX;
                float z = Random.Range(minZ, maxZ);
                spawnPos = new Vector3(x, 0, z);
                attempt++;
            }
            while (Vector3.Distance(spawnPos, lastPos) < minBatuDistance && attempt < 10);

            lastPos = spawnPos;

            GameObject batu = Instantiate(
                batuPrefabs[Random.Range(0, batuPrefabs.Length)],
                transform
            );

            batu.transform.localPosition = spawnPos;
            batu.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            batu.transform.localScale *= Random.Range(0.9f, 1.1f);
        }
    }

    // ================= POHON =================
    void SpawnTreeClusters()
    {
        for (int c = 0; c < clusterCount; c++)
        {
            float baseX = Random.value > 0.5f ? leftX : rightX;
            float baseZ = Random.Range(minZ, maxZ);

            int treeCount = Random.Range(minTreePerCluster, maxTreePerCluster + 1);

            for (int i = 0; i < treeCount; i++)
            {
                Vector2 offset = Random.insideUnitCircle * clusterRadius;

                Vector3 spawnPos = new Vector3(
                    baseX + offset.x,
                    0,
                    baseZ + offset.y
                );

                GameObject tree = Instantiate(
                    pohonPrefabs[Random.Range(0, pohonPrefabs.Length)],
                    transform
                );

                tree.transform.localPosition = spawnPos;
                tree.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                tree.transform.localScale *= Random.Range(0.95f, 1.1f);
            }
        }
    }
}
