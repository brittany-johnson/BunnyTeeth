using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;
//For song position tracking, music note spawning, and other song managing
//https://shinerightstudio.com/posts/music-syncing-in-rhythm-games/

class MidiNotes
{
    public long NoteDuration { get; set; }
    public long NoteValue { get; set; }
    public double NoteTick { get; set; }
    public long CurrentQuarterNote { get; set; }
}

public class SongManager : MonoBehaviour
{
    public MidiFileLoader MidiLoader;
    List<MPTKEvent> midiEvents;
    List<MidiNotes> midiNotes = new List<MidiNotes>();
    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller theBeatScroller;
    public static SongManager instance;
    float songPosition; //the current position of the song in seconds
    float songPosInBeats; //the current position of the song in beats
    float secondPerBeat; //the duration of a beat;
    float dspTimeSong; //how much time in seconds has passes since the song started
    public float bpm;
    float[] notes; //holds all the songPosInBeats of notes in the song
    int nextIndex = 0; //the index of the next note to be spawned
    float beatsShownInAdvance = 10;
    public float spawnPos;
    public float removePos;
    public float beatOfThisNote;
    public GameObject letterS_prefab;
    public GameObject letterD_prefab; 
    public GameObject letterF_prefab;
    public GameObject letterJ_prefab;
    public GameObject letterK_prefab;
    public GameObject letterL_prefab;
    private GameObject letterS;
    private GameObject letterD;
    private GameObject letterF;
    private GameObject letterJ;
    private GameObject letterK;
    private GameObject letterL;
    public GameObject Beats;
    public GameObject SpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        AudioListener.pause = true;
        //TODO - create public variable to load song in
        MidiLoader.MPTK_Load();
        midiEvents = MidiLoader.MPTK_ReadMidiEvents();
        getMidiEvent(midiEvents);
        secondPerBeat = 60f / bpm;

        //record the time of the song when it starts
        dspTimeSong = (float) AudioSettings.dspTime;

        foreach (MidiNotes note in midiNotes)
        {
            // Debug.Log(note.NoteValue);
        }

        
    }
    

    // Update is called once per frame
    void Update()
    {
        if(!startPlaying)
        {
            if(Input.anyKeyDown) 
            {
                AudioListener.pause = false;
                startPlaying = true;
                theBeatScroller.hasStarted = true;

                theMusic.Play();
            }
        }
        songPosition = (float) (AudioSettings.dspTime - dspTimeSong); //secs
        songPosInBeats = songPosition / secondPerBeat;
        // 1. check if there are any notes left in the notes array 
        // 2. If there are, then check if the current note is ready to be spawned (If songPosInBeats = 1.5, beatsShownInAdvance = 3 and notes[2] = 2. Is 2 < 4.5? Yes so a new note should be instantiated)
        if (nextIndex < midiNotes.Count && midiNotes[nextIndex].NoteTick < songPosInBeats + beatsShownInAdvance)
        {
            if (!startPlaying) return;
            //NEXT: 
            //need to dynamically calculate y value so that notes do not spawn on top of each other
            // float spawnPosition
            // 
            noteValueToLetterPrefab(midiNotes[nextIndex].NoteValue);
            // letterS.transform.SetParent(Beats.transform);

            //initalize the feilds of the music note

            //NEXT 
            //instantiate object as child of beats so the beat scroller script can be applied✅
            //6 notes are being generated before the song even starts?? fix this. (maybe look back at tutorial to see how the positioning code is)
            //position a spwn point so that the first note and subsequient notes reach when theyre suppose to 
            //move hit and miss functions so that theyre working 
            //based on note value instantiate a different object 
            //based on note length add a tail indicating to hold the key down 
            // Debug.Log(songPosInBeats + beatsShownInAdvance);
            nextIndex++;
        }
    }

    void getMidiEvent(List<MPTKEvent> midiEvents)
    {

        foreach (MPTKEvent midiEvent in midiEvents)
        {   //need to remove events that aren't notes and don't instantiate if its a rest and doesn't have a value
        //mayve do w/ instantiate block
        // Debug.Log("duration: " + note.NoteDuration);
        //     Debug.Log("tick: " + note.NoteTick);
        //     Debug.Log("value: " + note.NoteValue);
            if(midiEvent.Duration > 0)
            {
                midiNotes.Add(new MidiNotes() 
                {
                    NoteDuration=midiEvent.Duration, 
                    NoteValue=midiEvent.Value, 
                    NoteTick=(double)midiEvent.Tick / (double)MidiLoader.MPTK_DeltaTicksPerQuarterNote 
                });
            }
        }

        
    }
// S D F J K L 
// 67, 69, 71, 72, 74, 76, 77, 79, 81, 83, 84, 86 
    void noteValueToLetterPrefab(long value)
    {    
        Debug.Log(value);
        switch (value)
        {
            case 67:
            case 69: 
            case 71:
                letterS = Instantiate(letterS_prefab, new Vector3(letterS_prefab.transform.position.x, SpawnPoint.transform.position.y, 0f), Quaternion.identity ,Beats.transform);
                break;
            case 72:
                letterD = Instantiate(letterD_prefab, new Vector3(letterD_prefab.transform.position.x, SpawnPoint.transform.position.y, 0f), Quaternion.identity ,Beats.transform);
                break;
            case 74:
            case 76:
                letterF = Instantiate(letterF_prefab, new Vector3(letterF_prefab.transform.position.x, SpawnPoint.transform.position.y, 0f), Quaternion.identity ,Beats.transform);
                break;
            case 77:
            case 79:
                letterJ = Instantiate(letterJ_prefab, new Vector3(letterJ_prefab.transform.position.x, SpawnPoint.transform.position.y, 0f), Quaternion.identity ,Beats.transform);
                break;
            case 81:
            case 83:
                letterK = Instantiate(letterK_prefab, new Vector3(letterK_prefab.transform.position.x, SpawnPoint.transform.position.y, 0f), Quaternion.identity ,Beats.transform);
                break;
            case 84:
            case 86:
                letterL = Instantiate(letterL_prefab, new Vector3(letterL_prefab.transform.position.x, SpawnPoint.transform.position.y, 0f), Quaternion.identity ,Beats.transform);
                break;
        }
    }
}

// w -4.33
// s -2.36
// d -0.39
// a -6.25

//Next:
    // current tick / delta ticker per quarter note = current quarter note
    
    // ✅replace BeatGeneration script with SongManager script on MidiFileLoader game object 
    // store midi event data in an array so that I can easily traverse it, pop items off, etc✅
    // for each midi event
        // check if there is a event.note value, if so keep it ✅
        // check tick✅
        // convert tick to currentQuarterNote
        // push currentQuarterNote value, duration, and event.note to notes array 
            // notes = [ [1, 79, 716], [1.5, 76, 387]]
        // finish writing note instantiation code
            // decide which type of note to instantiate based on which note value and duration it is
    
