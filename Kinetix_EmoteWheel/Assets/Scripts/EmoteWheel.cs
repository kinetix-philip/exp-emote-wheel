using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteWheel : MonoBehaviour
{
    [SerializeField] private int emoteCount;
    [SerializeField] private WheelMember emotePrefab;
    [SerializeField] private Transform emoteContainer;

    private float angle;

	private void Awake()
	{
        Vector3 pos;
        float radius = 200;
        WheelMember currentEmote;
        angle = Mathf.Deg2Rad * 360 / emoteCount;
        for (int i = emoteCount; i > 0; i--)
        {
			pos = new Vector3(Mathf.Cos(angle * i) * radius, Mathf.Sin(angle * i) * radius);
            currentEmote = Instantiate(emotePrefab, emoteContainer);
            currentEmote.transform.localPosition = pos;
            currentEmote.SetName(i);
        }
    }

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
