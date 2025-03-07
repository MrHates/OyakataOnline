using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamenStateMenager : MonoBehaviour
{
    [SerializeField]
    private RamenDatabase ramenDatabase;

    [SerializeField]
    private CheckList checkList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    public void IncraseRamenUniversal(int[] actions, int actionType)
    {
        foreach (int action in actions)
        {
            var foundObject = ramenDatabase.ramen.Find(data => data.ID == action);
            Debug.Log(ramenDatabase.ramen);
            if (foundObject != null)
            {
                if(actionType == 0)
                {
                    foundObject.ContainMachine = true;
                    Debug.Log(foundObject);
                }
                else if(actionType == 1)
                {
                    foundObject.ContainCargo = true;
                    Debug.Log(foundObject);
                }

                if(foundObject.ContainCargo == true && foundObject.ContainMachine == true && !foundObject.isAlreadyChecked)
                {
                    foundObject.isAlreadyChecked = true;
                    checkList.checkListItems[action].sprite = checkList.trueSprite;
                    checkList.gameObject.GetComponent<AudioSource>().PlayOneShot(checkList.checkClip);
                }
            }
        }
    }
    */
}
