using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;


public class DayCycleMenager : MonoBehaviour
{

    [SerializeField]
    private SummaryDatabase[] summaryDatabases;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI firstPage;
    public TextMeshProUGUI secondPage;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI stuffText;

    [SerializeField]
    private Animator endShiftAnim;

    [SerializeField]
    private GameObject blackBackground;

    private bool isShiftEnded = false;

    private int dayCount = 0;

    [HideInInspector]
    public UnityEvent onSummaryComplete;


    // Start is called before the first frame update
    void Awake()
    {
        if(!isShiftEnded)
        {
            blackBackground.SetActive(false);
        }

        if (onSummaryComplete == null)
            onSummaryComplete = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNewSummary()
    {
        dayCount += 1;
        bool isSummaryAccepted = false;

        while (!isSummaryAccepted)
        {
            int rndDatabase = Random.Range(0, summaryDatabases.Length);
            int rndSummaryID = RandomizeSummary(rndDatabase);
            Summary randomSummary = summaryDatabases[rndDatabase].summaryPack[rndSummaryID];

            if (rndDatabase == 0)
            {
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! TEST 
                isSummaryAccepted = summaryDatabases[rndDatabase].EnemyAttack(rndSummaryID);
            }
            else
            {
                isSummaryAccepted = true;
            }

            dayText.text = dayCount.ToString();
            firstPage.text = randomSummary.firstPage;
            secondPage.text = randomSummary.secondPage;
        }

        onSummaryComplete.Invoke();
        Debug.Log("onSummaryComplete event invoked");  // Dodaj log po Invoke
    }

    public int RandomizeSummary(int summaryNumber)
    {
        int rnd = Random.Range(0, summaryDatabases[summaryNumber].summaryPack.Count);
        return rnd;
    }

    //Use when shift changes and starts
    public void ShiftChange()
    {
        StartCoroutine(AnimationLoad());
    }

    public IEnumerator AnimationLoad()
    {
        if(!isShiftEnded)
        {
            blackBackground.SetActive(true);
            endShiftAnim.SetBool("isOpen", true);
            isShiftEnded = true;
            yield return new WaitForSeconds(3f);
            SetNewSummary();
            endShiftAnim.SetBool("isOpen", false);
            isShiftEnded = false;
            yield return new WaitForSeconds(3f);
            blackBackground.SetActive(false);
        }
    }
}
