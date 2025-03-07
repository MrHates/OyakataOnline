using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu]
public class SummaryDatabase : ScriptableObject
{
    public List<Summary> summaryPack;
    private DayResultMenager dayResultMenager;

    //Result functions. If returns TRUE attack was succeed. If FALSE attack cant be run and script tells we have to randomize again
    public bool EnemyAttack(int id)
    {
        dayResultMenager = GameObject.FindGameObjectWithTag("DayResultMenager").GetComponent<DayResultMenager>();
        switch (id)
        {
            case 0:
                {
                    GameObject[] structures = GameObject.FindGameObjectsWithTag("StructureCamera");
                    if (structures.Any(structure => structures != null))
                    {
                        return false;
                    }
                    else
                    {
                        dayResultMenager.RemoveStuff("StructureUniversal", 2);
                        return true;
                    }
                }

            case 1:
                {
                    GameObject[] structures = GameObject.FindGameObjectsWithTag("StructureCamera");
                    if (structures.Any(structure => structures != null))
                    {
                        return false;
                    }
                    else
                    {
                        dayResultMenager.RemoveStuff("StructureStuffUniversal", 3);
                        return true;
                    }
                }
            case 2:
                {
                    GameObject[] structures = GameObject.FindGameObjectsWithTag("StructureWatchman");
                    if (structures.Any(structure => structures != null))
                    {
                        return false;
                    }
                    else
                    {
                        dayResultMenager.RemoveMoney(1500);
                        return true;
                    }
                }
            default:
                {
                    return false;
                }


        }
    }
}

[Serializable]
public class Summary
{
    [field: SerializeField]
    public string title { get; private set; }

    [field: SerializeField]
    [field: TextArea]
    public string firstPage { get; private set; }

    [field: SerializeField]
    [field: TextArea]
    public string secondPage { get; private set; }

    [field: SerializeField]
    public int ID { get; private set; }

    [field: SerializeField]
    public string difficulty { get; private set; }
}
