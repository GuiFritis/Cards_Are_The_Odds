using System;
using Utils.StateMachine;

public enum GameStates
{
    PLAYER_TURN,
    ENEMY_TURN
}

public class GameState : StateBase {}

public class GameStatePlayerTurn : GameState
{
    public static Action StartPlayerTurn;

    public override void OnStateEnter(params object[] objs)
    {
        base.OnStateEnter(objs);
        StartPlayerTurn?.Invoke();
    }
}

public class GameStateEnemyTurn : GameState
{
    public static Action StartEnemyTurn;

    public override void OnStateEnter(params object[] objs)
    {
        base.OnStateEnter(objs);
        StartEnemyTurn?.Invoke();
    }
}
