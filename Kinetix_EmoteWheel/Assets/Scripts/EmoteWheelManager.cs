using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteWheelManager : MonoBehaviour
{
    [SerializeField] private KeyCode input;
    [SerializeField] private EmoteWheel wheel;

    private bool isWheelActive;

	private void Awake()
	{
		wheel.gameObject.SetActive(isWheelActive);

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
