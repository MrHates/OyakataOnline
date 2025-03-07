using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script contains creator of database for multiple variants of nudles

[CreateAssetMenu(fileName = "New Ramen Configuration", menuName = "RamenDatabase")]
public class CompletedRamenDatabase : ScriptableObject
{
    public List<CompletedRamen> completedRamens;
}

[Serializable]
public class CompletedRamen
{
    [field: SerializeField]
    public RamenType ramenType { get; set; }

    [field: SerializeField]
    public int Amount { get; set; }
}
