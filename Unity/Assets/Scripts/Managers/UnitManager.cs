using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitManager
{
	private static UnitManager instance;
	public static UnitManager Instance
	{
		get
		{
			return instance ?? (instance = new UnitManager());
		}
	}

	private Dictionary<int, Unit> unitMap;

	public void Init()
	{
		this.unitMap = new Dictionary<int, Unit>();

		foreach (var player in PlayerManager.Instance.PlayerList)
		{
			var unit = new Unit(player.unitId);
			
			// Set Hpbar Reference
			var hpBarName = string.Format("{0}pHpFrame", player.playerId);
			unit.hpBar = GameObject.Find("L4").transform.Find(hpBarName).GetComponent<UIProgressBar>();
			unit.hpBarAnimation = unit.hpBar.gameObject.AddMissingComponent<AnimationComponent>();

			// Set Manager
			if (!this.unitMap.ContainsKey(player.playerId))
			{
				this.unitMap.Add(player.playerId, unit);
			}

			// Set Reference
			player.unit = unit;
		}
	}

	public Unit FindUnit(int playerId)
	{
		return this.unitMap[playerId];
	}
}
