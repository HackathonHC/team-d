using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cursol
{
	public GameObject gameObject;
	public UISprite outerSprite;
	public UISprite playerSprite;
	private Player player;
	private int CURSOL_START_INDEX = 0;
	public int currentCursolIndex;
	public AnimationComponent animationComponent;

	public void SetUp(Player player, GameObject cursolObject)
	{
		this.player = player;
		this.gameObject = cursolObject;
		this.playerSprite = this.gameObject.transform.Find("Player").GetComponent<UISprite>();
		this.outerSprite = this.gameObject.transform.Find("Outer").GetComponent<UISprite>();
		this.currentCursolIndex = 0;

		// Set Sprite
		this.playerSprite.spriteName = GetPlayerSpriteName(this.player.playerId);
		this.outerSprite.spriteName = GetOuterSpriteName(this.player.playerId);

		// Set Position
		this.gameObject.transform.localPosition = GetCursolPositionByIndex(CURSOL_START_INDEX);

		// Set Animation Component
		this.animationComponent = this.gameObject.AddMissingComponent<AnimationComponent>();
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

	private int reserveProgressCount;
	private Promises.Deferred deferred;

	public Promises.Deferred Progress(int value)
	{
		this.deferred = new Promises.Deferred();

		this.reserveProgressCount = value;
		ProgressCore();

		return this.deferred;
	}

	private void ProgressCore()
	{
		// reduce count
		this.reserveProgressCount -= 1;

		var from = StageData.PANEL_POSITION_MAP[this.currentCursolIndex];
		var to =  StageData.PANEL_POSITION_MAP[NextIndex(this.currentCursolIndex)];

		// move next
		this.animationComponent.Move(0.1f, from, to).Done(()=>
		                                                  {
			if (this.reserveProgressCount > 0)
			{
				ProgressCore();
			}
			else
			{
				this.deferred.Resolve();
			}
		});

		// add count
		this.currentCursolIndex = NextIndex(this.currentCursolIndex);
	}

	public int NextIndex(int current)
	{
		var next = current + 1;
		if (next > (PanelManager.Instance.PANEL_SIZE - 1))
		{
			return 0;
		}
		return next;
	}

	public int PrevIndex(int current)
	{
		var prev = current - 1;
		if (prev < 0)
		{
			return PanelManager.Instance.PANEL_SIZE - 1;
		}
		return prev;
	}
}
