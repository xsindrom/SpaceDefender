using UnityEngine;
using System.Collections;

public class PanelAnimation : MonoBehaviour
{
    private Animator animationController;
    public bool isPlayed;
    void Start()
    {
        animationController = gameObject.GetComponent<Animator>();
    }

    public void ShowPanel()
    {
        animationController.Play(StringNamesInfo.SHOWPANEL_animation);
    }
    public void HidePanel()
    {
        animationController.Play(StringNamesInfo.HIDEPANEL_animation);
    }
}
