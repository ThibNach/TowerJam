using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    Dictionary<Vector2Int, GameObject> tiles = new Dictionary<Vector2Int, GameObject>();
    public GameObject[] builds;
    public GameObject[] previewBuilds;
    public int indexCurrentBuild;
    public static BuildingManager Instance;

    private void Start()
    {
        Instance = this;
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

    public void SelectNextBuild()
    {
        indexCurrentBuild = (indexCurrentBuild + 1)%builds.Length;
    }

    public void ActiveCurrentPreviewBuild(bool active)
    {
        foreach (var preview in previewBuilds)
        {
            preview.SetActive(false);
        }
        currentPreviewBuild.SetActive(active);
    }

    public void PlacePreviewOnGrid(Vector2Int position)
    {
        currentPreviewBuild.transform.position = new Vector3(position.x, 0, position.y);
    }

    public GameObject currentBuild
    {
        get
        {
            return builds[indexCurrentBuild];
        }
    }

    public GameObject currentPreviewBuild
    {
        get
        {
            return previewBuilds[indexCurrentBuild];
        }
    }
}
