using UnityEngine;
using System.Collections;

public class Cursol
{
	public GameObject gameObject;
	public UISprite outerSprite;
	public UISprite playerSprite;
	private Player player;
	private int CURSOL_START_INDEX = 0;

	public void SetUp(Player player, GameObject cursolObject)
	{
		this.player = player;
		this.gameObject = cursolObject;
		this.playerSprite = this.gameObject.transform.Find("Player").GetComponent<UISprite>();
		this.outerSprite = this.gameObject.transform.Find("Outer").GetComponent<UISprite>();

		// Set Sprite
		this.playerSprite.spriteName = GetPlayerSpriteName(this.player.playerId);
		this.outerSprite.spriteName = GetOuterSpriteName(this.player.playerId);

		// Set Position
		this.gameObject.transform.localPosition = GetCursolPositionByIndex(CURSOL_START_INDEX);
	}

	private string GetPlayerSpriteName(int playerId)
	{
		return string.Format("{0}p_select_", playerId);
	}

	private string GetOuterSpriteName(int playerId)
	{
		return string.Format("{0}p_select", playerId);
	}

	private Vector3 GetCursolPositionByIndex(int cursolIndex)
	{
		return StageData.PANEL_POSITION_MAP[cursolIndex];
	}
}
