using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;
	public static GameManager Instance
	{
		get
		{
			return instance ?? (instance = GameObject.Find("GameManager").GetComponent<GameManager>());
		}
	}

	private AnimationManager animationManager;
	public Player.Parameter playerParameter1;
	public Player.Parameter playerParameter2;

	void Awake ()
	{
		Init();
	}


	public void Init()
	{
		this.playerParameter1 = new Player.Parameter(1, 1, 1); // Player Id, Unit Id, Stage Id
		this.playerParameter2 = new Player.Parameter(2, 1, 1);
	}

	public void GameStart()
	{
		// Initialize Manager
		PlayerManager.Instance.Init(this.playerParameter1, this.playerParameter2);
		UnitManager.Instance.Init();
		AnimationManager.Instance.Init();
		PanelManager.Instance.Init();
		CursolManager.Instance.Init();
		UIAnimationManager.Instance.Init();
		TurnManager.Instance.Init();
		DamageNumberManager.Instance.Init();
		// GameStart
		TurnManager.Instance.StartTurn();
		SoundManager.Instance.PlayBGM("game_bgm");
	}
}
