using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingEmplacement : MonoBehaviour
{
    [SerializeField] private Transform firstElementToAppear;
    [SerializeField] private Transform lastElementToAppear;
    [SerializeField] private Transform waitingZone;

    private List<WheelMember> membersInWaiting = new List<WheelMember>();

	public List<WheelMember> MembersInWaiting { get => membersInWaiting; set => membersInWaiting = value; }

	public void SetEmplacements()
	{
        membersInWaiting[0].transform.parent = firstElementToAppear;
        membersInWaiting[0].SetModeMove();
        membersInWaiting[membersInWaiting.Count-1].transform.parent = lastElementToAppear;
        membersInWaiting[membersInWaiting.Count - 1].SetModeMove();

        if (membersInWaiting.Count <= 2) return;

		for (int i = 1; i <= membersInWaiting.Count - 2; i++)
		{
            membersInWaiting[i].transform.parent = waitingZone;
		}
	}
}
