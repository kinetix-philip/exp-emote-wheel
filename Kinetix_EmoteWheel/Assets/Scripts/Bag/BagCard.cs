using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagCard : EmoteVisual
{
	[SerializeField] private Text animDuration;

	private const string ANIM_DURATION_FORMAT = "00 : {0}";

	public override void Init(EmoteInfo info)
	{
		base.Init(info);
		animDuration.text = string.Format(ANIM_DURATION_FORMAT, Mathf.Round(info.Emote.length));
	}
}
