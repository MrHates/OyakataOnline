using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayResultMenager : MonoBehaviour
{
    public Money money;

    public void RemoveMoney(int amount)
    {
        money.UpdateMoney(-amount);
    }
    
    public void AddMoney(int amount)
    {
        money.UpdateMoney(amount);
    }

    public void RemoveStuff(string structureTag, int amount)
    {
        GameObject[] structure = GameObject.FindGameObjectsWithTag(structureTag);

        for (int i = 0; i < structure.Length; i++)
        {
            Debug.Log("Dostepne struktury:" + structure[i]);
        }

        if(structure != null)
        {
            int removeCount = Mathf.Min(amount, structure.Length);

            for (int i = 0; i < removeCount; i++)
            {
                try
                {
                    if(structure[i] != null)
                    {
                        if (structure[i].GetComponent<MachineStats>() != null)
                        {
                            structure[i].GetComponent<MachineStats>().RemoveMachine();
                        }
                        else if (structure[i].GetComponent<StuffStats>() != null)
                        {
                            structure[i].GetComponent<StuffStats>().RemoveStuff();
                        }
                        Destroy(structure[i]);
                    }
                }
                catch
                {

                }
            }
        }
    }
}
