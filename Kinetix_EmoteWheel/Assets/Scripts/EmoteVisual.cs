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
    
    protected EmoteInfo emoteInfo;
    protected int indexOnWheel;
    protected bool isOnWheel;
    protected bool hasVFX;

    public void ChangeIndex(int newIndex, bool isOnWheel = true)
    {
        indexOnWheel = newIndex;
        this.isOnWheel = isOnWheel;
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
            if (item.element == emoteInfo.EmoteElement)
            {
                elementIcon.sprite = item.elementIcon;
                break;
            }
        }
    }
}
