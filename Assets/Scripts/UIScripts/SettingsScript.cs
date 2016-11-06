using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class SettingsScript : MonoBehaviour
{
    #region FIELDS
    private Slider musicSoundSlider = null;
    private Slider effectSoundSlider = null;

    private GameObject[] musicSoundSourceObjects;
    private GameObject[] effectSoundSourceObjects;

    private AudioSource[] musicSoundSources;
    private AudioSource[] effectSoundSources;
    [SerializeField]
    private static float effectVolume = 1.0f;
    [SerializeField]
    private static float musicVolume = 1.0f;
    #endregion
    #region STANDART_EVENTS
    void Awake()
    {
        if (effectVolume != 1.0f)
        {
            if (effectSoundSlider)
            {
                effectSoundSlider.value = effectVolume;
            }
        }
        if (musicVolume != 1.0f)
        {
            if (musicSoundSlider)
            {
                musicSoundSlider.value = musicVolume;
            }
        }
    }
    
    void Start()
    {
        #region FIND_OBJECTS
        musicSoundSourceObjects = GameObject.Find(StringNamesInfo.MUSIC_group).GetChilds();
        effectSoundSourceObjects = GameObject.Find(StringNamesInfo.EFFECT_group).GetChilds();
        #endregion
        #region SET_MUSIC_SETTINGS
        if (musicSoundSourceObjects.Length != 0)
        {
            musicSoundSources = new AudioSource[musicSoundSourceObjects.Length];
            musicSoundSources.CacheComponents<AudioSource>(musicSoundSourceObjects);
            if (musicSoundSlider)
            {
                musicSoundSlider.onValueChanged.AddListener(delegate { OnValueChange(musicSoundSlider, musicSoundSources); });
                musicSoundSlider.onValueChanged.AddListener(delegate { musicVolume = musicSoundSlider.value; });
            }
            else
            {
                foreach (var music in musicSoundSources)
                {
                    music.volume = musicVolume;
                }
            }
        }
        #endregion
        #region SET_EFFECT_SETTINGS
        if (effectSoundSourceObjects.Length != 0)
        {
            effectSoundSources = new AudioSource[effectSoundSourceObjects.Length];
            effectSoundSources.CacheComponents<AudioSource>(effectSoundSourceObjects);
            if (effectSoundSlider)
            {
                effectSoundSlider.onValueChanged.AddListener(delegate { OnValueChange(effectSoundSlider, effectSoundSources); });
                effectSoundSlider.onValueChanged.AddListener(delegate { effectVolume = effectSoundSlider.value; });
            }
            else
            {
                foreach (var effect in effectSoundSources)
                {
                    effect.volume = effectVolume;
                }
            }
        }
        #endregion
    }
    #endregion

    #region EVENTS
    public void OnValueChange(Slider slider,AudioSource[] values)
    {
        for (int index = 0; index < values.Length; index++)
        {
            values[index].volume = slider.value;
        }
    }

    public void OnMuteChange()
    {
        musicSoundSlider.value = 0.0f;
        effectSoundSlider.value = 0.0f;
    }
    #endregion
}
