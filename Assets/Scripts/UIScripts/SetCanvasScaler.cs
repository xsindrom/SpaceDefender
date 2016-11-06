using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SetCanvasScaler : MonoBehaviour
{
    public CanvasScaler thisCanvasScaler = null;
    public Component[] childrenComponents;
    public static float scale;
    public static int[][] fontSize = null;
    public static int baseFont = 40;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (fontSize == null)
        {
            fontSize = new int[SceneManager.sceneCountInBuildSettings][];
        }
        thisCanvasScaler = gameObject.GetComponent<CanvasScaler>();
        thisCanvasScaler.referenceResolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        childrenComponents = gameObject.GetComponentsInChildren(typeof(Text), true);
        scale = gameObject.transform.localScale.x;
        if (fontSize[SceneManager.GetActiveScene().buildIndex] == null)
        {
            fontSize[SceneManager.GetActiveScene().buildIndex] = new int[childrenComponents.Length];
            for (int index = childrenComponents.Length - 1; index >= 0; index--)
            {
                fontSize[SceneManager.GetActiveScene().buildIndex][index] = ((Text)childrenComponents[index]).resizeTextMaxSize;
            }
        }
        for (int index = childrenComponents.Length - 1; index >= 0; index--)
        {
            ((Text)childrenComponents[index]).resizeTextMaxSize = (int)(fontSize[SceneManager.GetActiveScene().buildIndex][index] * scale);
        }
    }
}
