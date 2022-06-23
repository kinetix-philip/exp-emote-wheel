using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EmoteWheelEventHandler(EmoteInfo info);
public class EmoteWheel : MonoBehaviour
{
    [SerializeField] private WheelMember emotePrefab;
    [SerializeField] private WheelEmplacement emplacementPrefab;
    [SerializeField] private Transform emoteContainer;

	public event EmoteWheelEventHandler OnEmoteSelected;

    private float angle;
	private List<WheelEmplacement> emplacements = new List<WheelEmplacement>();

	public void SetEmoteOnWheel(List<EmoteInfo> emotes)
	{
		int emplacementsCount = emplacements.Count;
		WheelEmplacement currentEmplacement;

		foreach (EmoteInfo item in emotes)
		{
			for (int i = 0; i < emplacementsCount; i++)
			{
				currentEmplacement = emplacements[i];
				if (currentEmplacement.EmoteIndex == item.IndexOnWheel)
					GenerateWheelMember(item, currentEmplacement);
				else
					continue;
			}
		}
	}

	private void GenerateWheelMember(EmoteInfo item, WheelEmplacement currentEmplacement)
	{
		WheelMember currentMember = Instantiate(emotePrefab, currentEmplacement.transform);
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
			emplacements.Add(currentEmplacement);
		}
	}

	private void CurrentMember_OnEmoteSelected(WheelMember sender, EmoteInfo info)
	{
		OnEmoteSelected?.Invoke(info);
	}
}
