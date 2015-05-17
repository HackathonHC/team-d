﻿using UnityEngine;
using System.Collections;

public class Panel
{
	public enum Type
	{
		Attack  = 1,
		Special = 2,
		Damage  = 3,
		Dead   	= 4,
		Recover = 5,
		Rest  	= 6,
		Rand    = 7,
		Non   	= 8,
	}

	public Type type;
	public GameObject gameObject;
	public UISprite sprite;

	public Panel(Type type)
	{
		this.type = type;
	}

	public void SetUp(GameObject panelObject)
	{
		this.gameObject = panelObject;
		this.sprite = this.gameObject.GetComponent<UISprite>();
		this.sprite.spriteName = GetSpriteNameByPanelType(this.type);
		this.sprite.MakePixelPerfect();
	}

	private Promises.Deferred deferred;
	public Promises.Deferred Execute(Player sourcePlayer, Player targetPlayer)
	{
		this.deferred = new Promises.Deferred();

		switch (this.type)
		{
			case Type.Attack:
				Attack(sourcePlayer, targetPlayer);
				break;
			case Type.Special:
				Special(sourcePlayer, targetPlayer);
				break;
			case Type.Damage:
				Damage(sourcePlayer, targetPlayer);
				break;
			case Type.Dead:
				Dead(sourcePlayer, targetPlayer);
				break;
			case Type.Recover:
				Recover(sourcePlayer, targetPlayer);
				break;
			case Type.Rest:
				Rest(sourcePlayer, targetPlayer);
				break;
			case Type.Rand:
				Rand(sourcePlayer, targetPlayer);
				break;
			case Type.Non:
				None(sourcePlayer, targetPlayer);
				break;
		}

		return this.deferred;
	}

	private string GetSpriteNameByPanelType(Type panelType)
	{
		return StageData.PANEL_SPRITE_NAME_MAP[panelType];
	}

	private void Attack(Player sourcePlayer, Player targetPlayer)
	{
		sourcePlayer.unitAnimation.PlayOnce(UnitAnimation.State.attack).Done(()=>
		{
			targetPlayer.unitAnimation.PlayOnce(UnitAnimation.State.damage);
			this.deferred.Resolve();
		});
	}

	private void Special(Player sourcePlayer, Player targetPlayer)
	{
		sourcePlayer.unitAnimation.PlayOnce(UnitAnimation.State.special).Done(()=>
		{
			targetPlayer.unitAnimation.PlayOnce(UnitAnimation.State.damage);
			this.deferred.Resolve();
		});
	}

	private void Damage(Player sourcePlayer, Player targetPlayer)
	{
		sourcePlayer.unitAnimation.PlayOnce(UnitAnimation.State.damage).Done(()=>
		{
			this.deferred.Resolve();
		});
	}

	private void Dead(Player sourcePlayer, Player targetPlayer)
	{
		sourcePlayer.unitAnimation.PlayOnce(UnitAnimation.State.dead).Done(()=>
		{
			this.deferred.Resolve();
		});
	}

	private void Recover(Player sourcePlayer, Player targetPlayer)
	{
		sourcePlayer.unitAnimation.PlayOnce(UnitAnimation.State.recover).Done(()=>
		                                                                      {

			this.deferred.Resolve();
		});
	}

	private void Rest(Player sourcePlayer, Player targetPlayer)
	{
		sourcePlayer.unitAnimation.Play(UnitAnimation.State.rest);
		this.deferred.Resolve();
	}

	private void Rand(Player sourcePlayer, Player targetPlayer)
	{
		sourcePlayer.unitAnimation.Play(UnitAnimation.State.non);
		this.deferred.Resolve();
	}

	private void None(Player sourcePlayer, Player targetPlayer)
	{
		sourcePlayer.unitAnimation.Play(UnitAnimation.State.non);
		this.deferred.Resolve();
	}
}
