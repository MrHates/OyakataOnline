using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopActivitiesMenager : MonoBehaviour
{
    [SerializeField]
    private InputMenager inputMenager;

    [SerializeField]
    private IngridientAmountSetter[] ingAmountSetters;
    [SerializeField]
    private IngridientAmountSetter[] cargoAmountSetters;

    public GameObject desktopShop;

    public GameObject ingridientMagazine;
    public GameObject cargoMagazine;
    public GameObject deliveryMap;
    public GameObject mapUI;
    public GameObject delivery;
    public GameObject ramenChoose;

    private bool isMenuOpen;

    public void OpenRamenChoose()
    {
        ramenChoose.SetActive(true);
        inputMenager.OnExit += CloseAll;
        desktopShop.SetActive(false);
    }

    public void CloseRamenChoose()
    {
        ramenChoose.SetActive(false);
        inputMenager.OnExit -= CloseAll;
        desktopShop.SetActive(true);
    }

    public void OpenDelivery()
    {
        delivery.SetActive(true);
        inputMenager.OnExit += CloseAll;
        desktopShop.SetActive(false);
    }

    public void CloseDelivery()
    {
        delivery.SetActive(false);
        inputMenager.OnExit -= CloseAll;
        desktopShop.SetActive(true);
    }

    public void OpenIngridientMagazine()
    {
        UpdateTextAmount();
        inputMenager.OnExit += CloseAll;
        ingridientMagazine.SetActive(true);
        desktopShop.SetActive(false);
    }

    public void OpenCargoMagazine()
    {
        UpdateTextAmountCargo();
        inputMenager.OnExit += CloseAll;
        cargoMagazine.SetActive(true);
        desktopShop.SetActive(false);
    }

    public void CloseAll()
    {
        ingridientMagazine.SetActive(false);
        cargoMagazine.SetActive(false);
        deliveryMap.SetActive(false);
        delivery.SetActive(false);
        ramenChoose.SetActive(false);
        mapUI.SetActive(false);
        inputMenager.OnExit -= CloseIngiridientMagazine;
    }

    public void CloseIngiridientMagazine()
    {
        desktopShop.SetActive(true);
        ingridientMagazine.SetActive(false);
    }

    public void OpenDeliveryMap()
    {
        inputMenager.OnExit += CloseAll;
        mapUI.SetActive(true);
        desktopShop.SetActive(false);
        deliveryMap.SetActive(true);
    }

    public void CloseDeliveryMap()
    {
        inputMenager.OnExit -= CloseAll;
        mapUI.SetActive(false);
        desktopShop.SetActive(true);
        deliveryMap.SetActive(false);
    }

    public void CloseCargoMagazine()
    {
        desktopShop.SetActive(true);
        cargoMagazine.SetActive(false);
    }

    private void UpdateTextAmount()
    {
        foreach(IngridientAmountSetter setter in ingAmountSetters)
        {
            setter.UpdateIngridientAmount();
        }
    }

    private void UpdateTextAmountCargo()
    {
        foreach (IngridientAmountSetter setter in cargoAmountSetters)
        {
            setter.UpdateIngridientAmount();
        }
    }
}
