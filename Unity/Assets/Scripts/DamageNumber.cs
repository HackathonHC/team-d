using UnityEngine;
using System.Collections;

public class DamageNumber
{
	public GameObject gameObject;
	private UISprite oneSprite;
	private UISprite tenSprite;
	private UISprite hundredSprite;
	private UISprite thouthandSprite;

	public DamageNumber(GameObject damageNumberObject, int damageValue)
	{
		this.gameObject = damageNumberObject;
		this.oneSprite = this.gameObject.transform.Find("0").GetComponent<UISprite>();
		this.tenSprite = this.gameObject.transform.Find("00").GetComponent<UISprite>();
		this.hundredSprite = this.gameObject.transform.Find("000").GetComponent<UISprite>();
		this.thouthandSprite = this.gameObject.transform.Find("0000").GetComponent<UISprite>();

		var one = damageValue % 10;
		var tens = (damageValue / 10) % 10;
		var hundreds = (damageValue / 100) % 10;
		var thousands = (damageValue / 1000) % 10;

		this.oneSprite.spriteName = one.ToString();
		this.tenSprite.spriteName = tens.ToString();
		this.hundredSprite.spriteName = hundreds.ToString();
		this.thouthandSprite.spriteName = thousands.ToString();

//		this.tenSprite.gameObject.SetActive(damageValue > 10);
//		this.hundredSprite.gameObject.SetActive(damageValue > 100);
//		this.thouthandSprite.gameObject.SetActive(damageValue > 100);
	}
}
