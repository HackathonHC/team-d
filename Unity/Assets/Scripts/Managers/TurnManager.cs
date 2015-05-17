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
	private UIButton diceButton;

	public void Init()
	{
		var senkouPlayer = PlayerManager.Instance.Player1;
		this.currentPlayerId = senkouPlayer.playerId;
		this.diceButton = GameObject.Find("L2").transform.Find("DiceButton").GetComponent<UIButton>();
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
		if (this.currentPlayerId == 1)
		{
			PlayerManager.Instance.Player1.playerStage.animationComponent.FadeIn();
			PlayerManager.Instance.Player2.playerStage.animationComponent.FadeOut();
		}
		else
		{
			PlayerManager.Instance.Player1.playerStage.animationComponent.FadeOut();
			PlayerManager.Instance.Player2.playerStage.animationComponent.FadeIn();
		}

		UIAnimationManager.Instance.PlayTurnAnimation(this.CurrentPlayer).Done(()=>
		                                                                       {
			this.diceButton.gameObject.SetActive(true);
		});
	}

	public void OnDiceRoleEnd(int value)
	{
		this.CurrentPlayer.cursol.Progress(value).Done(()=>
		{
			var currentIndex = this.CurrentPlayer.cursol.currentCursolIndex;
			var currentPanel = this.CurrentPlayer.playerStage.panelList[currentIndex];

			Player sourcePlayer;
			Player targetPlayer;
			if (this.currentPlayerId == 1)
			{
				sourcePlayer = PlayerManager.Instance.Player1;
				targetPlayer = PlayerManager.Instance.Player2;
			}
			else
			{
				sourcePlayer = PlayerManager.Instance.Player2;
				targetPlayer = PlayerManager.Instance.Player1;
			}
				
			currentPanel.Execute(sourcePlayer, targetPlayer).Done(()=>
			{
				OnPanelEffectEnd();
			});
		});
	}

	public void OnPanelEffectEnd()
	{
		if (this.currentPlayerId == 1)
		{
			this.currentPlayerId = 2;
		}
		else
		{
			this.currentPlayerId = 1;
		}
		StartTurn();
	}
}
