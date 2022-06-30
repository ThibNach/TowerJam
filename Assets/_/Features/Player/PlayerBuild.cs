using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerBuild : MonoBehaviour
{
    [SerializeField]
    protected KeyCode key;

    [SerializeField]
    protected GameObject tilePreview;

    protected Vector3 position;
    protected Vector3 positionHit;
    public float wheel;
    
    public bool buildingModeEnable;
    protected int layerGround;

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
        Vector3 distance;

        if(buildingModeEnable)
        {
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                distance = transform.position - hit.point;
                
                if(TileIsNeirbhor(new Vector2Int((int)distance.x, (int)distance.z)))
                {
                    position = hit.point;
                    positionHit = hit.point;
                    position.x += .5f;
                    position.x = Mathf.Floor(position.x);
                    position.y = 0;
                    position.z += .5f;
                    position.z = Mathf.Floor(position.z);
                    tilePreview.transform.position = position;

                    if(Input.GetMouseButtonDown(0))
                    {
                        //BuildingManager.Instance.AddBuildOnTile(new Vector2Int((int)position.x, (int)position.z));
                    }
                }
            }

            
        }
        wheel = Input.GetAxis("Mouse ScrollWheel");
        tilePreview.active = buildingModeEnable;
    }

    protected bool TileIsNeirbhor(Vector2Int distance)
    {
        // Debug.Log(distance);
        // Debug.Log(neighbor);
        return neighbor.ToList<Vector2Int>().Contains(distance);
    }

    private void OnDrawGizmos()
    {
        if(buildingModeEnable)
        {
            for (int i = -10; i < 10; i++)
            {
                for (int j = -10; j < 10; j++)
                {
                    Gizmos.color = new Color(1,1,0, .05f);
                    Gizmos.DrawCube(new Vector3(i, 0, j), new Vector3(1,.1f,1));
                    Gizmos.DrawWireCube(new Vector3(i, 0, j), new Vector3(1,.1f,1));
                    Gizmos.color = new Color(1,0,0, .5f);
                    Gizmos.DrawSphere(new Vector3(i, 0, j), .05f);
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
}
