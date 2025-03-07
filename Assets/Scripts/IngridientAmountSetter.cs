using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IngridientAmountSetter : MonoBehaviour
{
    public int ingridientID;
    public TextMeshProUGUI textAmount;
    public MagazineDatabase magazineDatabase;

    public void UpdateIngridientAmount()
    {
        MagazineData foundObject = magazineDatabase.magazineData.Find(obj => obj.ID == ingridientID);
        textAmount.text = foundObject.Amount.ToString();
    }
}
