using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ContentFillerScript : MonoBehaviour
{
    public List<GameObject> childObjects = new List<GameObject>();
    [Header("Anchors")]
    public Vector2 anchorMin = new Vector2();
    public Vector2 anchorMax = new Vector2();
    public Orientation orientation;
    void Start()
    {
        StartCoroutine(LateStart());
    }
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        childObjects = new List<GameObject>(transform.gameObject.GetChilds());
        for (int index = 0, size = childObjects.Count; index < size; index++)
        {
            SetAnchores(index, childObjects[index], size, 10);
        }
    }
    private void SetAnchores(int index,GameObject toSet, int elementLen, int norma)
    {
        Vector2 toSetVecMin = new Vector2();
        Vector2 toSetVecMax = new Vector2();
        switch (orientation)
        {
            case Orientation.horizontal:
                toSetVecMin = new Vector2(anchorMin.x + (anchorMax.x - anchorMin.x) * index, anchorMin.y);
                toSetVecMax = new Vector2(anchorMax.x + (anchorMax.x - anchorMin.x) * index, anchorMax.y);
                if (norma < elementLen)
                {
                    toSetVecMin.y = Mathf.Round((1.0f + (toSetVecMin.y - 1.0f) * (float)norma / elementLen) * 10000.0f) / 10000.0f;
                    toSetVecMax.y = Mathf.Round((1.0f + (toSetVecMax.y - 1.0f) * (float)norma / elementLen) * 10000.0f) / 10000.0f;
                }
                toSet.SetRectTransformer(toSetVecMin.x, toSetVecMin.y, toSetVecMax.x, toSetVecMax.y);
                
                break;
            case Orientation.vertical:
                toSetVecMin = new Vector2(anchorMin.x, anchorMin.y - (anchorMax.y - anchorMin.y) * index);
                toSetVecMax = new Vector2(anchorMax.x, anchorMax.y - (anchorMax.y - anchorMin.y) * index);
                if (norma < elementLen)
                {
                    toSetVecMin.y = Mathf.Round((1.0f + (toSetVecMin.y - 1.0f) * (float)norma / elementLen) * 10000.0f) / 10000.0f;
                    toSetVecMax.y = Mathf.Round((1.0f + (toSetVecMax.y - 1.0f) * (float)norma / elementLen) * 10000.0f) / 10000.0f;
                }
                toSet.SetRectTransformer(toSetVecMin.x, toSetVecMin.y, toSetVecMax.x, toSetVecMax.y);
                break;
        }
        childObjects[index].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
