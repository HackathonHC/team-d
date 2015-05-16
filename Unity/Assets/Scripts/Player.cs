using UnityEngine;
using System.Collections;

public class Player
{
	public int playerId;
	public int unitId;
	public int stageId;
	public PanelManager.PlayerStage playerStage;
	public UnitAnimation unitAnimation;

	public struct Parameter
	{
		public int playerId;
		public int unitId;
		public int stageId;

		public Parameter(int playerId, int unitId, int stageId)
		{
			this.playerId = playerId;
			this.unitId = unitId;
			this.stageId = stageId;
		}
	}

	public void Init(Parameter parameter)
	{
		this.playerId = parameter.playerId;
		this.unitId = parameter.unitId;
		this.stageId = parameter.stageId;
		this.unitAnimation = null;
	}
}
