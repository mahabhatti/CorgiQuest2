using UnityEngine;

public class BiomeSpawn : MonoBehaviour
{
    public BiomeState biome;

    void Start()
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.RegisterSpawnPoint(biome, transform);
        }
    }
}

