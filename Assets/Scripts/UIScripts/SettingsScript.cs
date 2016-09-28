using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsScript : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    private Slider musicSoundSlider;
    [SerializeField]
    private Slider effectSoundSlider;

    [SerializeField]
    private GameObject[] musicSoundSourceObjects;
    [SerializeField]
    private GameObject[] effectSoundSourceObjects;

    [SerializeField]
    private AudioSource[] musicSoundSources;
    [SerializeField]
    private AudioSource[] effectSoundSources;
    #endregion
    #region STANDART_EVENTS
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
            musicSoundSlider.onValueChanged.AddListener(delegate { OnValueChange(musicSoundSlider, musicSoundSources); });
        }
        #endregion
        #region SET_EFFECT_SETTINGS
        if (effectSoundSourceObjects.Length != 0)
        {
            effectSoundSources = new AudioSource[effectSoundSourceObjects.Length];
            effectSoundSources.CacheComponents<AudioSource>(effectSoundSourceObjects);
            effectSoundSlider.onValueChanged.AddListener(delegate { OnValueChange(effectSoundSlider, effectSoundSources); });
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
