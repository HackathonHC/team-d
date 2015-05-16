﻿using UnityEngine;
using System.Collections;

public class UnitAnimation : MonoBehaviour 
{
	private Player player;
	private GameObject containerObject;
	private GameObject animationObject;
	private State status;
	public Script_SpriteStudio_PartsRoot ssPartsRoot;

	public enum State
	{
		wait,
		attack1,
	}

	public void Init(Player player, GameObject containerObject, GameObject animationObject)
	{
		this.player = player;
		this.containerObject = containerObject;
		this.animationObject = animationObject;
		this.status = State.wait;
		this.ssPartsRoot = animationObject.GetComponent<Script_SpriteStudio_PartsRoot>();

		this.containerObject.name = GetContainerName(this.player);
		this.containerObject.transform.localScale = GetScale(this.player);
		this.containerObject.transform.localPosition = GetPosition(this.player);
		this.animationObject.name = "Animation";
		this.containerObject.layer = LayerMask.NameToLayer("2DUI");

		Play(State.wait);
	}

	public bool IsWait
	{
		get { return this.status == State.wait; }
	}

	public bool IsAttack
	{
		get { return this.status == State.attack1; }
	}

	public void Play(State state)
	{
		var motionName = state.ToString();
		var index = GetAnimationIndex(motionName);
		this.ssPartsRoot.AnimationPlay(index);
		this.status = state;
	}

	public int GetAnimationIndex(string motionName)
	{
		return this.ssPartsRoot.AnimationGetIndexNo(motionName);
	}

	private string GetContainerName(Player player)
	{
		return string.Format("P{0}", player.playerId);
	}

	private Vector3 GetScale(Player player)
	{
		if (player.playerId == 1)
		{
			return new Vector3(-0.5f, 0.5f);
		}
		else
		{
			return new Vector3(0.5f, 0.5f);
		}
	}

	private Vector3 GetPosition(Player player)
	{
		if (player.playerId == 1)
		{
			return new Vector3(-216f, -151f);
		}
		else
		{
			return new Vector3(236f, -151f);
		}
	}
}