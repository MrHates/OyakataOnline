using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class MapContractSetter : MonoBehaviour
{
    [SerializeField]
    private ContractDatabase contractDatabase;

    [SerializeField]
    private RamenTypesDatabase ramenTypesDatabase;

    public Image companyImage;
    public TextMeshProUGUI description;
    public TextMeshProUGUI requirements;
    public TextMeshProUGUI price;

    public GameObject contractPrefab;
    public GameObject contractList;

    [Header("Info about taking mission/contract")]
    public GameObject contract;
    public GameObject notVisibleContract;

    public DeliveryMenager deliveryMenager;


    private int currentContractId = 0;

    // Start is called before the first frame update
    void Start()
    {
        ContractNotActive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ContractActive()
    {
        contract.SetActive(true);
        notVisibleContract.SetActive(false);
    }

    public void ContractNotActive()
    {
        contract.SetActive(false);
        notVisibleContract.SetActive(true);
    }



    public void DisplayContract(int id)
    {
        Contract foundObject = contractDatabase.contract.Find(obj => obj.ID == id);
        Debug.Log(foundObject.CompanyName);

        companyImage.sprite = foundObject.CompanyBanner;
        description.text = foundObject.Description;

        requirements.text = "";

        for (int i = 0; i < foundObject.NeededRamenTypes.Count; i++)
        {
            RamenType neededRamen = ramenTypesDatabase.ramenTypes.Find(obj => obj.ID == foundObject.NeededRamenTypes[i]);
            requirements.text += (foundObject.NeededAmounts[i] + "x " + neededRamen.Name + ", ");
        }
        price.text = foundObject.Prize.ToString() + "$";
        currentContractId = id;
        ContractActive();
    }

    public void TakeContract()
    {
        Contract foundObject = contractDatabase.contract.Find(obj => obj.ID == currentContractId);

        GameObject newContract = Instantiate(contractPrefab, contractList.transform);
        newContract.transform.localPosition = Vector3.zero;
        newContract.transform.localRotation = Quaternion.identity;

        newContract.transform.GetChild(0).GetComponent<Image>().sprite = foundObject.CompanyBanner;
        deliveryMenager.AddContract(currentContractId);
        ContractNotActive();

    }
}
