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
    #region Singleton definition
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }
    //Code to reference singleton: SceneChanger.Instance;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        foreach (Sound s in sounds)
        {
            s.SetSource(gameObject.AddComponent<AudioSource>());
            s.LinkSource();
            soundsDictionary.Add(s.Name, s); //adds sounds to our dictionary so play found can call via string
        }
    }
    #endregion

    private Dictionary<string,Sound> soundsDictionary = new Dictionary<string, Sound>();
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
