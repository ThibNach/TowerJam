using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RessourceData : ScriptableObject
{
    public string name;
    public GameObject prefabs;
    public Sprite icon;
    public float defaultValue;
}
