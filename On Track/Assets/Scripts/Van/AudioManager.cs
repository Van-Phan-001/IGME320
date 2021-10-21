using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    [Header("List of sounds")]
    [SerializeField] private Sound[] sounds;
    public static AudioManager instance;

    private Dictionary<string,Sound> soundsDictionary = new Dictionary<string, Sound>();
    // Start is called before the first frame update
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
}
