using UnityEngine;
using System.Collections;

public class GroupAudioFromScene : MonoBehaviour
{
    #region FIELDS
    [SerializeField]
    private GameObject musicGroup;
    [SerializeField]
    private GameObject effectGroup;
    #endregion
    #region STANDART_EVENTS
    void Awake()
    {
        #region CREATE_GROUPS
        musicGroup = new GameObject(StringNamesInfo.MUSIC_group);
        effectGroup = new GameObject(StringNamesInfo.EFFECT_group);
        #endregion
        #region SET_POSITION_OF_GROUPS
        musicGroup.transform.position = Vector2.zero;
        effectGroup.transform.position = Vector2.zero;
        #endregion
        #region SET_GROUPS
        GameObject.FindGameObjectsWithTag(StringNamesInfo.MUSIC_tag).SetParentToObjects(musicGroup);
        GameObject.FindGameObjectsWithTag(StringNamesInfo.EFFECT_tag).SetParentToObjects(effectGroup);
        #endregion
    }
    #endregion
}
