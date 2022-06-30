using UnityEngine;

public class Ressource : MonoBehaviour
{
    [SerializeField]
    public RessourceData data;
    public float valueRessource;
    public float valuePick;

    public float PickRessource()
    {
        valueRessource -= valuePick;
        return valuePick;
    }

    private void Update()
    {
        if(valueRessource <= 0)
        {
            DestroyRessource();
        }
    }

    protected void DestroyRessource()
    {
        Destroy(gameObject);
    }
}