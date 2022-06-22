using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteWheel : MonoBehaviour
{
    [SerializeField] private WheelMember emotePrefab;
    [SerializeField] private Transform emoteContainer;

    private float angle;

	public void SetEmoteOnWheel(int emoteCount, float radius)
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
			currentEmote.SetName(count);
		}
	}
}
