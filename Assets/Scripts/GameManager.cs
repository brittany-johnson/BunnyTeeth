using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller theBeatScroller; 
    public static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        // if(!startPlaying)
        // {
        //     if(Input.anyKeyDown) 
        //     {
        //         startPlaying = true;
        //         theBeatScroller.hasStarted = true;

        //         theMusic.Play();
        //     }
        // }
    }

    public void NoteHit()
    {
        Debug.Log("Hit");
    }

    public void NoteMissed()
    {
        Debug.Log("Miss");
    }
}
