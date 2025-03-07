using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffStats : MonoBehaviour
{
    [Header("Ingridients = 0, Cargo(Not ready to serve) = 1")]
    [SerializeField]
    private int databaseType;

    [Header("Id of ingridient or Cargo")]
    [SerializeField]
    private int[] stuffID;

    [Header("How much ingridients or palletes you wanna add to magazine")]
    [SerializeField]
    private int stuffCount;

    [SerializeField]
    private int durability;

    private ObjectStateChanger obc;
    private ProductMakingMenager pmm;
    private MagazineMenager magazineMenager;

    private void Awake()
    {
        magazineMenager = GameObject.FindGameObjectWithTag("MagazineMenager").GetComponent<MagazineMenager>();
        pmm = GameObject.FindGameObjectWithTag("ProductMakingMenager").GetComponent<ProductMakingMenager>();
        obc = gameObject.GetComponent<ObjectStateChanger>();
        DayCycleMenager dsm = GameObject.FindGameObjectWithTag("DayCycleMenager").GetComponent<DayCycleMenager>();
        dsm.onSummaryComplete.AddListener(RemoveStuffDurability);

        //Dodaje rzeczy do magazynu
        AddStuffToMagazine();

    }

    public void RemoveStuffDurability()
    {
        durability--;
        CheckStuffCondition();
    }

    public void CheckStuffCondition()
    {
        if (durability <= 0)
        {
            RemoveStuff();
        }
    }

    public void AddStuffToMagazine()
    {
        for (int i = 0; i < stuffID.Length; i++)
        {
            magazineMenager.AddToDatabase(databaseType, stuffID[i], stuffCount);
        }
    }

    public void RemoveStuffFromMagazine()
    {
        for (int i = 0; i < stuffID.Length; i++)
        {
            magazineMenager.RemoveFromDatabase(databaseType, stuffID[i], stuffCount);
        }
    }

    public void RemoveStuff()
    {
        try
        {
            pmm.RemoveExistingStructure(obc.actionType, this.gameObject);
            Destroy(this.gameObject);
        }
        catch
        {

        }
    }
}
