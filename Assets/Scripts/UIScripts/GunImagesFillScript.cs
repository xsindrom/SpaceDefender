using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GunImagesFillScript : MonoBehaviour
{
    public Sprite[] gunSprites;
    public GameObject prefabToInstantiate;
    public GameObject[] childrenGunImages;

    public enum Orientation { horizontal,vertical}
    public Orientation orientation;
    void Start()
    {
        gunSprites = Resources.LoadAll<Sprite>(StringPathsInfo.GUNS_IMAGES_PATH);
        childrenGunImages = new GameObject[gunSprites.Length];
        SwitchOrientation();
        for (int index = 0; index < childrenGunImages.Length; index++)
        {
            childrenGunImages[index] = Instantiate(prefabToInstantiate) as GameObject;
            childrenGunImages[index].SetSprite(gunSprites[index]);
        }
        childrenGunImages.SetNameToObjects(StringNamesInfo.GUN_name);
        childrenGunImages.SetParentToObjects(this.gameObject);
    }

    void SwitchOrientation()
    {
        switch (orientation)
        {
            case Orientation.horizontal: gameObject.SetRectTransformer(0.0f, 0.0f, gunSprites.Length, 1.0f); break;
            case Orientation.vertical: gameObject.SetRectTransformer(0.0f, -gunSprites.Length, 1.0f, 1.0f); break;
        }
    }
}
