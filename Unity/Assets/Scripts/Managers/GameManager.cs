using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private AnimationManager animationManager;

	void Awake ()
	{
		Init();
	}

	private void Init()
	{
		PlayerManager.Instance.Init();
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
