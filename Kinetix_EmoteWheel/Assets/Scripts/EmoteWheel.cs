using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EmoteWheelEventHandler(EmoteInfo info);
public class EmoteWheel : MonoBehaviour
{
    [SerializeField] private WheelMember emotePrefab;
    [SerializeField] private Transform emoteContainer;

	public event EmoteWheelEventHandler OnEmoteSelected;

    private float angle;

	public void SetEmoteOnWheel(int emoteCount, float radius, List<EmoteInfo> emotes)
	{
		Vector3 pos;
		WheelMember currentEmote;
		angle = Mathf.Deg2Rad * 360 / emoteCount;
		int count = 0;
		for (int i = emoteCount; i > 0; i--)
		{
			count++;
			pos = new Vector3(Mathf.Cos(angle * i) * radius, Mathf.Sin(angle * i) * radius);
			currentEmote = Instantiate(emotePrefab, emoteContainer);
			currentEmote.transform.localPosition = pos;
			currentEmote.Init(emotes[count-1]);
			currentEmote.OnEmoteSelected += CurrentEmote_OnEmoteSelected;
		}
	}

	private void CurrentEmote_OnEmoteSelected(WheelMember sender, EmoteInfo info)
	{
		OnEmoteSelected?.Invoke(info);
	}
}
