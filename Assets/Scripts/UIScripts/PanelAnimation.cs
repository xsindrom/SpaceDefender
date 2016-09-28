using UnityEngine;
using System.Collections;

public class PanelAnimation : MonoBehaviour
{
    public GameObject panelToControll;
    #region COMPONENTS_TO_CACHE
    private Animator animationController;
    #endregion
    #region STATES
    public static bool isPlayed = false;
    #endregion
    #region STANDART_EVENTS
    void Awake()
    {
        isPlayed = false;
    }
    #endregion
    #region LOGIC
    public void ShowPanel()
    {
        if (!isPlayed)
        {
            panelToControll.SetActive(true);
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
        }
    }
    #endregion
    IEnumerator WaitBeforeDeactivate()
    {
        yield return new WaitForSeconds(animationController.GetCurrentAnimatorStateInfo(0).length);
        panelToControll.SetActive(false);
    }
}
