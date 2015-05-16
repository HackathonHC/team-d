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

	public void Init()
	{
		this.player1 = new Player();
		this.player2 = new Player();
		this.playerList = new List<Player>(PLAYER_SIZE) { this.player1, this.player2 };

		var playerPalameter1 = new Player.Parameter(1, 0); // Player Id, Unit Id
		var playerPalameter2 = new Player.Parameter(2, 0);

		this.player1.Init(playerPalameter1);
		this.player2.Init(playerPalameter2);
	}

	public List<Player> PlayerList
	{
		get
		{
			return this.playerList;
		}
	}
}
