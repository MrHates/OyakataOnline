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
            Debug.Log("Lewy przycisk myszy klikniêty!");

            if (IsPointerOverSpecificUI("RenderPSX"))
            {
                Debug.Log("Klikniêcie ZABLOKOWANE przez UI RenderPSX!");
            }
            else
            {
                Debug.Log("Klikniêcie WYKRYTE – wywo³anie OnClicked!");
                OnClicked?.Invoke();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape naciœniêty – wywo³anie OnExit!");
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
            Debug.Log($"Klikniêto na UI: {result.gameObject.name}");

            if (result.gameObject.CompareTag(tag))
            {
                return true; // Znaleziono UI z podanym tagiem, ignorujemy klikniêcie
            }
        }
        return false;
    }

    public Vector3 GetSelectedMapPosition()
    {
        if (IsPointerOverSpecificUI("RenderPSX"))
        {
            Debug.Log("Pozycja NIE zaktualizowana - klikniêcie na UI RenderPSX!");
            return lastPosition; // Ignoruje UI "RenderPSX"
        }

        Vector3 mousePos = Input.mousePosition;
        Ray ray = playerCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, placementLayermask))
        {
            Debug.Log($"Raycast trafi³ w: {hit.collider.gameObject.name} na pozycji {hit.point}");
            lastPosition = hit.point;
        }
        else
        {
            Debug.Log("Raycast NIE trafi³ w ¿aden obiekt!");
        }

        return lastPosition;
    }
}
