using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationManager
{
	private static AnimationManager instance;
	public static AnimationManager Instance
	{
		get
		{
			return instance ?? (instance = new AnimationManager());
		}
	}

	private string ANIMATION_PREFAB_PATH = "Animations/{0}/Prefab/{1}";

	private Transform viewTransform;
	private Dictionary<int, GameObject> animationPrefabMap;

	public void Init()
	{
		this.viewTransform = GameObject.Find("AnimCamera").transform.Find("View");
		this.animationPrefabMap = new Dictionary<int, GameObject>();

		foreach (var player in PlayerManager.Instance.PlayerList)
		{
			var unitId = player.unitId;
			// Cache Prefab
			if (!this.animationPrefabMap.ContainsKey(unitId))
			{
				var animationPath = GetPrefabPath(unitId);
				var animationPrefab = Resources.Load<GameObject>(animationPath);
				this.animationPrefabMap.Add(unitId, animationPrefab);
			}
			// Create GameObject
			var animationContainerObject = new GameObject();
			var prefab = this.animationPrefabMap[unitId];
			var animationObject = NGUITools.AddChild(animationContainerObject, prefab);

			// Set Scene
			animationContainerObject.transform.SetParent(this.viewTransform);

			// Setting Animation
			var unitAnimation = new UnitAnimation();
			unitAnimation.Init(player, animationContainerObject, animationObject);

			// Setting Reference
			player.unitAnimation = unitAnimation;
		}
	}

	private string GetPrefabPath(int id)
	{
		return string.Format(ANIMATION_PREFAB_PATH, id, id);
	}
}
