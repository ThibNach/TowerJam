using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    Dictionary<Vector2Int, GameObject> tiles = new Dictionary<Vector2Int, GameObject>();
    public GameObject[] builds;
    public GameObject currentBuild;

    public static BuildingManager Instance;

    private void Start()
    {
        Instance = this;
        currentBuild = builds[0];
    }

    public bool AddBuildOnTile(Vector2Int tilePosition)
    {
        bool exist = tiles.ContainsKey(tilePosition);

        if(!exist)
        {
            tiles.Add(tilePosition, currentBuild);
            
            Instantiate(currentBuild, new Vector3(tilePosition.x, 0, tilePosition.y), Quaternion.identity);
        }
        
        return !exist;
    }
}
