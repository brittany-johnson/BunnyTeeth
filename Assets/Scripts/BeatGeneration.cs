using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;

//GOAL 
//initialize a new beat prefab in the scene for each midi event

//STEPS
//1. Log anything from MidiPlayerTK []
//2. Log each midi note to the console as its played

//read midi events in real time not ticks 
namespace MPTK.NAudio.Midi 
{
public class BeatGeneration : MonoBehaviour
{
    public MidiFileLoader MidiLoader;
    public MidiFilePlayer MidiFilePlayer;

    // Start is called before the first frame update
    void Start()
    {
        MidiLoader.MPTK_LogEvents = true;
        MidiLoader.MPTK_Load();
        Debug.Log(MidiLoader.MPTK_MidiName);
        List<MPTKEvent> events = MidiLoader.MPTK_ReadMidiEvents();
        foreach (MPTKEvent evt in events)
        {
            Debug.Log(evt);
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    
}
}

//NOTES
//First thing I was finally able to do was change the Log Midi events in the unity editor to true, meaning from unchecked box to checked by writing this line MidiLoader.MPTK_LogEvents = true; I added the beat generation script to the midi file loader prefab in my scene 
//I then came to realize two things. 1. By having acacess to MidiLoader in my file, w/ vscode I can see all available properties and methods while typing 2. I can use this to see what info is available to me via the midi output 
//I tested this by logging the midievents, based on error saw that I had to load the midi file first via a function, then when that worked I logged the midiname, then found an example of iterating through the midi events, realized it was an enumerable output and logged each item w/ a for each 