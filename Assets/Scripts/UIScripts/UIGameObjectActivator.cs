using UnityEngine;
using System.Collections;

public class UIGameObjectActivator : MonoBehaviour
{
    public GameObject loadPanelButton;

    private void ActivateButton(GameObject toActivate, bool condition)
    {
        toActivate.SetActive(!condition);
    }
    
    public void OnLoadPanelButtonActivation()
    {
        if (loadPanelButton != null)
        {
            ActivateButton(loadPanelButton, PlayerStats.Current.Equals(PlayerStats.Empty));
        }
    }
}
