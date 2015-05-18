using UnityEngine;
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
			// Compute damage
			var damagePoint = sourcePlayer.unit.master.attack;

			// Compute Bar From
			var from = targetPlayer.unit.ComputeHpRate(targetPlayer.unit.hp);

			// Reduce target unit's Hp
			targetPlayer.unit.hp -= damagePoint;

			// Compute Bar To
			var to = targetPlayer.unit.ComputeHpRate(targetPlayer.unit.hp);

			// Animation Hp Bar
			targetPlayer.unit.hpBarAnimation.HpBar(from, to);

			// Show Damage
			DamageNumberManager.Instance.Show(damagePoint, targetPlayer.unitAnimation);

			// Damage Animation
			targetPlayer.unitAnimation.PlayOnce(UnitAnimation.State.damage);
			this.deferred.Resolve();
		});
	}

	private void Special(Player sourcePlayer, Player targetPlayer)
	{
		sourcePlayer.unitAnimation.PlayOnce(UnitAnimation.State.special).Done(()=>
		{
			// Compute damage
			var damagePoint = sourcePlayer.unit.master.attack * 2;

			// Compute Bar From
			var from = targetPlayer.unit.ComputeHpRate(targetPlayer.unit.hp);

			// Reduce target unit's Hp
			targetPlayer.unit.hp -= damagePoint;

			// Compute Bar To
			var to = targetPlayer.unit.ComputeHpRate(targetPlayer.unit.hp);

			// Animation Hp Bar
			targetPlayer.unit.hpBarAnimation.HpBar(from, to);

			// Show Damage
			DamageNumberManager.Instance.Show(damagePoint, targetPlayer.unitAnimation);

			// Damage Animation
			targetPlayer.unitAnimation.PlayOnce(UnitAnimation.State.damage);
			this.deferred.Resolve();
		});
	}

	private void Damage(Player sourcePlayer, Player targetPlayer)
	{
		// Compute damage
		var damagePoint = 150;

		// Compute Bar From
		var from = sourcePlayer.unit.ComputeHpRate(sourcePlayer.unit.hp);

		// Reduce target unit's Hp
		sourcePlayer.unit.hp -= damagePoint;

		// Compute Bar To
		var to = sourcePlayer.unit.ComputeHpRate(sourcePlayer.unit.hp);

		// Animation Hp Bar
		sourcePlayer.unit.hpBarAnimation.HpBar(from, to);

		// Show Damage
		DamageNumberManager.Instance.Show(damagePoint, sourcePlayer.unitAnimation);

		// Damage Animation
		sourcePlayer.unitAnimation.PlayOnce(UnitAnimation.State.damage).Done(()=>
		{
			this.deferred.Resolve();
		});
	}

	private void Dead(Player sourcePlayer, Player targetPlayer)
	{
		// Compute damage
		var damagePoint = 9999;

		// Compute Bar From
		var from = sourcePlayer.unit.ComputeHpRate(sourcePlayer.unit.hp);

		// Reduce target unit's Hp
		sourcePlayer.unit.hp -= damagePoint;

		// Compute Bar To
		var to = sourcePlayer.unit.ComputeHpRate(sourcePlayer.unit.hp);

		// Animation Hp Bar
		sourcePlayer.unit.hpBarAnimation.HpBar(from, to);

		// Show Damage
		DamageNumberManager.Instance.Show(damagePoint, sourcePlayer.unitAnimation);

		// Damage Animation
		sourcePlayer.unitAnimation.PlayOnce(UnitAnimation.State.dead).Done(()=>
		{
			this.deferred.Resolve();
		});
	}

	private void Recover(Player sourcePlayer, Player targetPlayer)
	{
		// Compute damage
		var damagePoint = 300;
		
		// Compute Bar From
		var from = sourcePlayer.unit.ComputeHpRate(sourcePlayer.unit.hp);
		
		// Add target unit's Hp
		sourcePlayer.unit.hp += damagePoint;
		
		// Compute Bar To
		var to = sourcePlayer.unit.ComputeHpRate(sourcePlayer.unit.hp);
		
		// Animation Hp Bar
		sourcePlayer.unit.hpBarAnimation.HpBar(from, to);

		// Show Heal
		DamageNumberManager.Instance.Show(damagePoint, sourcePlayer.unitAnimation);
		
		// Recover Animation
		sourcePlayer.unitAnimation.PlayOnce(UnitAnimation.State.recover).Done(()=>
		{
			this.deferred.Resolve();
		});
	}

	private void Rest(Player sourcePlayer, Player targetPlayer)
	{
		// TODO
		sourcePlayer.unitAnimation.Play(UnitAnimation.State.rest);
		this.deferred.Resolve();
	}

	private void Rand(Player sourcePlayer, Player targetPlayer)
	{
		// NOTE: Random
		//		攻撃マス	15
		//		必殺技マス	10
		//		ダメージマス	23
		//		一撃死マス	2
		//		回復マス	20
		//		一回休みマス	10
		//		空白マス	20

		var rand = Random.Range(0, 100);
		if (rand < 15)
		{
			Attack(sourcePlayer, targetPlayer);
		}
		else if (rand < 25)
		{
			Special(sourcePlayer, targetPlayer);
		}
		else if (rand < 48)
		{
			Damage(sourcePlayer, targetPlayer);
		}
		else if (rand < 50)
		{
			Dead(sourcePlayer, targetPlayer);
		}
		else if (rand < 70)
		{
			Recover(sourcePlayer, targetPlayer);
		}
		else if (rand < 80)
		{
			Rest(sourcePlayer, targetPlayer);
		}
		else
		{
			None(sourcePlayer, targetPlayer);
		}
	}

	private void None(Player sourcePlayer, Player targetPlayer)
	{
		sourcePlayer.unitAnimation.Play(UnitAnimation.State.non);
		this.deferred.Resolve();
	}
}
