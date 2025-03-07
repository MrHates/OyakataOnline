using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InputMenager : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;

    private Vector3 lastPosition;

    [SerializeField]
    private LayerMask placementLayermask;

    public event Action OnClicked, OnExit;

    public bool isInActivity = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Lewy przycisk myszy klikni�ty!");

            if (IsPointerOverSpecificUI("RenderPSX"))
            {
                Debug.Log("Klikni�cie ZABLOKOWANE przez UI RenderPSX!");
            }
            else
            {
                Debug.Log("Klikni�cie WYKRYTE � wywo�anie OnClicked!");
                OnClicked?.Invoke();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape naci�ni�ty � wywo�anie OnExit!");
            OnExit?.Invoke();
        }
    }

    public bool IsPointerOverSpecificUI(string tag)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            Debug.Log($"Klikni�to na UI: {result.gameObject.name}");

            if (result.gameObject.CompareTag(tag))
            {
                return true; // Znaleziono UI z podanym tagiem, ignorujemy klikni�cie
            }
        }
        return false;
    }

    public Vector3 GetSelectedMapPosition()
    {
        if (IsPointerOverSpecificUI("RenderPSX"))
        {
            Debug.Log("Pozycja NIE zaktualizowana - klikni�cie na UI RenderPSX!");
            return lastPosition; // Ignoruje UI "RenderPSX"
        }

        Vector3 mousePos = Input.mousePosition;
        Ray ray = playerCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, placementLayermask))
        {
            Debug.Log($"Raycast trafi� w: {hit.collider.gameObject.name} na pozycji {hit.point}");
            lastPosition = hit.point;
        }
        else
        {
            Debug.Log("Raycast NIE trafi� w �aden obiekt!");
        }

        return lastPosition;
    }
}
