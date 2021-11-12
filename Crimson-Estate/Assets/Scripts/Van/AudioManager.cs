using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

/// <summary>
/// Singleton that contains a list of all audio
/// To play an audio clip call play function with the audio's name
/// </summary>
public class AudioManager : MonoBehaviour
{
    #region Fields
    [Header("List of sounds")]
    [SerializeField] private Sound[] sounds;
    public static AudioManager instance;

    private Dictionary<string,Sound> soundsDictionary = new Dictionary<string, Sound>();
    #endregion

    #region Awake
    void Awake()
    {
        instance = this;
        foreach (Sound s in sounds)
        {
            s.SetSource(gameObject.AddComponent<AudioSource>());
            s.LinkSource();
            soundsDictionary.Add(s.Name, s); //adds sounds to our dictionary so play found can call via string
        }
    }
    #endregion

    #region Functions
    public void PlaySound(string _soundName)
    {
        try
        {
            soundsDictionary[_soundName].PlaySound();
        }
        catch (System.Exception)
        {
            Debug.Log("Can't find sound!");
            throw;
        }
    }
    #endregion

}
