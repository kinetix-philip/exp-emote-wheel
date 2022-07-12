using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    [SerializeField] private Transform inventory;
    [SerializeField] private Transform emoteWheelInBag;
    [SerializeField] private BagEmplacement emoteBagEmplacement;
    [SerializeField] private BagEmplacement bagEmplacement;

	private List<BagEmplacement> inventoryEmplacements = new List<BagEmplacement>();
	private List<BagEmplacement> wheelEmplacements = new List<BagEmplacement>();

	public void Init(int nEmotes, int nWheelEmplacements)
	{
		SetEmplacements(inventory, bagEmplacement, inventoryEmplacements, nEmotes);
		SetEmplacements(emoteWheelInBag, emoteBagEmplacement, wheelEmplacements, nWheelEmplacements);
		InitWheelEmplacements(nWheelEmplacements);
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

	// Update is called once per frame
	void Update()
    {
        
    }
}
