using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
    public const float k = -0.0005f;
    public const string PlayerTag = "Player";
    public const int MinimumRequirePlayers = 2;
    public TextMeshProUGUI gameText;
    protected GameState gameState;
    protected IStateController stateController;
    protected IStateController[] availableStates;

    private void Awake()
    {
        availableStates = new IStateController[] { new PreGameState(this),
            new ActiveGameState(this),
            new PostGameState(this) };
    }

    private void Start()
    {
        if (gameState == GameState.None)
        {
            RpcSetGameState(GameState.Pregame);
        }
        //maybe make a cmd to the server to get the state?
    }

    void Update()
    {
        if (isServer && stateController != null)
        {
            stateController.OnUpdate();
        }
    }

    [ClientRpc]
    protected void RpcSetGameState(GameState state)
    {
        Debug.Log(isServer + " changing state to " + state);
        if (stateController != null)
        {
            stateController.OnExitState();
        }
        gameState = state;
        int stateControllerIndex = (int)gameState;
        stateController = availableStates[stateControllerIndex-1];
        stateController.OnEnterState();
    }

    [ClientRpc]
    public void RpcEndGame()
    {
        if (isServer)
        {
            RpcSetGameState(GameState.Postgame);
        }
        //Invoke("RpcStartGame", 3);
        //RestartGame();
    }

    [ClientRpc]
    public void RpcStartGame()
    {
        if (isServer)
        {
            RpcSetGameState(GameState.Active);
        }
    }

    [ClientRpc]
    public void RpcRestartGame()
    {
        NetworkManager nm = GameManager.FindObjectOfType<NetworkManager>();
        nm.ServerChangeScene("Map");
        RpcSetGameState(GameState.Active);
    }

    public bool IsPlayerDead(GameObject player)
    {
        if (player.transform.position.y < -1.0f)
        {
            return true;
        }
        return false;
    }

    public List<GameObject> GetActivePlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(PlayerTag);
        List<GameObject> playerList = new List<GameObject>();

        foreach (var player in players)
        {
            if (!IsPlayerDead(player))
            {
                playerList.Add(player);
            }
        }
        return playerList;
    }
}
public enum GameState
{
    None,
    Pregame,
    Active,
    Postgame
}
public interface IStateController
{
    void OnEnterState();
    void OnExitState();
    void OnUpdate();
}
public class PreGameState : StateControllerBase, IStateController
{
    public PreGameState(GameManager gameManager) : base(gameManager)
    {

    }

    public void OnEnterState()
    {
        gameManager.gameText.SetText("Waiting for other playes to join");
    }

    public void OnExitState()
    {
    }

    public void OnUpdate()
    {
        if (gameManager.isServer && gameManager.GetActivePlayers().Count >= GameManager.MinimumRequirePlayers)
        {
            gameManager.RpcStartGame();
        }
    }
}

public class ActiveGameState : StateControllerBase, IStateController
{
    public ActiveGameState(GameManager gameManager) : base(gameManager)
    {

    }

    public void OnEnterState()
    {
        gameManager.gameText.SetText("Fight!");
    }

    public void OnExitState()
    {
    }

    public void OnUpdate()
    {
        if (gameManager.isServer && gameManager.GetActivePlayers().Count <= 1)
        {
            gameManager.RpcEndGame();
        }
    }
}

public class PostGameState : StateControllerBase, IStateController
{
    public PostGameState(GameManager gameManager) : base(gameManager)
    {

    }

    public void OnEnterState()
    {
        gameManager.gameText.SetText("Player wins!");
    }

    public void OnExitState()
    {
    }

    public void OnUpdate()
    {
    }
}

public class StateControllerBase
{
    public GameManager gameManager;

    public StateControllerBase(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
