using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.Collections;
using System.IO;
using System.Collections.Generic;
public static class MyExtensionMethods
{
    public static void AddComponentsToGameObject(this GameObject whereToAdd, params Component[] components)
    {
        foreach (var component in components)
        {
            whereToAdd.AddComponent(component.GetType());
        }
    }
    public static void SetRectTransformer(this GameObject whereToGet, float minAnchorX, float minAnchorY, float maxAnchorX, float maxAnchorY)
    {
        RectTransform rectTransformer = whereToGet.GetComponent<RectTransform>();
        if (rectTransformer == null) { return; }
        rectTransformer.anchorMin = new Vector2(minAnchorX, minAnchorY);
        rectTransformer.anchorMax = new Vector2(maxAnchorX, maxAnchorY);
        rectTransformer.offsetMin = new Vector2(0.0f, 0.0f);
        rectTransformer.offsetMax = new Vector2(0.0f, 0.0f);
    }
    public static void SetSprite(this GameObject whereToget, Sprite spriteToSet)
    {
        Image image = whereToget.GetComponent<Image>();
        if (image == null) { return; }
        image.sprite = spriteToSet;
    }

    public static void SetParentToObjects(this GameObject[] childObjects, GameObject parent)
    {
        foreach (GameObject child in childObjects)
        {
            child.transform.SetParent(parent.transform);
        }
    }
    public static void SetNameToObjects(this GameObject[] whereToSet, string name)
    {
        for (int index = 0; index < whereToSet.Length; index++)
        {
            whereToSet[index].name = name + index;
        }
    }
    public static GameObject[] GetChilds(this GameObject parent)
    {
        GameObject[] childs = new GameObject[parent.transform.childCount];
        for (int index = 0; index < childs.Length; index++)
        {
            childs[index] = parent.transform.GetChild(index).gameObject;
        }
        return childs;
    }
    public static void CacheComponents<T>(this T[] whereToCache,GameObject[] from)
    {
        for (int index = 0; index < from.Length; index++)
        {
            whereToCache[index] = from[index].GetComponent<T>();
        }
    }
    public static void SaveJDataToList<T>(this JsonData jDataList, string jsonName,params T[] objectsToSaveInJson)
    {
        foreach (var objectToSaveInJson in objectsToSaveInJson as IList)
        {
            jDataList.Add(JsonMapper.ToObject(JsonMapper.ToJson(objectToSaveInJson)));
        }
        File.WriteAllText(jsonName, jDataList.ToJson());
    }
    public static void SaveJData<T>(this JsonData jData, string jsonName, T objectToSaveInJson)
    {
        jData = JsonMapper.ToObject(JsonMapper.ToJson(objectToSaveInJson));
        File.WriteAllText(jsonName, jData.ToJson());
    }
}
