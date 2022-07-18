using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewCard : MonoBehaviour
{
	[SerializeField] private Text timer;
	[SerializeField] private Image emoteLogo;
	[SerializeField] private Button playBtn;

	private const string TIMER_FORMAT = "00 : {0} / 00 : {1}";

	private float animDuration;

	public void Init(float animDuration)
	{
		this.animDuration = animDuration;
		timer.text = string.Format(TIMER_FORMAT, 0, animDuration);
	}
}
