using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EmoteWheelEventHandler(EmoteInfo info);
public class EmoteWheel : MonoBehaviour
{
    [SerializeField] private WheelMember emotePrefab;
    [SerializeField] private WheelEmplacement emplacementPrefab;
    [SerializeField] private Transform waitingTransform;
    [SerializeField] private Transform emoteContainer;

	public event EmoteWheelEventHandler OnEmoteSelected;

    private float angle;
	private int indexIn;
	private int indexOut;
	private int maxIndex;
	private Dictionary<int, WheelEmplacement> emplacements = new Dictionary<int, WheelEmplacement>();
	private List<WheelMember> waitingList = new List<WheelMember>();
	private List<WheelMember> onWheelList = new List<WheelMember>();


	public void Init(int indexIn, int indexOut)
	{
		this.indexIn = indexIn;
		this.indexOut = indexOut;
	}

	public void SetEmoteOnWheel(List<EmoteInfo> emotes)
	{
		int emplacementsCount = emplacements.Count;
		WheelEmplacement currentEmplacement;

		foreach (EmoteInfo item in emotes)
		{
			if (!item.IsOnWheel)
			{
				GenerateWheelMember(item, waitingTransform, waitingList);
				continue;
			}

			for (int i = 1; i <= emplacementsCount; i++)
			{
				currentEmplacement = emplacements[i];
				if(item.IndexOnWheel == currentEmplacement.EmoteIndex) GenerateWheelMember(item, currentEmplacement.transform, onWheelList);
			}
		}
		
	}

	private void GenerateWheelMember(EmoteInfo item, Transform currentEmplacement, List<WheelMember> listToAdd)
	{
		WheelMember currentMember = Instantiate(emotePrefab, currentEmplacement);
		listToAdd.Add(currentMember);
		currentMember.Init(item);
		currentMember.OnEmoteSelected += CurrentMember_OnEmoteSelected;
	}

	public void SetEmplacementsOnWheel(int emoteCount, float radius)
	{
		Vector3 pos;
		WheelEmplacement currentEmplacement;
		angle = Mathf.Deg2Rad * 360 / emoteCount;
		int count = 0;
		for (int i = emoteCount; i > 0; i--)
		{
			count++;
			pos = new Vector3(Mathf.Cos(angle * i) * radius, Mathf.Sin(angle * i) * radius);
			currentEmplacement = Instantiate(emplacementPrefab, emoteContainer);
			currentEmplacement.transform.localPosition = pos;
			currentEmplacement.EmoteIndex = count;
			emplacements.Add(count, currentEmplacement);
		}

		maxIndex = count;
	}

	private void CurrentMember_OnEmoteSelected(WheelMember sender, EmoteInfo info)
	{
		OnEmoteSelected?.Invoke(info);
	}

	public void MoveAllEmotes(bool isUp)
	{
		WheelMember wheelMemberToRemove = default;
		WheelMember wheelMemberToAdd = default;
		foreach (WheelMember item in onWheelList)
		{
			if (isUp)
			{
				if (item.IndexOnWheel != indexOut)
				{
					MoveEmote(item, isUp);
				}
				else
				{
					MoveEmote(isUp, out wheelMemberToRemove, out wheelMemberToAdd, item, 0);
				}
			}	
			else
			{
				if (item.IndexOnWheel != indexIn)
				{
					MoveEmote(item, isUp);
				}
				else
				{
					MoveEmote(isUp, out wheelMemberToRemove, out wheelMemberToAdd, item, waitingList.Count-1);
				}
				
			}
			
		}
		onWheelList.Remove(wheelMemberToRemove);
		onWheelList.Add(wheelMemberToAdd);
	}

	private void MoveEmote(bool isUp, out WheelMember wheelMemberToRemove, out WheelMember wheelMemberToAdd, WheelMember item, int indexFromWaitingList)
	{

		WheelEmplacement newEmplacements;
		bool foundEmplacement;
		
		wheelMemberToRemove = item;
		wheelMemberToRemove.ChangeIndex(0, false);
		wheelMemberToRemove.transform.parent = waitingTransform;
		wheelMemberToRemove.transform.localPosition = Vector3.zero;
		wheelMemberToRemove.SetModeMove();
		
		wheelMemberToAdd = waitingList[indexFromWaitingList];
		wheelMemberToAdd.ChangeIndex(isUp ? indexIn : indexOut);

		foundEmplacement = emplacements.TryGetValue(isUp ? indexIn : indexOut, out newEmplacements);
		if (foundEmplacement)
		{
			wheelMemberToAdd.transform.parent = newEmplacements.transform;
			wheelMemberToAdd.transform.localPosition = Vector3.zero;
			wheelMemberToAdd.SetModeMove();
		}

		waitingList.RemoveAt(indexFromWaitingList);
		waitingList.Add(wheelMemberToRemove);
	}

	private void MoveEmote(WheelMember item, bool isUp)
	{

		WheelEmplacement newEmplacements;
		bool foundEmplacement;
		int factor = (isUp ? -1 : 1);
		int valueToTest = isUp ? 0 : maxIndex + 1;
		int newIndex = item.IndexOnWheel + factor;
		newIndex = newIndex == valueToTest ? (isUp ? maxIndex : 1) : newIndex;

		item.ChangeIndex(newIndex);

		foundEmplacement = emplacements.TryGetValue(item.IndexOnWheel, out newEmplacements);
		if (foundEmplacement)
		{
			item.transform.parent = newEmplacements.transform;
			item.transform.localPosition = Vector3.zero;
			item.SetModeMove();
		}
	}
}
