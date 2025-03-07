using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MagazineMenager : MonoBehaviour
{
    public MagazineDatabase[] magazineDatabases;
    // Start is called before the first frame update

    public MagazineData[] GetAmountByID(int databaseIndex, int[] id)
    {
        MagazineData[] foundObjects = magazineDatabases[databaseIndex].magazineData.FindAll(obj => id.Contains(obj.ID)).ToArray();

        if(foundObjects != null)
        {
            return foundObjects;
        }
        else
        {
            Debug.LogWarning($"Nie znaleziono przedmiotu o ID: {id}");
            return null; // Zwraca 0, jeœli ID nie istnieje
        }
    }

    public void AddToDatabase(int databaseIndex, int id, int count)
    {
        MagazineData foundObject = magazineDatabases[databaseIndex].magazineData.Find(obj => obj.ID == id);

        if(foundObject != null)
        {
            foundObject.Amount += count;
        }
    }

    public void RemoveFromDatabase(int databaseIndex, int id, int count)
    {
        MagazineData foundObject = magazineDatabases[databaseIndex].magazineData.Find(obj => obj.ID == id);

        if (foundObject != null)
        {
            foundObject.Amount -= count;
        }
    }
}
