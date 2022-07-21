using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void ShortCutEventHandler(ShortCut sender);
public class ShortCut : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private Text txtBtn;
    [SerializeField] private Image icon;

    public event ShortCutEventHandler OnClick;
    // Start is called before the first frame update
    void Awake()
    {
        btn.onClick.AddListener(Btn_OnClick);
    }

    public void Init(InputStruct data)
	{
        txtBtn.text = data.input.ToString();
        icon.sprite = data.shortCutIcon;
	}

	private void Btn_OnClick()
	{
        OnClick?.Invoke(this);
	}
}
