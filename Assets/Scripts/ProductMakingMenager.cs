using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductMakingMenager : MonoBehaviour
{
    //This script should keep all MACHINES AND STUFF (Cargo) !!!!!!!!!!!!!!!!!!!!!!
    //

    [SerializeField]
    public List<GameObject> existingMachines;

    [SerializeField]
    public List<GameObject> existingStuffs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckProductionCondition()
    {
        int[] stuffActions = null;

        foreach (GameObject existingStuff in existingStuffs)
        {
            if(existingStuff != null)
            {
                stuffActions = existingStuff.GetComponent<ObjectStateChanger>().idActions;
            }
        }

        foreach (GameObject existingMachine in existingMachines)
        {
            if(stuffActions != null)
            {
                if (stuffActions.Length > 0)
                {
                    //Takes machineActions (Makes sauce, noodles...)
                    int[] machineActions = existingMachine.GetComponent<ObjectStateChanger>().idActions;


                    //Takes all machineActions and compares IF existingStuff have that
                    for (int i = 0; i < machineActions.Length; i++)
                    {
                        for (int j = 0; j < stuffActions.Length; j++)
                        {
                            if (machineActions[i] == stuffActions[j])
                            {
                                Debug.Log("MASZYNA: " + existingMachine.name + " Moze pracowac na skladniku: " + stuffActions[j]);
                                //existingMachine.GetComponent<ObjectStateChanger>().idStuffContain[i] = true;
                                existingMachine.GetComponent<ObjectStateChanger>().CheckToStart();
                            }
                        }
                    }
                }
            }
        }
    }

    public void AddExisitingStructure(int actionType, GameObject structure)
    {
        if(actionType == 0)
        {
            existingMachines.Add(structure);
        }
        else if(actionType == 1)
        {
            existingStuffs.Add(structure);
        }

        CheckProductionCondition();
    }

    public void RemoveExistingStructure(int actionType, GameObject structure)
    {
        if(existingMachines.Contains(structure))
        {
            if (actionType == 0)
            {
                int indexToRemove = existingMachines.IndexOf(structure);
                Debug.Log(indexToRemove);
                existingMachines.RemoveAt(indexToRemove);
            }
            else if (actionType == 1)
            {
                int indexToRemove = existingStuffs.IndexOf(structure);
                Debug.Log(indexToRemove);
                existingStuffs.RemoveAt(indexToRemove);
            }
        }

        CheckProductionCondition();
    }
}
