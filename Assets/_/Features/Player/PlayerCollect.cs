using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    [SerializeField]
    protected Camera camera;
    [SerializeField]
    protected float distanceGather;
    [SerializeField]
    protected float currentTime;
    public float durationPick;
    protected bool enableTimeout;

    private void Update()
    {
        PickRessource();

        if(enableTimeout)
        {
            if(currentTime > durationPick)
            {
                currentTime = 0;
                enableTimeout = false;
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
    }

    protected void PickRessource()
    {
        RaycastHit hit = default(RaycastHit);
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] raycastHits = Physics.RaycastAll(ray, Mathf.Infinity);

        foreach (var currentHit in raycastHits)
        {
            if(currentHit.collider.CompareTag("Gatherable"))
            {
                hit = currentHit;
                break;
            }
        }

        if(hit.collider)
        {
            Collider collider = hit.collider;

            if(collider && collider.CompareTag("Gatherable"))
            {
                bool v = Input.GetMouseButtonDown(0);
                if(v && !enableTimeout)
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanceGather);
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
    }
}