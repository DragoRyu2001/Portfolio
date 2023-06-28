using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _backgroundMusic;
    [SerializeField]
    private AudioSource _interfaceClick;
    [SerializeField]
    private AudioSource _bubbleClick;
    [SerializeField]
    private AudioSource _noteClick;
    [SerializeField]
    private List<AudioClip> _planetNotes;
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
        _interfaceClick.Play();
    }
    public void UIClick()
    {
        _bubbleClick.Play();
    }
    public void MuteAudio(bool mute)
    {
        _backgroundMusic.mute = mute;
        _interfaceClick.mute = mute;
        _bubbleClick.mute = mute;
    }
    private int noteNumber = 0;
    public void PlanetNote()
    {
        //_noteClick.clip = _planetNotes[0];
        _noteClick.pitch = 1.5f + ((float)(noteNumber) / 8f);
        noteNumber = (noteNumber + 1) % 3;
        _noteClick.Play();
    }
}
