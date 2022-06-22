using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "EmoteWheel", fileName = "EmoteInfo", order = 0)]
public class EmoteInfo : ScriptableObject
{
    [SerializeField] private string emoteName;

	public string EmoteName => emoteName;
	//[SerializeField] private Image EmoteLogo;
}
