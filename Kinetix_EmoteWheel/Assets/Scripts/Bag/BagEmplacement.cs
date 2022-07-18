using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void BagEmplacementEventHandler(BagEmplacement sender, EmoteInfo info);
public class BagEmplacement : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerExitHandler
{
	protected BagCard bagCard;
	protected bool isDragSetup;

	private bool isSelected;

	public event BagEmplacementEventHandler OnSetDrag;
	public event Action OnStartDrag;

	virtual public void Init(BagCard card)
	{
		bagCard = card;
	}

	virtual public void EndDrag()
	{
		isDragSetup = false;
	}

	virtual public void OnPointerClick(PointerEventData eventData)
	{
		bagCard.SetPreview(!bagCard.IsPreviewOpen);
	}

	virtual public void OnPointerDown(PointerEventData eventData)
	{
		if (isSelected) return;
		
		OnSetDrag?.Invoke(this, bagCard.EmoteInfo);
		isDragSetup = true;
	}
	
	virtual public void OnPointerExit(PointerEventData eventData)
	{
		if (!isDragSetup) return;

		OnStartDrag?.Invoke();
	}

	public void OnSelected(bool isSelected)
	{
		this.isSelected = isSelected;
		bagCard.SelectedIcon.gameObject.SetActive(isSelected);
	}
}
