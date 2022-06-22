using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteWheelManager : MonoBehaviour
{
    [SerializeField] private KeyCode input;
    [SerializeField] private EmoteWheel wheel;
	[SerializeField] private float radius;
	[SerializeField] private List<EmoteInfo> emotes;

    private bool isWheelActive;
	private int emoteCount;

	private void Awake()
	{
		emoteCount = emotes.Count;
		wheel.gameObject.SetActive(isWheelActive);
		wheel.SetEmoteOnWheel(emoteCount, radius, emotes);
		wheel.OnEmoteSelected += Wheel_OnEmoteSelected;
	}

	private void Wheel_OnEmoteSelected(EmoteInfo info)
	{
		Debug.Log("Play emote: " + info.EmoteName);
		wheel.gameObject.SetActive(false);
		isWheelActive = false;
	}

	// Update is called once per frame
	void Update()
	{
		CheckInput();
		wheel.gameObject.SetActive(isWheelActive);
	}

	private void CheckInput()
	{
		if (Input.GetKeyDown(input))
		{
			isWheelActive = true;
		}
		else if (Input.GetKeyUp(input))
		{
			isWheelActive = false;
		}
	}
}
