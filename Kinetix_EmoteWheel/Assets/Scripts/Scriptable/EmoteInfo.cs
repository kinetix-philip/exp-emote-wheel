using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "EmoteWheel", fileName = "EmoteInfo", order = 0)]
public class EmoteInfo : ScriptableObject
{
    [SerializeField] private string emoteName;
    [SerializeField] private int indexOnWheel;

	public string EmoteName => emoteName;
	public int IndexOnWheel { get => indexOnWheel; set => indexOnWheel = value; }
	//[SerializeField] private Image EmoteLogo;
}
