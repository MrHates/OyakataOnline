using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineNoiceGenerator : MonoBehaviour
{
    public AudioSource source;
    private bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNoice()
    {
        if(!isPlaying)
        {
            source.Play();
            isPlaying = true;
        }
    }

}
