using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "EmoteWheel", fileName = "EmoteInfo", order = 0)]
public class EmoteInfo : ScriptableObject
{
    [SerializeField] private string emoteName;
    [SerializeField] private int indexOnWheel;
    [SerializeField] private bool isOnWheel;

	public string EmoteName => emoteName;
	public int IndexOnWheel { get => indexOnWheel; set => indexOnWheel = value; }
    public bool IsOnWheel { get => isOnWheel; set => isOnWheel = value; }
}
