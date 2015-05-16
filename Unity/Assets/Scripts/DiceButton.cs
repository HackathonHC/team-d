using UnityEngine;
using System.Collections;

public class DiceButton : MonoBehaviour
{
	private UISprite sprite;

	void Awake()
	{
		this.sprite = this.transform.Find("Sprite").GetComponent<UISprite>();
	}

	public void OnButtonClick()
	{
		Debug.Log("click!");
	}
}
