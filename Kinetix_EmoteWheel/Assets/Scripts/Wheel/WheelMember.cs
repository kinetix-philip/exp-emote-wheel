using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void WheelMemberEventHandler(WheelMember sender, EmoteInfo info); 
public class WheelMember : EmoteVisual
{
    [SerializeField] private Image hover;
    [SerializeField] private float moveDuration;
    [SerializeField] private float moveSpeed;

	private float elapsedTime;

	public event WheelMemberEventHandler OnEmoteSelected;
	private Action DoAction;

	private Vector3 startPos;
	private Quaternion startRot;
	private bool isHovered;

	public int IndexOnWheel => indexOnWheel;

	public bool IsOnWheel => isOnWheel;

	override public void Init(EmoteInfo info)
	{
		base.Init(info);
		SetModeWait();
	}

	public void OnHovered()
	{
		if (!isOnWheel) return;
		hover.gameObject.SetActive(true);
		vfxIcon.gameObject.SetActive(hasVFX && true);
		elementIcon.gameObject.SetActive(EmoteInfo.EmoteElement != ElementEnum.NONE);
		rarityText.gameObject.SetActive(true);
		emoteLogo.sprite = EmoteInfo.HoveredSilhouette;

		isHovered = true;
	}

	public void OnUnhovered()
	{
		if (!isOnWheel) return;
		hover.gameObject.SetActive(false);
		elementIcon.gameObject.SetActive(false);
		rarityText.gameObject.SetActive(false);
		if (hasVFX) vfxIcon.gameObject.SetActive(false);
		emoteLogo.sprite = EmoteInfo.UnhoveredSilhouette;

		isHovered = false;
	}

	private void Update()
	{
		DoAction();

		if (Input.GetMouseButtonDown(0) && isHovered)
			OnEmoteSelected?.Invoke(this, EmoteInfo);
	}

	private void SetModeWait()
	{
		DoAction = DoActionWait;
	}

	public void SetModeMove()
	{
		startPos = transform.localPosition;
		startRot = transform.localRotation;

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
		transform.localRotation = Quaternion.Lerp(startRot, Quaternion.identity, elapsedTime / moveDuration);

		if(elapsedTime >= moveDuration)
		{
			elapsedTime = 0;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			SetModeWait();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (isOnWheel && collision.CompareTag("Indicator"))
			OnHovered();
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (isOnWheel && collision.CompareTag("Indicator"))
			OnUnhovered();
	}
}
