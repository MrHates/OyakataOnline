using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeliveryMenager : MonoBehaviour
{
    [SerializeField]
    public ContractDatabase contractList;

    [SerializeField]
    public CompletedRamenDatabase completedRamenDatabase;

    [SerializeField]
    public List<Contract> takenContracts;

    [SerializeField]
    public Money money;

    public GameObject contractListPsyhical;

    public void AddContract(int id)
    {
        takenContracts.Add(contractList.contract.Find(obj => obj.ID == id));
    }

    public void RemoveContract(int id)
    {
        takenContracts.RemoveAll(obj => obj.ID == id);
    }

    public void FinalizeDelivery()
    {
        bool canMakeDeal = true;

        foreach (var contract in takenContracts)
        {
            // Removing completed ramens
            for (int j = 0; j < contract.NeededRamenTypes.Count; j++)
            {
                if(completedRamenDatabase.completedRamens[contract.NeededRamenTypes[j]].Amount >= contract.NeededAmounts[j])
                {
                    canMakeDeal = true;
                }
                else
                {
                    canMakeDeal = false;
                }
            }
        }

        if(canMakeDeal)
        {
            foreach (var contract in takenContracts)
            {
                // Removing completed ramens
                for (int j = 0; j < contract.NeededRamenTypes.Count; j++)
                {
                    completedRamenDatabase.completedRamens[contract.NeededRamenTypes[j]].Amount -= contract.NeededAmounts[j];
                }
                money.UpdateMoney(contract.Prize);
            }
            
            foreach(Transform child in contractListPsyhical.transform)
            {
                Destroy(child.gameObject);
            }

            takenContracts.Clear();
            Debug.Log("Deal udany");
        }
        else
        {
            Debug.Log("Deal spierdolil sie");
        }
    }
}
