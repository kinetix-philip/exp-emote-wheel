using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    [SerializeField] private Transform inventory;
    [SerializeField] private Transform emoteWheelInBag;
    [SerializeField] private BagEmplacement emoteBagEmplacement;
    [SerializeField] private BagEmplacement bagEmplacement;
    [SerializeField] private BagCard bagCard;

	private List<BagEmplacement> inventoryEmplacements = new List<BagEmplacement>();
	private List<BagEmplacement> wheelEmplacements = new List<BagEmplacement>();

	public void Init(int nEmotes, int nWheelEmplacements, List<EmoteInfo> emotes)
	{
		SetEmplacements(inventory, bagEmplacement, inventoryEmplacements, nEmotes);
		SetEmplacements(emoteWheelInBag, emoteBagEmplacement, wheelEmplacements, nWheelEmplacements);
		InitWheelEmplacements(nWheelEmplacements);
		CreateBagCards(emotes);
	}

	private void CreateBagCards(List<EmoteInfo> emotes)
	{
		int count = emotes.Count;
		EmoteInfo currentEmote;
		BagEmplacement currentEmplacement;
		BagCard currentBagCard;
		for (int i = 0; i < count; i++)
		{
			currentEmote = emotes[i];
			if (currentEmote.IsOnWheel)
				currentEmplacement = wheelEmplacements[i];
			else
				currentEmplacement = inventoryEmplacements[i];

			currentBagCard = Instantiate(bagCard, currentEmplacement.transform);
			currentBagCard.Init(currentEmote);
		}
	}

	private void SetEmplacements(Transform container, BagEmplacement prefabToUse, List<BagEmplacement> listToFill, int nEmplacements)
	{
		BagEmplacement newEmplacements;
		for (int i = 0; i < nEmplacements; i++)
		{
			newEmplacements = Instantiate(prefabToUse, container);
			listToFill.Add(newEmplacements);
		}
	}

	private void InitWheelEmplacements(int nEmplacements)
	{
		for (int i = 0; i < nEmplacements; i++)
		{
			wheelEmplacements[i].GetComponent<WheelBagEmplacement>().Init(i + 1, nEmplacements);
		}
	}

	
}
