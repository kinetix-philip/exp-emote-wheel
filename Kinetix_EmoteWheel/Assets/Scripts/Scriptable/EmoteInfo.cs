using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "EmoteWheel", fileName = "EmoteInfo", order = 0)]
public class EmoteInfo : ScriptableObject
{
    [SerializeField] private RarityEnum emoteRarity;
    [SerializeField] private ElementEnum emoteElement;
    [SerializeField] private int indexOnWheel;
    [SerializeField] private bool isOnWheel;
    [SerializeField] private bool hasVFX;
    [SerializeField] private Sprite hoveredSilhouette;
    [SerializeField] private Sprite unhoveredSilhouette;

	public RarityEnum EmoteRarity => emoteRarity;
	public int IndexOnWheel { get => indexOnWheel; set => indexOnWheel = value; }
    public bool IsOnWheel { get => isOnWheel; set => isOnWheel = value; }
	public ElementEnum EmoteElement => emoteElement;
	public bool HasVFX => hasVFX;
	public Sprite HoveredSilhouette => hoveredSilhouette;
	public Sprite UnhoveredSilhouette => unhoveredSilhouette;
}
