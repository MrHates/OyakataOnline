using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Money : MonoBehaviour
{
    public DayCycleMenager dayCycleMenager;

    public int moneyAmount;

    [SerializeField]
    private TextMeshProUGUI moneyText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateMoney(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMoney(int amount)
    {
        moneyAmount += amount;
        moneyText.text = moneyAmount.ToString();
    }
}
