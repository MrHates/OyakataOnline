using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySummary : MonoBehaviour
{
    [SerializeField]
    private InputMenager inputMenager;

    public AudioSource source;
    public AudioClip paperShowClip;
    public Animator summaryAnim;
    private bool isAlreadyOpen = false;
    public GameObject[] pages;
    public int currentPage = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSummary()
    {
        if(!isAlreadyOpen && !inputMenager.isInActivity)
        {
            inputMenager.isInActivity = true;
            summaryAnim.SetBool("isOpen", true);
            inputMenager.OnExit += CloseSummary;
            source.PlayOneShot(paperShowClip);
            isAlreadyOpen = true;
        }
    }

    public void CloseSummary()
    {
        if (isAlreadyOpen)
        {
            inputMenager.isInActivity = false;
            summaryAnim.SetBool("isOpen", false);
            inputMenager.OnExit -= CloseSummary;
            source.PlayOneShot(paperShowClip);
            isAlreadyOpen = false;
        }
    }

    public void NextPage()
    {
        if(currentPage < 1)
        {
            pages[currentPage].SetActive(false);
            currentPage += 1;
            pages[currentPage].SetActive(true);
        }
    }

    public void PreviousPage()
    {
        if(currentPage > 0)
        {
            pages[currentPage].SetActive(false);
            currentPage -= 1;
            pages[currentPage].SetActive(true);
        }
    }
}
