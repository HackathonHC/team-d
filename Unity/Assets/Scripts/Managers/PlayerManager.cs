using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager
{
	private static PlayerManager instance;
	public static PlayerManager Instance
	{
		get
		{
			return instance ?? (instance = new PlayerManager());
		}
	}

	private Player player1;
	private Player player2;
	private List<Player> playerList;
	private int PLAYER_SIZE = 2;

	public void Init(Player.Parameter playerParameter1, Player.Parameter playerParameter2)
	{
		this.player1 = new Player();
		this.player2 = new Player();
		this.playerList = new List<Player>(PLAYER_SIZE) { this.player1, this.player2 };

		this.player1.Init(playerParameter1);
		this.player2.Init(playerParameter2);
	}

	public Player Player1
	{
		get { return this.player1; }
	}

	public Player Player2
	{
		get { return this.player2; }
	}

	public List<Player> PlayerList
	{
		get
		{
			return this.playerList;
		}
	}
		
	public Player FindPlayer(int playerId)
	{
		if (playerId == 1)
		{
			return this.player1;
		}
		else
		{
			return this.player2;
		}
	}
}
