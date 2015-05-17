using UnityEngine;
using System.Collections;

public class DamageNumberManager
{
	private static DamageNumberManager instance;
	public static DamageNumberManager Instance
	{
		get
		{
			return instance ?? (instance = new DamageNumberManager());
		}
	}

	private GameObject damageNumberPrefab; 
	private GameObject l5Object; 

	public void Init()
	{
		this.damageNumberPrefab = Resources.Load<GameObject>("Prefabs/DamageNumber");
		this.l5Object = GameObject.Find("L5");
	}

	public void Show(int damageValue, UnitAnimation unitAnimation)
	{
		var damageObject = NGUITools.AddChild(this.l5Object, this.damageNumberPrefab);
		var damageNumber = new DamageNumber(damageObject, damageValue);

		if (unitAnimation.player.playerId == 1)
		{
			damageObject.transform.localPosition = new Vector3(0f, 0f);
		}
		else
		{
			damageObject.transform.localPosition = new Vector3(200f, 0f);
		}

		var timerCallback = damageObject.AddMissingComponent<TimerCallback>();
		timerCallback.time = 2f;
		timerCallback.SetCustomCallback(() =>
		{
			GameObject.Destroy(damageObject);
        });
	}
}
