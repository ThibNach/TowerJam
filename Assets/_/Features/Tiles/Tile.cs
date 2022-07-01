using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    protected TypeTile type;
    
    [SerializeField]
    protected GameObject prefabTree;

    [SerializeField]
    protected GameObject prefabStone;


    private void Start()
    {
        SpawnRessource();
    }

    protected void SpawnRessource()
    {
        if(type == TypeTile.GATHERABLE)
        {
            int countRessource = Random.Range(0, 4);

            float lp = .5f;

            for (var i = 0; i < countRessource; i++)
            {
                GameObject instance = Instantiate(prefabTree, transform.position, transform.rotation, transform);
                instance.transform.position += new Vector3(Random.Range(-lp, lp), 0, Random.Range(-lp, lp));
                instance.transform.rotation = Quaternion.Euler(Random.Range(-25,25), Random.Range(0,360), Random.Range(-25,25));
                instance.transform.localScale = new Vector3(Random.Range(1, 1.5f), Random.Range(1, 1.5f), Random.Range(1, 1.5f));
                prefabTree.GetComponent<Ressource>().quantityRessource = Random.Range(5, 10);
            }

            countRessource = Random.Range(0, 4);

            for (var i = 0; i < countRessource; i++)
            {
                GameObject instance = Instantiate(prefabStone, transform.position, transform.rotation, transform);
                instance.transform.position += new Vector3(Random.Range(-lp, lp), 0, Random.Range(-lp, lp));
                instance.transform.rotation = Quaternion.Euler(Random.Range(-25,25), Random.Range(0,360), Random.Range(-25,25));
                instance.transform.localScale = new Vector3(Random.Range(1, 1.5f), Random.Range(1, 1.5f), Random.Range(1, 1.5f));
                prefabStone.GetComponent<Ressource>().quantityRessource = Random.Range(5, 10);
            }
        }
    }
}
