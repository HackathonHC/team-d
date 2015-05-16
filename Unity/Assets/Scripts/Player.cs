using UnityEngine;
using System.Collections;

public class Player
{
	public int playerId;
	public int unitId;
	public UnitAnimation unitAnimation;

	public struct Parameter
	{
		public int playerId;
		public int unitId;

		public Parameter(int playerId, int unitId)
		{
			this.playerId = playerId;
			this.unitId = unitId;
		}
	}

	public void Init(Parameter parameter)
	{
		this.playerId = parameter.playerId;
		this.unitId = parameter.unitId;
		this.unitAnimation = null;
	}
}
