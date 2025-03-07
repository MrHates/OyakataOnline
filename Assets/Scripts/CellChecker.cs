using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellChecker : MonoBehaviour
{
    [SerializeField]
    private GameObject[] checkers;
    [SerializeField]
    private LayerMask layerMask;

    private void Update()
    {

    }

    public bool CanPlaceStructure(int size)
    {
        switch(size)
        {
            case 1:
                checkers[0].SetActive(true);
                break;
            case 2:
                checkers[1].SetActive(true);
                checkers[2].SetActive(true);
                checkers[3].SetActive(true);
                checkers[4].SetActive(true);
                break;
            default:
                return false;
                break;

        }

        foreach (GameObject checker in checkers)
        {
            if(checker.gameObject.activeInHierarchy)
            {
                if (Physics.CheckSphere(checker.transform.position, 0.1f, layerMask))
                {
                    Debug.Log("Znaleziono Strukture!");
                    return false;
                }
            }
        }

        foreach (GameObject checker in checkers)
        {
            checker.SetActive(false);
        }

        return true;
    }
}
