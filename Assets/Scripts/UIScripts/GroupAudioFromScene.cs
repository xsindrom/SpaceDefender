using UnityEngine;
using System.Collections;

public class GroupAudioFromScene : MonoBehaviour
{
    public GameObject musicGroup;
    public GameObject effectGroup;
    
    void Awake()
    {
        musicGroup = new GameObject(StringNamesInfo.MUSIC_group);
        effectGroup = new GameObject(StringNamesInfo.EFFECT_group);
        musicGroup.transform.position = Vector2.zero;
        effectGroup.transform.position = Vector2.zero;

        GameObject.FindGameObjectsWithTag(StringNamesInfo.MUSIC_tag).SetParentToObjects(musicGroup);
        GameObject.FindGameObjectsWithTag(StringNamesInfo.EFFECT_tag).SetParentToObjects(effectGroup);

    }
    
}
