using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragContainer : MonoBehaviour
{
    [SerializeField] private BagCard visual;

	public BagCard Visual => visual;

	public void Init(EmoteInfo info)
	{
		visual.Init(info);
	}
}
