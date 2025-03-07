using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script contains creator of database for multiple variants of nudles

[CreateAssetMenu(fileName = "New ramen types", menuName = "Ramen Types")]
public class RamenTypesDatabase : ScriptableObject
{
    public List<RamenType> ramenTypes;
}

[Serializable]
public class RamenType
{
    [field: SerializeField]
    public string Name { get; set; }

    [field: SerializeField]
    public int ID { get; set; }

    [field: SerializeField]
    public Sprite RamenSprite { get; set; }

    [field: SerializeField]
    public float Price { get; set; }

    [field: SerializeField]
    public string Quality { get; set; }

    [field: SerializeField]
    public int[] NeededIngridients;
}
