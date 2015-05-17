using UnityEngine;
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
		attack   = 1,
		special  = 2,
		damage   = 3,
		dead     = 4,
		recover  = 5,
		rest     = 6,
		random   = 7,
		non      = 8,
	}

	public void Init(Player player, GameObject containerObject, GameObject animationObject)
	{
		this.player = player;
		this.containerObject = containerObject;
		this.animationObject = animationObject;
		this.status = State.non;
		this.ssPartsRoot = animationObject.GetComponent<Script_SpriteStudio_PartsRoot>();

		this.containerObject.name = GetContainerName(this.player);
		this.containerObject.transform.localScale = GetScale(this.player);
		this.containerObject.transform.localPosition = GetPosition(this.player);
		this.animationObject.name = "Animation";
		this.containerObject.layer = LayerMask.NameToLayer("2DUI");

		Play(State.non);
	}

	public bool IsWait
	{
		get { return this.status == State.non; }
	}

	public bool IsAttack
	{
		get { return this.status == State.attack; }
	}

	public void Play(State state)
	{
		var motionName = state.ToString();
		var index = GetAnimationIndex(motionName);
		this.ssPartsRoot.AnimationPlay(index);
		this.status = state;
	}

	public Promises.Deferred PlayOnce(State state)
	{
		var deferred = new Promises.Deferred();

		var motionName = state.ToString();
		var index = GetAnimationIndex(motionName);
		this.ssPartsRoot.AnimationPlay(index, 1);
		this.status = state;

        this.ssPartsRoot.FunctionPlayEnd = (_) =>
        {
			if (this.player.unit.isRest)
			{
				Play(State.rest);
			}
			else
			{
            	Play(State.non);
			}
            deferred.Resolve();
            return true;
        };

		return deferred;
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
			return new Vector3(0.5f, 0.5f);
		}
		else
		{
			return new Vector3(-0.5f, 0.5f);
		}
	}

	private Vector3 GetPosition(Player player)
	{
		if (player.playerId == 1)
		{
//			return new Vector3(-216f, 0f);
			return new Vector3(-240f, 0f);
		}
		else
		{
//			return new Vector3(236f, 0f);
			return new Vector3(240f, 0f);
		}
	}
}
