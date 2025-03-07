using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stuff : MonoBehaviour
{
    public DayCycleMenager dayCycleMenager;

    public int stuffAmount;

    [SerializeField]
    private TextMeshProUGUI stuffText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateStuff(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateStuff(int amount)
    {
        stuffAmount += amount;
        stuffText.text = stuffAmount.ToString();
    }
}
