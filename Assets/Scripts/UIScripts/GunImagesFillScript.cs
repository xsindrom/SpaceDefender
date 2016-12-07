using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum Orientation { horizontal, vertical }
public class GunImagesFillScript : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    private Sprite[] gunSprites;
    private GameObject[] childrenGunImages;
    public Orientation orientation;
    void Awake()
    {
        gunSprites = Resources.LoadAll<Sprite>(StringPathsInfo.GUNS_IMAGES_PATH);
        
        childrenGunImages = new GameObject[gunSprites.Length];

        SethOrientation();
        for (int index = 0; index < childrenGunImages.Length; index++)
        {
            childrenGunImages[index] = Instantiate(prefabToInstantiate) as GameObject;
            childrenGunImages[index].SetSpriteToImage(gunSprites[index]);
        }
        childrenGunImages.SetNameToObjects(StringNamesInfo.GUN_name);
        childrenGunImages.SetParentToObjects(this.gameObject);
        for (int index = 0; index < childrenGunImages.Length; index++)
        {
            childrenGunImages[index].SetRectTransformer(0.0f,1 - (float)(index + 1) / childrenGunImages.Length,
                                                        1.0f, 1 - (float)(index) / childrenGunImages.Length); 
        }
    }
    void SethOrientation()
    {
        switch (orientation)
        {
            case Orientation.horizontal: gameObject.SetRectTransformer(0.0f, 0.0f, gunSprites.Length, 1.0f); break;
            case Orientation.vertical: gameObject.SetRectTransformer(0.0f, -gunSprites.Length, 1.0f, 1.0f); break;
        }
    }
}
