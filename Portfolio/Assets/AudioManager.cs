using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    [FormerlySerializedAs("backgroundMusic")] [FormerlySerializedAs("_backgroundMusic")] [SerializeField]
    private AudioSource BackgroundMusic;
    [FormerlySerializedAs("interfaceClick")] [FormerlySerializedAs("_interfaceClick")] [SerializeField]
    private AudioSource InterfaceClick;
    [FormerlySerializedAs("bubbleClick")] [FormerlySerializedAs("_bubbleClick")] [SerializeField]
    private AudioSource BubbleClick;
    [FormerlySerializedAs("noteClick")] [FormerlySerializedAs("_noteClick")] [SerializeField]
    private AudioSource NoteClick;
    [FormerlySerializedAs("planetNotes")] [FormerlySerializedAs("_planetNotes")] [SerializeField]
    private List<AudioClip> PlanetNotes;
    public static AudioManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(Instance);
            Instance = this;
        }
    }
    public void StarClick()
    {
        InterfaceClick.Play();
    }
    public void UIClick()
    {
        BubbleClick.Play();
    }
    public void MuteAudio(bool mute)
    {
        BackgroundMusic.mute = mute;
        InterfaceClick.mute = mute;
        BubbleClick.mute = mute;
    }
    private int _noteNumber = 0;
    public void PlanetNote()
    {
        ////_noteClick.clip = _planetNotes[0];
        //NoteClick.pitch = 1.5f + ((float)(_noteNumber) / 8f);
        //_noteNumber = (_noteNumber + 1) % 3;
        //NoteClick.Play();
    }
}
