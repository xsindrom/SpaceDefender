using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GunImagesFillScript : MonoBehaviour
{
    public enum Orientation { horizontal, vertical }
    #region FIELDS
    public GameObject prefabToInstantiate;
    private Sprite[] gunSprites;
    private GameObject[] childrenGunImages;
    public Orientation orientation;
    #endregion
    #region STANDART_EVENTS
    void Awake()
    {
        #region GET_DATA
        gunSprites = Resources.LoadAll<Sprite>(StringPathsInfo.GUNS_IMAGES_PATH);
        GunRendererScript.baseSprite = Resources.LoadAll<Sprite>(StringPathsInfo.GUNS_base_IMAGES_PATH);
        GunRendererScript.barrelSprite = Resources.LoadAll<Sprite>(StringPathsInfo.GUNS_barrel_IMAGES_PATH);
        #endregion
        #region CREATE_IMAGES
        childrenGunImages = new GameObject[gunSprites.Length];

        #endregion
        SethOrientation();
        #region FILL_IMAGES
        for (int index = 0; index < childrenGunImages.Length; index++)
        {
            childrenGunImages[index] = Instantiate(prefabToInstantiate) as GameObject;
            childrenGunImages[index].SetSpriteToImage(gunSprites[index]);
        }
        #endregion
        #region INIT_IMAGES
        childrenGunImages.SetNameToObjects(StringNamesInfo.GUN_name);
        childrenGunImages.SetParentToObjects(this.gameObject);
        for (int index = 0; index < childrenGunImages.Length; index++)
        {
            childrenGunImages[index].SetRectTransformer(0.0f,1 - (float)(index + 1) / childrenGunImages.Length,
                                                        1.0f, 1 - (float)(index) / childrenGunImages.Length); 
        }
        #endregion
    }
    #endregion
    #region LOGIC
    void SethOrientation()
    {
        switch (orientation)
        {
            case Orientation.horizontal: gameObject.SetRectTransformer(0.0f, 0.0f, gunSprites.Length, 1.0f); break;
            case Orientation.vertical: gameObject.SetRectTransformer(0.0f, -gunSprites.Length, 1.0f, 1.0f); break;
        }
    }
    #endregion
}
