using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourcesManager : MonoBehaviour
{
    [SerializeField]
    protected RessourceData ressourceA;
    [SerializeField]
    protected RessourceData ressourceB;
    protected float valueRessourceA;
    protected float valueRessourceB;

    public static RessourcesManager Instance;

    private void Start()
    {
        Instance = this;
        valueRessourceA = ressourceA.defaultValue;
        valueRessourceB = ressourceB.defaultValue;
    }

    public void IncreaseRessourceA(float value)
    {
        valueRessourceA += value;
    }

    public void IncreaseRessourceB(float value)
    {
        valueRessourceB += value;
    }

    public bool HaveEnoughtResBuild(AbstractStructure structure)
    {
        return structure.costRessourceA <= valueRessourceA && structure.costRessourceB <= valueRessourceB;
    }
}
