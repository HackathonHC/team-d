using UnityEngine;
using System.Collections;

public class UIAnimationManager
{
	private static UIAnimationManager instance;
	public static UIAnimationManager Instance
	{
		get
		{
			return instance ?? (instance = new UIAnimationManager());
		}
	}

	public enum Type 
	{
		BattleStart = 1,
		TurnStart = 2,
	}

	private string BATTLE_START_PATH = "Prefabs/BattleStart";
	private GameObject battleStartPrefab;
	private GameObject l5Object;

	public void Init()
	{
		this.battleStartPrefab = Resources.Load<GameObject>(BATTLE_START_PATH);
		this.l5Object = GameObject.Find("L5");
	}

	public Promises.Deferred PlayAnimation(Type type)
	{
		var deferred = new Promises.Deferred();
		if (type == Type.BattleStart)
		{
			// Create Object
			var battleStart = CreateGameObject(this.battleStartPrefab);
			// Tween
			var animation = battleStart.GetComponent<AnimationComponent>();
			animation.SlideIn(1f, new Vector3(800f, 0f), new Vector3(0f, 0f)).Done(() =>
			{
				animation.SlideOut(1f, new Vector3(0f, 0f), new Vector3(-800f, 0f)).Done(() =>
				{
					deferred.Resolve();
				});
			});
		}
		return deferred;
	}

	public Promises.Deferred PlayTurnAnimation(Player currentPlayer)
	{
		var deferred = new Promises.Deferred();
		// Create Object
		var turnStart = CreateGameObject(this.battleStartPrefab);
		var turnSprite = turnStart.transform.Find("Sprite").GetComponent<UISprite>();
		turnSprite.spriteName = string.Format("{0}p", currentPlayer.playerId);
		// Tween
		var animation = turnStart.GetComponent<AnimationComponent>();
		animation.SlideIn(1f, new Vector3(800f, 0f), new Vector3(0f, 0f)).Done(() =>
		                                                                       {
			animation.SlideOut(1f, new Vector3(0f, 0f), new Vector3(-800f, 0f)).Done(() =>
			                                                                         {
				deferred.Resolve();
			});
		});
		return deferred;
	}

	public GameObject CreateGameObject(GameObject prefab)
	{
		return NGUITools.AddChild(this.l5Object, prefab);
	}

	private Vector3 GetFromPosition(Type type)
	{
		if (type == Type.BattleStart)
		{
			return new Vector3(400f, 0f);
		}
		return Vector3.zero;
	}

	private Vector3 GetToPosition(Type type)
	{
		if (type == Type.BattleStart)
		{
			return new Vector3(0f, 0f);
		}
		return Vector3.zero;
	}

}
