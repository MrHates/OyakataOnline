using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectStateChanger : MonoBehaviour
{
    //Ten skrypt powinien byc przypisany tylko i wylacznie do struktury. 

    [Header("What cargo is required to work")]
    [SerializeField]
    public int[] idActions;

    //ONLY FOR MACHINES. SHOWS WHAT INGRIDIENTS ARE CONTAINED
    /*
    [SerializeField]
    public bool[] idStuffContain;
    */

    [SerializeField, Header("Action type:               0 - machine   1 - stuff")]
    public int actionType;

    private RamenStateMenager ramenStateMenager;

    private ProductMakingMenager productMakingMenager;

    private MachineNoiceGenerator machineNoiceGenerator;

    private MagazineMenager magazineMenager;

    [SerializeField]
    public int cargoRequired;

    [SerializeField]
    public int ingridientOutcome;

    [HideInInspector]
    public UnityEvent ingridientIncrase, ingridientDecrase;

    public bool isMachineWorking = false;

    [Header("Used to check if machine can work on cargo")]
    [SerializeField]
    private int databaseType = 1;

    [Header("Graphical aspects")]
    public GameObject workingParticles;
    public Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        //Sets ingridient that machine contains to false
        //idStuffContain = new bool[idActions.Length];

        magazineMenager = GameObject.FindGameObjectWithTag("MagazineMenager").GetComponent<MagazineMenager>();
        machineNoiceGenerator = GameObject.FindGameObjectWithTag("MachineNoiceGenerator").GetComponent<MachineNoiceGenerator>();
        productMakingMenager = GameObject.FindGameObjectWithTag("ProductMakingMenager").GetComponent<ProductMakingMenager>();
        ramenStateMenager = GameObject.FindGameObjectWithTag("RamenStateMenager").GetComponent<RamenStateMenager>();
        DayCycleMenager dsm = GameObject.FindGameObjectWithTag("DayCycleMenager").GetComponent<DayCycleMenager>();

        //ingridientIncrase.AddListener(() => ramenStateMenager.IncraseRamenUniversal(idActions, actionType));
        Debug.Log("Liczba s³uchaczy: " + ingridientIncrase.GetPersistentEventCount());
        productMakingMenager.AddExisitingStructure(actionType, gameObject);


        dsm.onSummaryComplete.AddListener(CheckToStart);
        CheckToStart();
    }


    public void CheckToStart()
    {
        MagazineData[] cargoObjects = magazineMenager.GetAmountByID(databaseType, idActions);

        //Checks if all cargo elements are in place
        bool allEnough = true;
        
        foreach(MagazineData data in cargoObjects)
        {
            Debug.Log(data.Amount);
        }

        foreach (MagazineData data in cargoObjects)
        {
            if(data.Amount < cargoRequired)
            {
                Debug.Log(data.Name + "" + data.Amount);
                allEnough = false;
                break;
            }
        }

        if(allEnough)
        {
            StartWorking();
        }
        else
        {
            StopWorking();
        }
    }

    public void StartWorking()
    {
        try
        {
            isMachineWorking = true;
            anim.SetBool("isWorking", true);
            machineNoiceGenerator.StartNoice();
            workingParticles.SetActive(true);
        }
        catch
        {

        }
    }

    public void StopWorking()
    {
        try
        {
            isMachineWorking = false;
            anim.SetBool("isWorking", false);
            workingParticles.SetActive(false);
        }
        catch
        {

        }
    }
}
