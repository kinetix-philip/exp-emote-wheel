using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void WheelMemberEventHandler(WheelMember sender, EmoteInfo info); 
public class WheelMember : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text nameText;
    [SerializeField] private Image hover;
    [SerializeField] private Button button;

	public event WheelMemberEventHandler OnEmoteSelected;

	private EmoteInfo emoteInfo;

	private void Awake()
	{
		button.onClick.AddListener(Button_OnClick);
	}

	private void Button_OnClick()
	{
		OnEmoteSelected?.Invoke(this, emoteInfo);

	}

	public void Init(EmoteInfo info)
	{
		emoteInfo = info;
		nameText.text = info.EmoteName;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		hover.gameObject.SetActive(true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		hover.gameObject.SetActive(false);
	}
}
