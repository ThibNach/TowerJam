using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourcesManager : MonoBehaviour
{
    [SerializeField]
    protected RessourceData ressourceA;
    protected RessourceData ressourceB;

    public static RessourcesManager Instance;

    private void Start()
    {
        Instance = this;
    }

    public void IncreaseRessourceA(float value)
    {
        ressourceA.value += value;
    }

    public void IncreaseRessourceB(float value)
    {
        ressourceB.value += value;
    }

    public bool HaveEnoughtResBuild(AbstractStructure structure)
    {
        return structure.costRessourceA <= ressourceA.value && structure.costRessourceB <= ressourceB.value;
    }
}
