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
        _backgroundMusic.mute   = mute;
        _interfaceClick.mute    = mute;
        _bubbleClick.mute       = mute;
    }
}
