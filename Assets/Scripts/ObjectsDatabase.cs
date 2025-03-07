using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectsDatabase : ScriptableObject
{
    public List<ObjectData> objectsData;
}

[Serializable]
public class ObjectData
{
    [field: SerializeField]
    public string Name { get; private set;}

    [field: SerializeField]
    public int ID { get; private set; }

    [field: SerializeField]
    public int Size { get; private set; }

    [field: SerializeField]
    public GameObject Prefab { get; private set; }

    [field: SerializeField]
    public int Price { get; private set; }
}