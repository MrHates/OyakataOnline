using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{

    public GameObject mousePointer;
    public GameObject[] cellPointer;

    public InputMenager inputMenager;
    public Grid grid;

    [SerializeField]
    private Money money;
    [SerializeField]
    private ObjectsDatabase database;
    [SerializeField]
    private CellChecker cellChecker;
    private int selectedObjectIndex = -1;
    private int selectedObjectSize = -1;

    [SerializeField]
    private GameObject gridVisualisation;

    public AudioSource source;
    public AudioClip buildClip;
    public GameObject buildParticle;

    private void Start()
    {
        StopPlacement();
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisualisation.SetActive(false);
        if(selectedObjectSize >= 0)
        {
            cellPointer[selectedObjectSize].SetActive(false);
        }
        //Subskrybja wydarzen z input menagera - Funkcje wykonuj¹ siê po evencie w input menadzerze. 
        inputMenager.OnClicked -= PlaceStructure;
        inputMenager.OnExit -= StopPlacement;

    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        var foundObject = database.objectsData.Find(data => data.ID == ID);
        if(foundObject != null)
        {
            selectedObjectIndex = foundObject.ID;
            selectedObjectSize = foundObject.Size;
        }
        if(selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        gridVisualisation.SetActive(true);
        cellPointer[selectedObjectSize].SetActive(true);
        //Kolejna subskrybcja
        inputMenager.OnClicked += PlaceStructure;
        inputMenager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if(!cellChecker.CanPlaceStructure(selectedObjectSize))
        {
            return;
        }
        if (inputMenager.IsPointerOverSpecificUI("RenderTexture"))
        {
            return;
        }
        if(database.objectsData[selectedObjectIndex].Price > money.moneyAmount)
        {
            return;
        }
        Vector3 mousePos = inputMenager.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);
        GameObject structureObj = Instantiate(database.objectsData[selectedObjectIndex].Prefab);
        structureObj.transform.position = grid.CellToWorld(gridPos);
        //Chyba dodaje do state builda zupki jakas rzecz KURWA NIE DZIALA JAK ZWYKLE JA PIERDOLE
        //Wreszcie zadzialalo, nie potrzebnie najeba³em tyle skrypcikow z eventami ale chcia³em przykozaczyc
        structureObj.GetComponent<ObjectStateChanger>().ingridientIncrase.Invoke();
        Instantiate(buildParticle, structureObj.transform.position + new Vector3(0,3,0), Quaternion.identity);
        money.UpdateMoney(-database.objectsData[selectedObjectIndex].Price);
        source.PlayOneShot(buildClip);
    }

    private void Update()
    {
        if (selectedObjectIndex < 0)
            return;

        Vector3 mousePos = inputMenager.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);

        mousePointer.transform.position = mousePos;
        cellPointer[selectedObjectSize].transform.position = grid.CellToWorld(gridPos);
    }
}
