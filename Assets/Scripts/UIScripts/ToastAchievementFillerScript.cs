using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToastAchievementFillerScript : MonoBehaviour
{
    public List<GameObject> fillerObjects = new List<GameObject>();
    public GameObject prefabToInstantiate;
    public int index;
    void Start()
    {
        index = 0;
    }
    IEnumerator DestroyFiller(GameObject filler)
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(filler);
        RemoveFillerObject(filler);
    }
    public void AddFillerObject(GameObject filler)
    {
        fillerObjects.Add(filler);
        filler.transform.SetParent(transform);
        filler.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        filler.SetRectTransformer(0.0f, 0.8f - 0.2f * index, 1.0f, 1.0f - 0.2f * index);
        index++;
        StartCoroutine(DestroyFiller(filler));
    }
    public void RemoveFillerObject(GameObject filler)
    {
        fillerObjects.Remove(filler);
        index--;
        if (index < 0) { index = 0; }
    }
}