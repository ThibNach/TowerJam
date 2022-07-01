using UnityEngine;

public class Ressource : MonoBehaviour
{
    [SerializeField]
    public RessourceData data;
    public float quantityRessource;
    public float valuePick;

    public float PickRessource()
    {
        quantityRessource -= valuePick;
        return valuePick;
    }

    private void Update()
    {
        if(quantityRessource <= 0)
        {
            DestroyRessource();
        }
    }

    protected void DestroyRessource()
    {
        Destroy(gameObject);
    }
}