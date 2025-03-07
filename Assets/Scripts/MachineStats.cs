using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MachineStats : MonoBehaviour
{
    public int durability = 3;
    public int earningsPerDay;
    private Money money;
    private ObjectStateChanger obc;
    private ProductMakingMenager pmm;
    private MagazineMenager magazineMenager;

    [Header("Ingridients = 0, Cargo(Not ready to serve) = 1")]
    [SerializeField]
    private int databaseType;

    [Header("Id of ingridient or Cargo")]
    [SerializeField]
    private int[] stuffID;

    private void Awake()
    {
        magazineMenager = GameObject.FindGameObjectWithTag("MagazineMenager").GetComponent<MagazineMenager>();
        money = GameObject.FindGameObjectWithTag("Money").GetComponent<Money>();
        pmm = GameObject.FindGameObjectWithTag("ProductMakingMenager").GetComponent<ProductMakingMenager>();
        DayCycleMenager dsm = GameObject.FindGameObjectWithTag("DayCycleMenager").GetComponent<DayCycleMenager>();
        obc = gameObject.GetComponent<ObjectStateChanger>();

        dsm.onSummaryComplete.AddListener(MakeIngridients);
        dsm.onSummaryComplete.AddListener(RemoveDurability);

    }

    
    public void MakeIngridients()
    {
        if(obc.isMachineWorking)
        {
            for (int i = 0; i < stuffID.Length; i++)
            {
                magazineMenager.RemoveFromDatabase(1, stuffID[i], obc.cargoRequired);
                magazineMenager.AddToDatabase(0, stuffID[i], obc.ingridientOutcome);
            }
        }
    }

    //Old version that creates straight income from working
    public void TakeIncome()
    {
        if (money != null && obc.isMachineWorking)
        {
            money.UpdateMoney(earningsPerDay);
        }
    }

    public void RemoveDurability()
    {
        if(obc.isMachineWorking)
        {
            durability--;
            CheckMachineCondition();
        }
    }

    public void CheckMachineCondition()
    {
        if (durability <= 0)
        {
            RemoveMachine();
        }
    }

    public void RemoveMachine()
    {
        pmm.RemoveExistingStructure(obc.actionType, this.gameObject);
        Destroy(this.gameObject);
    }


}
