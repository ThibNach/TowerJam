using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerBuild : MonoBehaviour
{
    [SerializeField]
    protected KeyCode key;
    protected Vector2Int previewPositionGrid;
    public Vector2Int playerPositionGrid;
    public Vector2Int distancePreview;
    protected bool buildingModeEnable;
    protected int layerGround;
    public bool canBuild;

    [SerializeField]
    protected Camera camera;

    public Vector2Int[] neighbor = new Vector2Int[8]{
        Vector2Int.left,
        Vector2Int.left + Vector2Int.up,
        Vector2Int.up,
        Vector2Int.up+Vector2Int.right,
        Vector2Int.right,
        Vector2Int.right + Vector2Int.down,
        Vector2Int.down,
        Vector2Int.down + Vector2Int.left};

    private void Start()
    {
        layerGround = LayerMask.NameToLayer("Ground");
    }
    
    void Update()
    {
        buildingModeEnable = Input.GetKeyDown(key) ? !buildingModeEnable : buildingModeEnable;

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        UpdatePlayerPositionGrid();
        
        if(buildingModeEnable)
        {
            if(Physics.Raycast(ray, out hit, Mathf.Infinity/*, layerGround*/))
            {
                UpdatePreviewPosition(hit.point);

                if(TileIsNeirbhor(previewPositionGrid - playerPositionGrid))
                {
                    BuildingManager.Instance.PlacePreviewOnGrid(previewPositionGrid);
                    
                    if(Input.GetMouseButtonDown(0))
                    {
                        //RessourcesManager.Instance.HaveEnoughtResBuild()
                        BuildingManager.Instance.AddBuildOnTile(previewPositionGrid);
                    }
                }
            }
        
            if(Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                BuildingManager.Instance.SelectNextBuild();
            }
            
            BuildingManager.Instance.ActiveCurrentPreviewBuild(true);
        }
    }

    protected void UpdatePlayerPositionGrid()
    {
        Vector3 positionPlayer = transform.position;
        positionPlayer.x += .5f;
        positionPlayer.x = Mathf.Floor(positionPlayer.x);
        positionPlayer.y = 0;
        positionPlayer.z += .5f;
        positionPlayer.z = Mathf.Floor(positionPlayer.z);
        playerPositionGrid = new Vector2Int((int)positionPlayer.x, (int)positionPlayer.z);
    }

    protected void UpdatePreviewPosition(Vector3 cursorPosition)
    {
        Vector3 previewPosition = cursorPosition;
        previewPosition.x += .5f;
        previewPosition.x = Mathf.Floor(previewPosition.x);
        previewPosition.y = 0;
        previewPosition.z += .5f;
        previewPosition.z = Mathf.Floor(previewPosition.z);

        previewPositionGrid.x = Mathf.FloorToInt(previewPosition.x);
        previewPositionGrid.y = Mathf.FloorToInt(previewPosition.z);
    }

    protected bool TileIsNeirbhor(Vector2Int distance)
    {
        return neighbor.ToList<Vector2Int>().Contains(distance);
    }

    private void OnDrawGizmos()
    {
        if(buildingModeEnable)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    Gizmos.color = new Color(1,1,0, .05f);
                    Gizmos.DrawCube(new Vector3(i, 0, j), new Vector3(1,.1f,1));
                    Gizmos.DrawWireCube(new Vector3(i, 0, j), new Vector3(1,.1f,1));
                    Gizmos.color = new Color(1,0,0, .5f);
                    Gizmos.DrawSphere(new Vector3(i, 0, j), .05f);
                }
            }
        }
            
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(ray.origin, ray.direction*100);
        }
    }
}
