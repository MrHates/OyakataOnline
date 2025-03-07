using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New magazine", menuName = "Magazine")]
public class MagazineDatabase : ScriptableObject
{
    public List<MagazineData> magazineData;
}

[Serializable]
public class MagazineData
{
    [field: SerializeField]
    public string Name { get; private set; }

    [field: SerializeField]
    public int ID { get; private set; }

    [field: SerializeField]
    public int Amount { get; set; }
}
