using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelBagEmplacement : MonoBehaviour
{
    [SerializeField] private Text indicator;

    private const string INDICATOR_FORMAT = "{0} / {1}";
	private int rank;


	public void Init(int rank, int maxRank)
	{
		indicator.text = string.Format(INDICATOR_FORMAT, rank, maxRank);
		this.rank = rank;
	}
}
