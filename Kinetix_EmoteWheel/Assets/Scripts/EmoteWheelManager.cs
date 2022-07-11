using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteWheelManager : MonoBehaviour
{
    [SerializeField] private KeyCode input;
    [SerializeField] private EmoteWheel wheel;
	[SerializeField] private float radius;
	[SerializeField] private int emoteOnWheelCount;
	[SerializeField] private int wheelIndexIn;
	[SerializeField] private int wheelIndexOut;
	[SerializeField] private List<EmoteInfo> emotes;
	[SerializeField] private Animator playerController;

    private bool isWheelActive;
	private float currentScrollValue;
	private AnimatorOverrideController animatorOverride;

	//private List<KeyValuePair<AnimationClip, AnimationClip>>

	private void Awake()
	{
		wheel.Init(wheelIndexIn, wheelIndexOut);
		wheel.gameObject.SetActive(isWheelActive);
		wheel.SetEmplacementsOnWheel(emoteOnWheelCount, radius);
		wheel.SetEmoteOnWheel(emotes);
		wheel.OnEmoteSelected += Wheel_OnEmoteSelected;

		animatorOverride = new AnimatorOverrideController(playerController.runtimeAnimatorController);
		playerController.runtimeAnimatorController = animatorOverride;
	}

	private void Wheel_OnEmoteSelected(EmoteInfo info)
	{
		wheel.gameObject.SetActive(false);
		isWheelActive = false;
		
		animatorOverride[animatorOverride.animationClips[1].name] = info.Emote;

		playerController.SetTrigger("playEmote");
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
			currentScrollValue = 0;
		}
		else if (Input.GetKeyUp(input))
		{
			isWheelActive = false;
		}

		if(emotes.Count > emoteOnWheelCount) currentScrollValue = Input.GetAxis("Mouse ScrollWheel");

		if (isWheelActive && currentScrollValue != 0) wheel.MoveAllEmotes(currentScrollValue > 0);

		
	}
}
