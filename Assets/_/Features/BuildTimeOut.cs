using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTimeOut : MonoBehaviour
{
    [SerializeField]
    protected float currentTime;
    [SerializeField]
    protected float timeout;

    private void Start()
    {
        //timeout = GetComponent<BaseTower>()._buildingTime;
        GetComponent<BaseTower>().enabled = false;
    }

    void Update()
    {
        if(currentTime > timeout)
        {
            GetComponent<BaseTower>().enabled = true;
            Destroy(this);
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }
}
