using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void BagManagerEventHandler(EmoteInfo info);
public class BagManager : MonoBehaviour
{
    [SerializeField] private Transform inventory;
    [SerializeField] private Transform emoteWheelInBag;
    [SerializeField] private BagEmplacement emoteBagEmplacement;
    [SerializeField] private BagEmplacement bagEmplacement;
    [SerializeField] private BagCard bagCard;
    [SerializeField] private BagCard wheelBagCard;

	private List<BagEmplacement> inventoryEmplacements = new List<BagEmplacement>();
	private Dictionary<EmoteInfo, BagEmplacement> inventoryEmotePairs= new Dictionary<EmoteInfo, BagEmplacement>();
	private List<BagEmplacement> wheelEmplacements = new List<BagEmplacement>();

	private List<EmoteInfo> selectedEmotes = new List<EmoteInfo>();
	private BagEmplacement selectedInventoryEmplacement;
	private EmoteInfo replacedEmote;
	private WheelBagEmplacement selectedWheelEmplacement;

	public List<EmoteInfo> SelectedEmotes => selectedEmotes;

	public event BagManagerEventHandler OnSetDrag;
	public event Action OnStartDrag;

	public void Init(int nEmotes, int nWheelEmplacements, List<EmoteInfo> emotes, int indexIn, int indexOut)
	{
		SetEmplacements(inventory, bagEmplacement, inventoryEmplacements, nEmotes);
		SetEmplacements(emoteWheelInBag, emoteBagEmplacement, wheelEmplacements, nWheelEmplacements);
		InitWheelEmplacements(nWheelEmplacements, indexIn, indexOut);

		SetupScrollTransform(nEmotes, inventory);
		SetupScrollTransform(nWheelEmplacements, emoteWheelInBag);

		CreateBagCards(emotes);
	}

	private void SetupScrollTransform(int nElements, Transform containerToSetup)
	{
		GridLayoutGroup gridLayoutGroup = containerToSetup.GetComponent<GridLayoutGroup>();

		int newHeight = gridLayoutGroup.padding.top + ((int)(gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y)) * Mathf.CeilToInt((float)nElements / gridLayoutGroup.constraintCount);

		containerToSetup.GetComponent<RectTransform>().sizeDelta = new Vector2(0, newHeight);
	}

	public void StopDrag(EmoteInfo info)
	{
		if (selectedInventoryEmplacement as WheelBagEmplacement)
		{
			((WheelBagEmplacement)selectedInventoryEmplacement).EndDrag(selectedWheelEmplacement);
		}
		else
		{
			selectedInventoryEmplacement?.EndDrag();
		}
		
		if(selectedWheelEmplacement)
		{
			selectedWheelEmplacement?.EndDrag(selectedWheelEmplacement.IsFilled);
			CreateBagCard(selectedWheelEmplacement, info, wheelBagCard);
			selectedWheelEmplacement.IsFilled = true;
			selectedWheelEmplacement.SetZOrder();
			selectedWheelEmplacement.SetData();
		}

		selectedWheelEmplacement?.EndDrag();
		selectedWheelEmplacement = default;
		selectedInventoryEmplacement = default;

		OnSelectNewEmote(info, replacedEmote);
		replacedEmote = default;
	}

	private void OnSelectNewEmote(EmoteInfo newEmote, EmoteInfo oldEmote)
	{
		BagEmplacement newEmoteEmplacement;
		inventoryEmotePairs.TryGetValue(newEmote, out newEmoteEmplacement);
		newEmoteEmplacement.OnSelected(true);

		if(oldEmote != default)
		{
			inventoryEmotePairs.TryGetValue(oldEmote, out newEmoteEmplacement);
			newEmoteEmplacement.OnSelected(false);
			SelectedEmotes[SelectedEmotes.IndexOf(oldEmote)] = newEmote;
		}
		else
		{
			SelectedEmotes.Add(newEmote);
		}
	}

	private void CreateBagCards(List<EmoteInfo> emotes)
	{
		int count = emotes.Count;
		EmoteInfo currentEmote;
		for (int i = 0; i < count; i++)
		{
			currentEmote = emotes[i];

			CreateBagCard(inventoryEmplacements[i], currentEmote, bagCard);
			inventoryEmplacements[i].OnSelected(currentEmote.IsOnWheel);
			inventoryEmotePairs.Add(currentEmote, inventoryEmplacements[i]);

			if (currentEmote.IsOnWheel)
			{
				CreateBagCard(wheelEmplacements[currentEmote.IndexOnWheel - 1], currentEmote, wheelBagCard);
				((WheelBagEmplacement)wheelEmplacements[currentEmote.IndexOnWheel - 1]).IsFilled = true;
				((WheelBagEmplacement)wheelEmplacements[currentEmote.IndexOnWheel - 1]).SetZOrder();
				OnSelectNewEmote(currentEmote, replacedEmote);
			}
		}
	}

	private void CreateBagCard(BagEmplacement parent, EmoteInfo info, BagCard cardPrefab)
	{
		BagCard currentBagCard = Instantiate(cardPrefab, parent.transform);
		currentBagCard.Init(info);
		parent.Init(currentBagCard);
	}

	private void SetEmplacements(Transform container, BagEmplacement prefabToUse, List<BagEmplacement> listToFill, int nEmplacements)
	{
		BagEmplacement newEmplacements;
		for (int i = 0; i < nEmplacements; i++)
		{
			newEmplacements = Instantiate(prefabToUse, container);
			listToFill.Add(newEmplacements);
			newEmplacements.OnSetDrag += InventoryEmplacement_OnSetDrag;
			newEmplacements.OnStartDrag += InventoryEmplacement_OnStartDrag;
		}
	}

	private void InventoryEmplacement_OnStartDrag()
	{
		OnStartDrag?.Invoke();
	}

	private void InventoryEmplacement_OnSetDrag(BagEmplacement sender, EmoteInfo info)
	{
		selectedInventoryEmplacement = sender;
		OnSetDrag?.Invoke(info);
	}

	private void InitWheelEmplacements(int nEmplacements, int indexIn, int indexOut)
	{
		WheelBagEmplacement currentEmplacement;
		for (int i = 0; i < nEmplacements; i++)
		{
			currentEmplacement = ((WheelBagEmplacement)wheelEmplacements[i]);
			currentEmplacement.Init(i + 1, nEmplacements, indexOut, indexIn);
			currentEmplacement.OnSetDrag += WheelEmplacement_OnSetDrag;
			currentEmplacement.SetDestinationDrag += WheelEmplacement_SetDestinationDrag;
		}
	}

	private void WheelEmplacement_SetDestinationDrag(WheelBagEmplacement sender, EmoteInfo info)
	{
		if (selectedInventoryEmplacement == default) return;

		replacedEmote = info;
		selectedWheelEmplacement = sender;
	}

	private void WheelEmplacement_OnSetDrag(WheelBagEmplacement sender, EmoteInfo info)
	{
		if (selectedInventoryEmplacement == default) selectedInventoryEmplacement = sender;
		else selectedWheelEmplacement = sender;
		OnSetDrag?.Invoke(info);
	}
}

