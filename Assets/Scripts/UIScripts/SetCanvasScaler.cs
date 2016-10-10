﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SetCanvasScaler : MonoBehaviour
{
    public CanvasScaler thisCanvasScaler = null;
    public Component[] childrenComponents;
    public float scale;
    public static int[] fontSize = null;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
    void OnSceneLoad(Scene scene,LoadSceneMode mode)
    {
        thisCanvasScaler = gameObject.GetComponent<CanvasScaler>();
        thisCanvasScaler.referenceResolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        childrenComponents = gameObject.GetComponentsInChildren(typeof(Text), true);
        scale = gameObject.transform.localScale.x;
        if (fontSize == null)
        {
            fontSize = new int[childrenComponents.Length];
            for (int index = fontSize.Length - 1; index > -1; index-- )
            {
                fontSize[index] = ((Text)childrenComponents[index]).resizeTextMaxSize;
            }
        }
        for (int index = childrenComponents.Length - 1; index > -1; index--)
        {
            ((Text)childrenComponents[index]).resizeTextMaxSize = (int)(fontSize[index] * scale);
        }
    }
}
