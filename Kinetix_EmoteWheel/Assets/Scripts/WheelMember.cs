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
    [SerializeField] private float moveDuration;
    [SerializeField] private float moveSpeed;

	private int indexOnWheel;
	private bool isOnWheel;
	private float elapsedTime;

	public event WheelMemberEventHandler OnEmoteSelected;
	private Action DoAction;

	private EmoteInfo emoteInfo;

	public int IndexOnWheel => indexOnWheel;

	public bool IsOnWheel => isOnWheel;

	private void Awake()
	{
		button.onClick.AddListener(Button_OnClick);
	}

	private void Button_OnClick()
	{
		OnEmoteSelected?.Invoke(this, emoteInfo);
		hover.gameObject.SetActive(false);

	}

	public void ChangeIndex(int newIndex, bool isOnWheel = true)
	{
		indexOnWheel = newIndex;
		this.isOnWheel = isOnWheel;
	}

	public void Init(EmoteInfo info)
	{
		emoteInfo = info;
		nameText.text = info.EmoteName;
		isOnWheel = info.IsOnWheel;
		indexOnWheel = info.IndexOnWheel;
		SetModeWait();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		hover.gameObject.SetActive(true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		hover.gameObject.SetActive(false);
	}

	private void Update()
	{
		DoAction();
	}

	private void SetModeWait()
	{
		DoAction = DoActionWait;
	}

	public void SetModeMove()
	{
		DoAction = DoActionMove;
	}

	private void DoActionWait()
	{

	}

	private void DoActionMove()
	{
		elapsedTime += Time.deltaTime * moveSpeed;

		if(elapsedTime >= moveDuration)
		{
			elapsedTime = 0;
			Debug.Log("finish moving");
			SetModeWait();
		}
	}
}
