using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagCard : EmoteVisual
{
	[SerializeField] private Text animDuration;
	[SerializeField] private PreviewCard preview;
	[SerializeField] private Image selectedIcon;
	[SerializeField] private Sprite cardBgPreviewOn;

	private const string ANIM_DURATION_FORMAT = "00 : {0}";

	private bool isPreviewOpen = false;

	private Sprite normalBg;
	private Image cardBg;

	public bool IsPreviewOpen => isPreviewOpen;

	public Image SelectedIcon => selectedIcon;

	private void Awake()
	{
		cardBg = GetComponent<Image>();
		normalBg = cardBg.sprite;
	}

	public override void Init(EmoteInfo info)
	{
		base.Init(info);
		animDuration.text = string.Format(ANIM_DURATION_FORMAT, Mathf.Round(info.Emote.length));
		preview.Init(Mathf.Round(info.Emote.length));
		elementIcon.gameObject.SetActive(EmoteInfo.EmoteElement != ElementEnum.NONE);
	}

	public void SetPreview(bool isOpen)
	{
		preview.gameObject.SetActive(isOpen);
		isPreviewOpen = isOpen;
		cardBg.sprite = isOpen ? cardBgPreviewOn : normalBg;
	}
}
