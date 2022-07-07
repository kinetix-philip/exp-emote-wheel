using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void WheelMemberEventHandler(WheelMember sender, EmoteInfo info); 
public class WheelMember : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text rarityText;
    [SerializeField] private Image hover;
    [SerializeField] private Image vfxIcon;
    [SerializeField] private Image elementIcon;
    [SerializeField] private Button button;
    [SerializeField] private float moveDuration;
    [SerializeField] private float moveSpeed;
	[SerializeField] private List<ElementIconStruct> elementIcons;

	private int indexOnWheel;
	private bool isOnWheel;
	private bool hasVFX;
	private float elapsedTime;

	public event WheelMemberEventHandler OnEmoteSelected;
	private Action DoAction;

	private Vector3 startPos;

	private EmoteInfo emoteInfo;

	public int IndexOnWheel => indexOnWheel;

	public bool IsOnWheel => isOnWheel;

	private void Awake()
	{
		button.onClick.AddListener(Button_OnClick);
	}

	private void Button_OnClick()
	{
		if (!isOnWheel) return;

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
		rarityText.text = info.EmoteRarity.ToString();
		isOnWheel = info.IsOnWheel;
		indexOnWheel = info.IndexOnWheel;
		hasVFX = info.HasVFX;
		SelectElementIcon();
		SetModeWait();
	}

	private void SelectElementIcon()
	{
		foreach (ElementIconStruct item in elementIcons)
		{
			if(item.element == emoteInfo.EmoteElement)
			{
				elementIcon.sprite = item.elementIcon;
				break;
			}
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!isOnWheel) return;
		hover.gameObject.SetActive(true);
		vfxIcon.gameObject.SetActive(hasVFX && true);
		elementIcon.gameObject.SetActive(true);
		rarityText.gameObject.SetActive(true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!isOnWheel) return;
		hover.gameObject.SetActive(false);
		elementIcon.gameObject.SetActive(false);
		rarityText.gameObject.SetActive(false);
		if (hasVFX) vfxIcon.gameObject.SetActive(false);
		
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
		startPos = transform.localPosition;

		elapsedTime = 0;
		DoAction = DoActionMove;
	}

	private void DoActionWait()
	{

	}

	private void DoActionMove()
	{
		elapsedTime += Time.deltaTime * moveSpeed;

		transform.localPosition = Vector3.Lerp(startPos, Vector3.zero, elapsedTime / moveDuration);

		if(elapsedTime >= moveDuration)
		{
			elapsedTime = 0;
			transform.localPosition = Vector3.zero;
			Debug.Log("finish moving");
			SetModeWait();
		}
	}
}
