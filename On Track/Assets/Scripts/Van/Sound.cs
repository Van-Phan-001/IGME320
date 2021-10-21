using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


/// <summary>
/// Serialized game object that contains the .wav clip of the audio that needs to be played
/// </summary>
[System.Serializable] public class Sound
{
    #region Fields
    private AudioSource source; //source we play audio from

    [Header("Sound file")]
    [SerializeField] private AudioClip clip;

    [Header("Audio values")]
    [SerializeField] [Range(0f, 1f)] private float volume = 0.5f;
    [SerializeField] [Range(.1f, 3f)] private float pitch = 1f;
    [SerializeField] private bool loop = false;
    [SerializeField] private string name = "nameless";
    #endregion

    #region Properties
    public string Name
    {
        get { return name; }
    }
    #endregion

    #region Functions
    //assigns an object source for audio to play from
    public void SetSource(AudioSource _source)
    {
        source = _source;
    }

    //attaches sound attributes to source player
    public void LinkSource()
    {
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
        source.loop = loop;
    }

    public void PlaySound()
    {
        source.Play();
    }
    #endregion
}
