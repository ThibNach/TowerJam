using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        Process();
    }

    public void Process()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach(var tile in tiles)
        {
            Vector2Int pos = new Vector2Int(Mathf.FloorToInt(tile.transform.position.x), Mathf.FloorToInt(tile.transform.position.y));
            BuildingManager.Instance.AddTile(pos, tile);
        };
    }
}