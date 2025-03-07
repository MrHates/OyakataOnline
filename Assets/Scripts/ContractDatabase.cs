using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script contains creator of database for multiple variants of nudles

[CreateAssetMenu(fileName = "New Contract", menuName = "Contract database")]
public class ContractDatabase : ScriptableObject
{
    public List<Contract> contract;
}

[Serializable]
public class Contract
{
    [field: SerializeField]
    public string CompanyName { get; set; }

    [field: SerializeField]
    public Sprite CompanyBanner { get; set; }

    [field: SerializeField]
    public int ID { get; set; }

    [field: SerializeField]
    public string Description { get; set; }

    [field: SerializeField]
    public List<int> NeededRamenTypes { get; set; }

    [field: SerializeField]
    public List<int> NeededAmounts { get; set; }

    [field: SerializeField]
    public int Prize { get; set; }
}
