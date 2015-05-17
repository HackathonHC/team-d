using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit
{
	public class Master
	{
		public int id;
		public int maxHp;
		public int attack;
		public string name;
		public Type type;
	}

	public enum Type
	{
		Attack  = 1,
		Balance = 2,
		Defence = 3,
	}

	public int hp;
	public Master master;
	public UnitAnimation unitAnimation;
	public Player player;
	public UIProgressBar hpBar;
	public AnimationComponent hpBarAnimation;

	public Unit(int id)
	{
		this.player = null;
		this.unitAnimation = null;

		// Load
		var parameters = UnitData.GetData(id);

		// Set Reference Master
		this.master = CreateMaster(parameters);

		// Set Game Params
		this.hp = this.master.maxHp;

		// Set Hpbar Reference
		var hpBarName = string.Format("{0}pHpFrame", id);
		this.hpBar = GameObject.Find("L4").transform.Find(hpBarName).GetComponent<UIProgressBar>();
		this.hpBarAnimation = this.hpBar.gameObject.AddMissingComponent<AnimationComponent>();
	}

	public float ComputeHpRate(int hp)
	{
		return (float)hp / (float)this.master.maxHp;
	}

	private Master CreateMaster(Dictionary<string, object> parameters) 
	{
		var master = new Master();
		master.id = (int)parameters["id"];
		master.maxHp = (int)parameters["hp"];
		master.attack = (int)parameters["attack"];
		master.type = (Type)parameters["type"];
		master.name = (string)parameters["name"];
		return master;
    }

}
