using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterBtn : MonoBehaviour
{
    [SerializeField] private Image bg;
    [SerializeField] private Image icon;
    [SerializeField] private Sprite isOnBg;
    [SerializeField] private Color isOnIconColor;

    private Sprite isOffBg;
    private Color isOffIconColor;

	private void Awake()
	{
        isOffBg = bg.sprite;
        isOffIconColor = icon.color;
        GetComponent<Toggle>().onValueChanged.AddListener(OnToggleValueChanged);
	}

	private void OnToggleValueChanged(bool isOn)
	{
        bg.sprite = isOn ? isOnBg : isOffBg;
        icon.color = isOn ? isOnIconColor : isOffIconColor;
	}
}
