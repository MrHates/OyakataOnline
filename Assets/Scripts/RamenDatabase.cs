using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script contains creator of database for multiple variants of nudles

[CreateAssetMenu(fileName = "New Ramen Configuration", menuName = "RamenDatabase")]
public class RamenDatabase : ScriptableObject
{
    public List<Ingridient> ramen;
}

[Serializable]
public class Ingridient
{
    [field: SerializeField]
    public string Name { get; set; }

    [field: SerializeField]
    public int ID { get; set; }

    [field: NonSerialized]
    public bool isAlreadyChecked { get; set; }

    [field: NonSerialized]
    public bool ContainMachine { get; set; }

    [field: NonSerialized]
    public bool ContainCargo { get; set; }
}
