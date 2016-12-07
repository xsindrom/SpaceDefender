using UnityEngine;
using System.Collections;

public class PanelAnimation : MonoBehaviour
{
    public GameObject panelToControll;
    private Animator animationController;
    public static bool isPlayed = false;
    public static GameObject currentPanel = null;
    void Awake()
    {
        isPlayed = false;
    }
    public void ShowPanel()
    {
        PanelAnimation.HideCurrentPanel();
        if (!isPlayed)
        {
            panelToControll.SetActive(true);
            currentPanel = panelToControll;
            if (!animationController)
            {
                animationController = panelToControll.GetComponent<Animator>();
            }
            animationController.Play(StringNamesInfo.SHOWPANEL_animation);
            isPlayed = true;
        }
    }
    public void HidePanel()
    {
        if (isPlayed)
        {
            if (!animationController)
            {
                animationController = panelToControll.GetComponent<Animator>();
            }
            animationController.Play(StringNamesInfo.HIDEPANEL_animation);
            StartCoroutine(WaitBeforeDeactivate());
            isPlayed = false;
            currentPanel = null;
        }
    }
    public static void HideCurrentPanel()
    {
        if (currentPanel)
        {
            Animator animatorofCurrentPanel = currentPanel.GetComponent<Animator>();
            animatorofCurrentPanel.Play(StringNamesInfo.HIDEPANEL_animation);
            isPlayed = false;
            currentPanel = null;
        }
    }
    IEnumerator WaitBeforeDeactivate()
    {
        yield return new WaitForSeconds(animationController.GetCurrentAnimatorStateInfo(0).length);
        panelToControll.SetActive(false);
        
    }
}
