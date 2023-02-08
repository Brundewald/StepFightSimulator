using System;
using System.Collections.Generic;
using FightSimulator;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private List<BaseScreenView> _screens;

    private void Awake()
    {
        foreach (var screen in _screens)
        {
            screen.SwitchScreen += OnScreenSwitch;
        }
    }

    private void OnScreenSwitch(ScreenType screenType)
    {
        foreach (var screen in _screens)
        {
            if (!screen.Type.Equals(screenType)) continue;
            screen.gameObject.SetActive(true);
            break;
        }
    }
}
