using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;
    public bool hasStarted;
    // Start is called before the first frame update
    void Start()
    {
        // BPM / 60 seconds will give us how fast the notes will move per second
        beatTempo = beatTempo / 60f; 
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted) 
        {
    
        } else 
        {
            //maybe instead of deltaTime use AudioSettings.dspTime
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }
}
