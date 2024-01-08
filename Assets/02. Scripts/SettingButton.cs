using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingButton : MonoBehaviour
{
    public GameObject settingPanel;

    private void Awake()
    {
        HideSettingPanel();
    }

    public void ToggleActive()
    {
        if(settingPanel.activeSelf == true)
        {
            HideSettingPanel();
        }
        else
        {
            ShowSettingPanel();
        }
    }

    public void ShowSettingPanel()
    {
        settingPanel.SetActive(true);
    }

    public void HideSettingPanel()
    {
        settingPanel.SetActive(false);
    }
}
