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
	private GameObject timerCallbackObject;

	public void Init()
	{
		var senkouPlayer = PlayerManager.Instance.Player1;
		this.currentPlayerId = senkouPlayer.playerId;
		this.diceButton = GameObject.Find("L2").transform.Find("DiceButton").GetComponent<UIButton>();
		this.timerCallbackObject = new GameObject();
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
		// Rest Once
		if (this.CurrentPlayer.unit.isRest)
		{
			this.CurrentPlayer.unitAnimation.Play(UnitAnimation.State.non);
			this.CurrentPlayer.unit.isRest = false;
			// End Turn
			OnPanelEffectEnd();
			return;
		}

		// 
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
				if (sourcePlayer.unit.IsDead || targetPlayer.unit.IsDead)
				{
					OnGameSet();
				}
				else
				{
					OnPanelEffectEnd();
				}
			});
		});
	}

	public void OnPanelEffectEnd()
	{
		DiceManager.Instance.state = DiceManager.State.init;

		if (this.currentPlayerId == 1)
		{
			this.currentPlayerId = 2;
		}
		else
		{
			this.currentPlayerId = 1;
		}

		var timerCallback = this.timerCallbackObject.AddMissingComponent<TimerCallback>();
		timerCallback.Reset();
		timerCallback.time = 0.7f;
		timerCallback.oneShot = true;
		timerCallback.SetCustomCallback(() =>
		{
			StartTurn();
		});
	}

	public void OnGameSet()
	{
		DiceManager.Instance.state = DiceManager.State.init;
		if (PlayerManager.Instance.Player1.unit.IsDead)
		{
			Win(PlayerManager.Instance.Player2);
		}
		else if (PlayerManager.Instance.Player2.unit.IsDead)
		{
			Win(PlayerManager.Instance.Player1);
		}
		else
		{
			Debug.LogError("error OnGameSet");
		}
	}

	public void Win(Player winner)
	{
		var winPrefab = Resources.Load<GameObject>("Prefabs/Win");
		var l5Object = GameObject.Find("L5");

		var winObject = NGUITools.AddChild(l5Object, winPrefab);
		var winSprite = winObject.GetComponent<UISprite>();

		// set sprite
		winSprite.spriteName = string.Format("{0}p_win", winner.playerId);

		// TODO game end
	}
}
