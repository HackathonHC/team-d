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

	public Unit(int id)
	{
		// Load
		var parameters = UnitData.GetData(id);

		// Set Reference Master
		this.master = CreateMaster(parameters);

		// Set Game Params
		this.hp = this.master.maxHp;
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
