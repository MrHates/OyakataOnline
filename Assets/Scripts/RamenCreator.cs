using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RamenCreator : MonoBehaviour
{
    [SerializeField]
    private InputMenager inputMenager;

    [SerializeField]
    private CompletedRamenDatabase completedRamenList;

    [SerializeField]
    private MagazineDatabase ingridientsDatabase;

    [SerializeField]
    private RamenTypesDatabase ramenTypeDatabase;

    public GameObject individualInfoBox;
    public TextMeshProUGUI finalAmountText;
    public TextMeshProUGUI cups,noodles,spicies,sauce,vegs;
    public GameObject createButton;

    private int currentRamenTypeID = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSpecificInfo(Transform infoSpawnPos)
    {
        individualInfoBox.SetActive(true);
        individualInfoBox.transform.position = infoSpawnPos.position;
        ResetValues();
    }

    public void OpenSpecificInfo(int ramenID)
    {
        currentRamenTypeID = ramenID;
        inputMenager.OnExit += CloseSpecificInfo;
    }

    public void CloseSpecificInfo()
    {
        ResetValues();
        individualInfoBox.SetActive(false);
        inputMenager.OnExit -= CloseSpecificInfo;
    }

    public void ResetValues()
    {
        createButton.SetActive(false);

        finalAmountText.text = "0";

        cups.text = "0";
        noodles.text = "0";
        spicies.text = "0";
        sauce.text = "0";
        vegs.text = "0";
    }



    public void OnScrollbarValueChange(float value)
    {

        Scrollbar scrollbar = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Scrollbar>();
        float steppedValue = Mathf.Round(scrollbar.value / 0.1f) * 10;
        int finalValue = (int)Mathf.Floor(steppedValue);

        finalAmountText.text = finalValue.ToString();

        RamenType currentRamen = ramenTypeDatabase.ramenTypes.Find(obj => obj.ID == currentRamenTypeID);
        cups.text = ((currentRamen.NeededIngridients[0])*finalValue).ToString();
        noodles.text = ((currentRamen.NeededIngridients[1]) * finalValue).ToString();
        spicies.text = ((currentRamen.NeededIngridients[2]) * finalValue).ToString();
        sauce.text = ((currentRamen.NeededIngridients[3]) * finalValue).ToString();
        vegs.text = ((currentRamen.NeededIngridients[4]) * finalValue).ToString();

        if(finalValue > 0)
        {
            createButton.SetActive(true);
        }
        else
        {
            createButton.SetActive(false);
        }

        Debug.Log(finalValue);
    }

    public void CreateRamen()
    {
        CompletedRamen foundRamen = completedRamenList.completedRamens.Find(obj => obj.ramenType.ID == currentRamenTypeID);

        int cupsAmount = ingridientsDatabase.magazineData[0].Amount;
        int noodlesAmount = ingridientsDatabase.magazineData[1].Amount;
        int spiciesAmount = ingridientsDatabase.magazineData[2].Amount;
        int sauceAmount = ingridientsDatabase.magazineData[3].Amount;
        int vegsAmount = ingridientsDatabase.magazineData[4].Amount;

        if (cupsAmount < int.Parse(cups.text) || noodlesAmount < int.Parse(noodles.text) || spiciesAmount < int.Parse(spicies.text) || sauceAmount < int.Parse(sauce.text) || vegsAmount < int.Parse(vegs.text))
        {
            individualInfoBox.GetComponent<Animator>().SetTrigger("Error");
            individualInfoBox.GetComponent<Animator>().SetTrigger("Idle");
            Debug.Log("womp womp brak ingridientow");
        }
        else
        {
            individualInfoBox.GetComponent<Animator>().SetTrigger("Success");
            individualInfoBox.GetComponent<Animator>().SetTrigger("Idle");
            foundRamen.Amount += int.Parse(finalAmountText.text);

            ingridientsDatabase.magazineData[0].Amount -= int.Parse(cups.text);
            ingridientsDatabase.magazineData[1].Amount -= int.Parse(noodles.text);
            ingridientsDatabase.magazineData[2].Amount -= int.Parse(spicies.text);
            ingridientsDatabase.magazineData[3].Amount -= int.Parse(sauce.text);
            ingridientsDatabase.magazineData[4].Amount -= int.Parse(vegs.text);
        }
    }
}
