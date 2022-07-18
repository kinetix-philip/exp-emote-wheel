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
	[SerializeField] private DragContainer drag;
	[SerializeField] private int nEmotesSelectables;
	private bool isBagOpen;

	[SerializeField] private List<EmoteInfo> emotes;

	private List<EmoteInfo> selectedEmotes = new List<EmoteInfo>();

	private Action DoAction;

	private void Awake()
	{
		SetModeWait();
		wheel.Init(wheelIndexIn, wheelIndexOut);
		bag.Init(emotes.Count, nEmotesSelectables, emotes, wheelIndexIn, wheelIndexOut);

		wheel.gameObject.SetActive(isWheelActive);
		wheel.SetEmplacementsOnWheel(emoteOnWheelCount, radius);

		animatorOverride = new AnimatorOverrideController(playerController.runtimeAnimatorController);
		playerController.runtimeAnimatorController = animatorOverride;
		if (bag.SelectedEmotes.Count > 0)
		{
			selectedEmotes = bag.SelectedEmotes;
			wheel.SetEmoteOnWheel(selectedEmotes);
		}

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
		if(selectedEmotes.Count > emoteOnWheelCount) currentScrollValue = Input.GetAxis("Mouse ScrollWheel");

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
		drag.gameObject.SetActive(true);
	}

	private void DoActionBag()
	{
		drag.transform.localPosition = bag.transform.InverseTransformPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

		if (Input.GetMouseButtonUp(0))
		{
			drag.gameObject.SetActive(false);
			bag.StopDrag(drag.Visual.EmoteInfo);
		}
	}

	private void CheckMainInput()
	{
		if(!isWheelActive) CheckBagInput();
		if(!isBagOpen) CheckWheelInput();
	}

	private void CheckWheelInput()
	{
		if (Input.GetKeyDown(wheelInput))
		{
			SetModeWheel();
			isWheelActive = true;
			currentScrollValue = 0;
			wheel.OnEmoteSelected += Wheel_OnEmoteSelected;
		}
		else if (Input.GetKeyUp(wheelInput))
		{
			isWheelActive = false;
			SetModeWait();
			wheel.OnEmoteSelected -= Wheel_OnEmoteSelected;
		}

		wheel.gameObject.SetActive(isWheelActive);
	}

	private void CheckBagInput()
	{
		if(Input.GetKeyDown(bagInput) && !isBagOpen)
		{
			isBagOpen = true;
			bag.gameObject.SetActive(true);
			bag.OnSetDrag += Bag_OnSetDrag;
			bag.OnStartDrag += Bag_OnStartDrag;
		}
		else if(Input.GetKeyDown(bagInput) && isBagOpen)
		{
			isBagOpen = false;
			bag.gameObject.SetActive(false);
			SetModeWait();
			bag.OnSetDrag -= Bag_OnSetDrag;
			bag.OnStartDrag -= Bag_OnStartDrag;
			selectedEmotes = bag.SelectedEmotes;

			wheel.SetEmoteOnWheel(selectedEmotes);
		}
	}

	private void Bag_OnStartDrag()
	{
		SetModeBag();
	}

	private void Bag_OnSetDrag(EmoteInfo info)
	{
		drag.Init(info);
	}
}
