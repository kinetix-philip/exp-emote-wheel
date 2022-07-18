using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void WheelBagEmplacementEventHandler(WheelBagEmplacement sender, EmoteInfo info);
public class WheelBagEmplacement : BagEmplacement, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text indicator;

    private const string INDICATOR_FORMAT = "{0} / {1}";
	private int rank;
	private bool isFilled = false;
	private bool isOnWheel;

	public bool IsFilled { get => isFilled; set => isFilled = value; }

	public new event WheelBagEmplacementEventHandler OnSetDrag;
	public event WheelBagEmplacementEventHandler SetDestinationDrag;

	public void Init(int rank, int maxRank, int indexOut, int indexIn)
	{
		indicator.text = string.Format(INDICATOR_FORMAT, rank, maxRank);
		this.rank = rank;
		isOnWheel = rank >= indexIn && rank <= indexOut;
	}

	public void EndDrag(bool destroyBagCard)
	{
		if(IsFilled)
		{
			base.EndDrag();
			if (destroyBagCard)
			{
				bagCard?.ChangeIndex(0, false, true);
				Destroy(bagCard.gameObject);
			}
			else bagCard?.gameObject.SetActive(true);
		}

		isFilled = !destroyBagCard;

		
	}

	public void SetData()
	{
		bagCard?.ChangeIndex(rank, isOnWheel, true);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		SetDestinationDrag?.Invoke(this, isFilled ? bagCard.EmoteInfo : default);
	}


	public override void OnPointerDown(PointerEventData eventData)
	{
		isDragSetup = true;
		OnSetDrag?.Invoke(this, bagCard.EmoteInfo);
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);
		if(isDragSetup) bagCard?.gameObject.SetActive(false);
	}

	public override void OnPointerClick(PointerEventData eventData)
	{

	}
}
