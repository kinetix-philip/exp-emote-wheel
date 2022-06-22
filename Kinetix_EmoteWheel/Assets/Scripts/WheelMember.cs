using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WheelMember : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text name;
    [SerializeField] private Image hover;
    [SerializeField] private Button button;
    private string nameFormat;

	private void Awake()
	{
		nameFormat = name.text;
	}
	public void SetName(int index)
	{
		name.text = string.Format(nameFormat, index);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		hover.gameObject.SetActive(true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		hover.gameObject.SetActive(false);
	}
}
