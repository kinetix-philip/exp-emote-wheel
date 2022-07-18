using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmoteVisual : MonoBehaviour
{
    [SerializeField] protected Text rarityText;
    [SerializeField] protected Image vfxIcon;
    [SerializeField] protected Image elementIcon;
    [SerializeField] protected Image emoteLogo;
    [SerializeField] protected List<ElementIconStruct> elementIcons;

	private EmoteInfo emoteInfo;
	protected int indexOnWheel;
    protected bool isOnWheel;
    protected bool hasVFX;

	public EmoteInfo EmoteInfo => emoteInfo;

	public void ChangeIndex(int newIndex, bool isOnWheel, bool changeBaseData)
    {
        indexOnWheel = newIndex;
        this.isOnWheel = isOnWheel;

        if(changeBaseData)
		{
            emoteInfo.IndexOnWheel = newIndex;
            emoteInfo.IsOnWheel = isOnWheel;
		}
    }

    virtual public void Init(EmoteInfo info)
    {
        emoteInfo = info;
        rarityText.text = info.EmoteRarity.ToString();
        isOnWheel = info.IsOnWheel;
        indexOnWheel = info.IndexOnWheel;
        hasVFX = info.HasVFX;
        emoteLogo.sprite = info.UnhoveredSilhouette;
        SelectElementIcon();
    }

    private void SelectElementIcon()
    {
        foreach (ElementIconStruct item in elementIcons)
        {
            if (item.element == EmoteInfo.EmoteElement)
            {
                elementIcon.sprite = item.elementIcon;
                break;
            }
        }
    }
}
