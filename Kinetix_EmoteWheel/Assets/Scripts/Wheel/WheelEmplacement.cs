using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelEmplacement : MonoBehaviour
{
    private int emoteIndex;

	public int EmoteIndex { get => emoteIndex; set => emoteIndex = value; }

	public void DestroyMember()
	{
		if (transform.childCount > 0) Destroy(transform.GetChild(0).gameObject);
	}
}
