using UnityEngine;

public class Ressource : MonoBehaviour
{
    [SerializeField]
    public RessourceData data;
    public float quantityRessource;
    public float valuePick;
    protected float currentTime;
    protected float timeoutAnimationPick = .5f;
    protected Vector3 originalPosition;
    protected bool startAnim;

    private void Start()
    {
        originalPosition = transform.position;
    }

    public float PickRessource()
    {
        quantityRessource -= valuePick;
        startAnim = true;
        return valuePick;
    }

    private void Update()
    {
        if(startAnim)
        {
            if(currentTime > timeoutAnimationPick)
            {
                startAnim = false;
                currentTime = 0;
                transform.position = originalPosition;

                if(quantityRessource <= 0)
                {
                    DestroyRessource();
                }   
            }
            else
            {
                Vector3 position = transform.position;
                float l = .01f;
                position.x += Random.Range(-l,l);
                position.z += Random.Range(-l,l);
                transform.position = position;
                currentTime += Time.deltaTime;
            }
        }
    }

    protected void DestroyRessource()
    {
        Destroy(gameObject);
    }
}