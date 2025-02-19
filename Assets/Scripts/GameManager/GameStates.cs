using System;
using Utils.StateMachine;

public enum GameStates
{
    PLAYER_TURN,
    ENEMY_TURN,
    WALKING,
    WIN,
    LOSE
}

public abstract class GameState : StateBase {
    protected GameManager _gameManager;

    public override void OnStateEnter(params object[] objs)
    {
        base.OnStateEnter(objs);
    }
}

public class GameStatePlayerTurn : GameState
{
    public GameStatePlayerTurn(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public override void OnStateEnter(params object[] objs)
    {
        base.OnStateEnter(objs);
        _gameManager.PlayerTurn();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        _gameManager.EndPlayerTurn();
    }
}

public class GameStateEnemyTurn : GameState
{
    public GameStateEnemyTurn(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public override void OnStateEnter(params object[] objs)
    {
        base.OnStateEnter(objs);
        _gameManager.EnemyTurn();
    }
}

public class GameStateWalking : GameState
{
    public GameStateWalking(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
}

public class GameStateWin : GameState
{
    public GameStateWin(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
}

public class GameStateLose : GameState
{
    public GameStateLose(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
}
