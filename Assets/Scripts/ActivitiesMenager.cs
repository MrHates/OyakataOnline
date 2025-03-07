using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivitiesMenager : MonoBehaviour
{
    [SerializeField]
    private InputMenager inputMenager;

    [SerializeField]
    private GameObject shopDisplay;

    [SerializeField]
    private GameObject desktopDisplay;

    public AudioSource source;
    public AudioClip crank1, crank2, crank3, crank4;

    private bool isShopOpened;
    private bool isDesktopOpened;

    public Animator shopAnim;
    public Animator desktopAnim;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenBusiness()
    {
        if(!isDesktopOpened && !inputMenager.isInActivity)
        {
            inputMenager.isInActivity = true;
            source.PlayOneShot(crank3);
            desktopAnim.SetBool("isOpened", true);
            inputMenager.OnExit += CloseBusiness;
            isDesktopOpened = true;
            StartCoroutine(DisplayDesktop());
        }
    }

    public void CloseBusiness()
    {
        if (isDesktopOpened)
        {
            inputMenager.isInActivity = false;
            source.PlayOneShot(crank4);
            desktopAnim.SetBool("isOpened", false);
            inputMenager.OnExit -= CloseBusiness;
            isDesktopOpened = false;
            StartCoroutine(DisplayDesktop());
        }
    }

    public void OpenShop()
    {
        if(!isShopOpened && !inputMenager.isInActivity)
        {
            inputMenager.isInActivity = true;
            source.PlayOneShot(crank1);
            shopAnim.SetBool("isOpened", true);
            inputMenager.OnExit += CloseShop;
            isShopOpened = true;
            StartCoroutine(DisplayShop());
        }
    }

    public void CloseShop()
    {
        if(isShopOpened)
        {
            inputMenager.isInActivity = false;
            source.PlayOneShot(crank2);
            shopAnim.SetBool("isOpened", false);
            inputMenager.OnExit -= CloseShop;
            isShopOpened = false;
            StartCoroutine(DisplayShop());
        }
    }

    private IEnumerator DisplayShop()
    {
        if(isShopOpened)
        {
            yield return new WaitForSeconds(0.2f);
            shopDisplay.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            shopDisplay.SetActive(false);
        }
    }

    private IEnumerator DisplayDesktop()
    {
        if (isDesktopOpened)
        {
            yield return new WaitForSeconds(0.4f);
            desktopDisplay.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(0.05f);
            desktopDisplay.SetActive(false);
        }
    }
}
