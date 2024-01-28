using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
    [SerializeField] private int width = 513;
    [SerializeField] private int height = 513;
    [SerializeField] private int depth = 20;
    [SerializeField] private int scale = 20;

    private void Start() {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData data) {
        data.heightmapResolution = width + 1;
        data.size = new Vector3(width, depth, height);
        data.SetHeights(0, 0, GenerateHeights());
        return data;
    }

    float[,] GenerateHeights() {
        float[,] heights = new float[width, height];
        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        return heights;
    }

    float CalculateHeight(int x, int y) {
        float xCoord = (float) x / width * scale;
        float yCoord = (float) y / height * scale;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
