using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraMove : MonoBehaviour
{
    public Transform playerCamera;
    private Vector3 turn;
    public Vector3 origin;

    private bool drag = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            drag = true;
        }
        else
        {
            drag = false;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            playerCamera.position = origin;
        }

        CalculateDrag();
    }

    private void CalculateDrag()
    {
        if (drag)
        {
            turn.x -= Input.GetAxis("Mouse X") / 3;
            turn.z -= Input.GetAxis("Mouse Y") / 3;
        }
        Vector3 target = new Vector3(turn.x, origin.y, turn.z);
        playerCamera.position = Vector3.Lerp(playerCamera.position, target, Time.deltaTime * 10);
    }
}
