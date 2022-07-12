using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelBagEmplacement : MonoBehaviour
{
    [SerializeField] private Text indicator;

    private string indicatorFormat;
	private int rank;

	private void Awake()
	{
		indicatorFormat = indicator.text;
	}

	public void Init(int rank, int maxRank)
	{
		indicator.text = string.Format(indicatorFormat, rank, maxRank);
		this.rank = rank;
	}
}
