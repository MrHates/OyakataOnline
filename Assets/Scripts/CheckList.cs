using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckList : MonoBehaviour
{
    [SerializeField]
    private MagazineDatabase ingridientDatabase;
    public Sprite trueSprite, falseSprite;
    public Image[] checkListItems;
    public AudioClip checkClip;
    // Start is called before the first frame update
    void Start()
    {
        CheckIngridientsStatus();
    }

    public void CheckIngridientsStatus()
    {
        for (int i = 0; i < checkListItems.Length; i++)
        {
            MagazineData ingridient = ingridientDatabase.magazineData.Find(obj => obj.ID == i);
            if (ingridient.Amount > 0)
            {
                checkListItems[i].sprite = trueSprite;
            }
            else
            {
                checkListItems[i].sprite = falseSprite;
            }
        }
    }
}
