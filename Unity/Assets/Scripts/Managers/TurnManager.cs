using UnityEngine;
using System.Collections;

public class TurnManager
{
	private static TurnManager instance;
	public static TurnManager Instance
	{
		get
		{
			return instance ?? (instance = new TurnManager());
		}
	}

	public enum State
	{
		TurnStartAnimation = 1,
		WaitDice 		   = 2,
		RoleDice 		   = 3,
	}

	private int currentPlayerId;
	private Promises.Deferred deferred;

	public void Init()
	{
		var senkouPlayer = PlayerManager.Instance.Player1;
		this.currentPlayerId = senkouPlayer.playerId;
	}

	public Player CurrentPlayer
	{
		get
		{
			return PlayerManager.Instance.FindPlayer(this.currentPlayerId);
		}
	}

	public void StartTurn()
	{
//		UIAnimationManager.Instance.PlayAnimation(UIAnimationManager.Type.BattleStart);
		UIAnimationManager.Instance.PlayTurnAnimation(this.CurrentPlayer);
	}
}
