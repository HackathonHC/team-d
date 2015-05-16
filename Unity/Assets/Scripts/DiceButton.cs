using UnityEngine;
using System.Collections;

public class DiceButton : MonoBehaviour
{
	private UISprite sprite;

	void Awake()
	{
		this.sprite = this.transform.Find("Sprite").GetComponent<UISprite>();
	}

	public void OnButtonClick()
	{
		Debug.Log("click!");

		// Change Animation Test
//		foreach (var player in PlayerManager.Instance.PlayerList)
//		{
//			var animation = AnimationManager.Instance.FindUnitAnimation(player.playerId);
//			if (animation.IsWait)
//			{
//				animation.Play(UnitAnimation.State.attack1);
//			}
//			else
//			{
//				animation.Play(UnitAnimation.State.wait);
//			}
//		}

		DiceManager.Instance.diceRole();
	}
}
