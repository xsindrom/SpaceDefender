using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsScript : MonoBehaviour
{
    public Slider musicSoundSlider;
    public Slider effectSoundSlider;

    public GameObject[] musicSoundSourceObjects;
    public GameObject[] effectSoundSourceObjects;

    public AudioSource[] musicSoundSources;
    public AudioSource[] effectSoundSources;
    void Start()
    {
        musicSoundSourceObjects = GameObject.Find(StringNamesInfo.MUSIC_group).GetChilds();
        effectSoundSourceObjects = GameObject.Find(StringNamesInfo.EFFECT_group).GetChilds();
        if (musicSoundSourceObjects.Length != 0)
        {
            musicSoundSources = new AudioSource[musicSoundSourceObjects.Length];
            musicSoundSources.CacheComponents<AudioSource>(musicSoundSourceObjects);
            musicSoundSlider.onValueChanged.AddListener(delegate { OnValueChange(musicSoundSlider, musicSoundSources); });
        }
        if (effectSoundSourceObjects.Length != 0)
        {
            effectSoundSources = new AudioSource[effectSoundSourceObjects.Length];
            effectSoundSources.CacheComponents<AudioSource>(effectSoundSourceObjects);
            effectSoundSlider.onValueChanged.AddListener(delegate { OnValueChange(effectSoundSlider, effectSoundSources); });
        }
    }

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
}
