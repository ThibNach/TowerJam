using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    [SerializeField]
    protected float currentTime;
    public float timeout;
    public bool enableTimeout;
    [SerializeField]
    protected Camera camera;
    [SerializeField]
    protected float distanceGather;

    private void Update()
    {
        PickRessource();

        if(enableTimeout)
        {
            if(currentTime > timeout)
            {
                currentTime = 0;
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
    }

    protected void PickRessource()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Collider collider = hit.collider;

            if(collider && collider.CompareTag("Gatherable"))
            {
                if(Input.GetMouseButtonDown(0) && !enableTimeout)
                {
                    enableTimeout = true;
                    if(Vector3.Distance(transform.position, collider.transform.position) < distanceGather)
                    {
                        RessourceData data = collider.GetComponent<Ressource>().data;
                        float valueRessource = collider.GetComponent<Ressource>().PickRessource();
                        
                        if(data.name == "wood")
                        {
                            RessourcesManager.Instance.IncreaseRessourceA(valueRessource);
                        }
                        
                        if(data.name == "stone")
                        {
                            RessourcesManager.Instance.IncreaseRessourceB(valueRessource);
                        }
                    }
                }
            }
        }
    }
}