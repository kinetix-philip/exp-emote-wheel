using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteWheelManager : MonoBehaviour
{

	[SerializeField] private Animator playerController;

	[Header("Inputs")]
	[SerializeField] private KeyCode wheelInput;
	[SerializeField] private KeyCode bagInput;

	[Header("Wheel Parameters")]
	[SerializeField] private EmoteWheel wheel;
	[SerializeField] private float radius;
	[SerializeField] private int emoteOnWheelCount;
	[SerializeField] private int wheelIndexIn;
	[SerializeField] private int wheelIndexOut;
	[SerializeField] private Transform indicator;
	private bool isWheelActive;
	private float currentScrollValue;
	private AnimatorOverrideController animatorOverride;

	[Header("Bag Parameters")]
	[SerializeField] private BagManager bag;
	[SerializeField] private int nEmotesSelectables;
	private bool isBagOpen;

	[SerializeField] private List<EmoteInfo> emotes;

	private Action DoAction;

	private void Awake()
	{
		SetModeWait();
		wheel.Init(wheelIndexIn, wheelIndexOut);
		bag.Init(emotes.Count, nEmotesSelectables);

		wheel.OnEmoteSelected += Wheel_OnEmoteSelected;
		wheel.gameObject.SetActive(isWheelActive);
		wheel.SetEmplacementsOnWheel(emoteOnWheelCount, radius);


		//wheel.SetEmoteOnWheel(emotes);

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
		CheckMainInput();
		DoAction();
	}

	private void SetModeWait()
	{
		DoAction = DoActionWait;
	}

	private void DoActionWait()
	{
		
	}

	private void SetModeWheel()
	{
		DoAction = DoActionWheel;
	}

	private void DoActionWheel()
	{
		if(emotes.Count > emoteOnWheelCount) currentScrollValue = Input.GetAxis("Mouse ScrollWheel");

		if (isWheelActive && currentScrollValue != 0) wheel.MoveAllEmotes(currentScrollValue > 0);

		if(isWheelActive)
		{
			Vector2 centerToMouse = Input.mousePosition - indicator.position;
			indicator.rotation = Quaternion.AngleAxis(Vector3.SignedAngle(transform.up, centerToMouse, transform.forward), transform.forward);
		}
	}

	private void SetModeBag()
	{
		DoAction = DoActionBag;
	}

	private void DoActionBag()
	{
		Debug.Log("coucou");
	}

	private void CheckMainInput()
	{
		CheckBagInput();
		if(!isBagOpen) CheckWheelInput();
	}

	private void CheckWheelInput()
	{
		if (Input.GetKeyDown(wheelInput))
		{
			SetModeWheel();
			isWheelActive = true;
			currentScrollValue = 0;
		}
		else if (Input.GetKeyUp(wheelInput))
		{
			isWheelActive = false;
			SetModeWait();
		}

		wheel.gameObject.SetActive(isWheelActive);
	}

	private void CheckBagInput()
	{
		if(Input.GetKeyDown(bagInput) && !isBagOpen)
		{
			isBagOpen = true;
			bag.gameObject.SetActive(true);
			SetModeBag();
		}
		else if(Input.GetKeyDown(bagInput) && isBagOpen)
		{
			isBagOpen = false;
			bag.gameObject.SetActive(false);
			SetModeWait();
		}
	}
}
