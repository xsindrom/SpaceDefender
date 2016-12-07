using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;

public class SettingsScript : MonoBehaviour
{
    public Slider musicSoundSlider = null; //
    public Slider effectSoundSlider = null;//

    private GameObject[] musicSoundSourceObjects;
    private GameObject[] effectSoundSourceObjects;

    private AudioSource[] musicSoundSources;
    private AudioSource[] effectSoundSources;
    [SerializeField]
    private static float effectVolume = 1.0f;
    public static float EffectVolume
    {
        get { return effectVolume; }
        set { effectVolume = value; }
    }
    [SerializeField]
    private static float musicVolume = 1.0f;
    public static float MusicVolume
    {
        get { return musicVolume; }
        set { musicVolume = value; }
    }
    [Serializable]
    public class VolumeSettings
    {
        [SerializeField]
        public float effectVolume = 1.0f;
        [SerializeField]
        public float musicVolume = 1.0f;
        public VolumeSettings()
        {

        }
        public VolumeSettings(float effectVolume, float musicVolume)
        {
            this.effectVolume = effectVolume;
            this.musicVolume = musicVolume;
        }
    }
    void Awake()
    {
        if (!effectSoundSlider)
        {
            GameObject effectObject = GameObject.Find(StringNamesInfo.EFFECTSLIDER_name);
            if (effectObject)
            {
                effectSoundSlider = effectObject.GetComponent<Slider>();
            }
        }
        if (!musicSoundSlider)
        {
            GameObject musicObject = GameObject.Find(StringNamesInfo.MUSICSLIDER_name);
            if (musicObject)
            {
                musicSoundSlider = musicObject.GetComponent<Slider>();
            }
        }
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
        musicSoundSourceObjects = GameObject.Find(StringNamesInfo.MUSIC_group).GetChilds();
        effectSoundSourceObjects = GameObject.Find(StringNamesInfo.EFFECT_group).GetChilds();
        Debug.Log("Music objects len: " + musicSoundSourceObjects.Length + " Effect objects len: " + effectSoundSourceObjects.Length);
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
                    music.volume = music.volume;
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

    public void OnValueChange(Slider slider,AudioSource[] values)
    {
        for (int index = 0; index < values.Length; index++)
        {
            values[index].volume =  slider.value;
        }
    }

    public void OnMuteChange()
    {
        musicSoundSlider.value = 0.0f;
        effectSoundSlider.value = 0.0f;
    }
}
