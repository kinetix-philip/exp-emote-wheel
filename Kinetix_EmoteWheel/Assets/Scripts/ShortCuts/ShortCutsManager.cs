using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShortCutsManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ShortCut bagShortCut; 
    [SerializeField] private ShortCut shopShortCut;
    [SerializeField] private ShortCut wheelShortCut;

    public event Action OnBag;
    public event Action OnShop;
    public event Action OnWheel;
    public event Action OnBar;
    public event Action OffBar;
    
    // Start is called before the first frame update
    void Awake()
    {
		bagShortCut.OnClick += ShortCut_OnClick;
        shopShortCut.OnClick += ShortCut_OnClick;
        wheelShortCut.OnClick += ShortCut_OnClick;
    }

    public void Init(InputStruct bag, InputStruct shop, InputStruct wheel)
	{
        bagShortCut.Init(bag);
        shopShortCut.Init(shop);
        wheelShortCut.Init(wheel);
	}

	private void ShortCut_OnClick(ShortCut sender)
	{
		if(sender == bagShortCut)
		{
            OnBag?.Invoke();
            return;
		}
        else if (sender == shopShortCut)
		{
            OnShop?.Invoke();
            return;
		}
        else
		{
            OnWheel?.Invoke();
            return;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
        OnBar?.Invoke();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
        OffBar?.Invoke();
	}
}
