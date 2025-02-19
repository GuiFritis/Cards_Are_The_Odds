using UnityEngine;
using Utils.StateMachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Character _player;
    private Character _enemy;
    private StateMachineBase<GameStates> _stm;
    public static System.Action OnEnterPlayerTurn;
    public static System.Action OnExitPlayerTurn;

    void Awake()
    {
        InitStateMachine();   
        Character.OnTurnEnd += TurnShift;
        _player.Health.OnDeath += GameOver;
    }

    private void InitStateMachine()
    {
        _stm = new();
        _stm.Init();
        _stm.RegisterStates(GameStates.PLAYER_TURN, new GameStatePlayerTurn(this));
        _stm.RegisterStates(GameStates.ENEMY_TURN, new GameStateEnemyTurn(this));
        _stm.RegisterStates(GameStates.WALKING, new GameStateWalking(this));
        _stm.RegisterStates(GameStates.WIN, new GameStateWin(this));
        _stm.RegisterStates(GameStates.LOSE, new GameStateLose(this));
        _stm.SwitchState(GameStates.PLAYER_TURN);
    }

    private void TurnShift(Character character)
    {
        if(character.Equals(_player))
        {
            _stm.SwitchState(GameStates.ENEMY_TURN);
        }
        else
        {
            _stm.SwitchState(GameStates.PLAYER_TURN);
        }
    }

    public void PlayerTurn()
    {
        _player.StartTurn();
        OnEnterPlayerTurn?.Invoke();
    }

    public void EndPlayerTurn()
    {
        OnExitPlayerTurn?.Invoke();
    }

    public void EnemyTurn()
    {
        _enemy.StartTurn();
    }

    private void GameOver(HealthBase hp)
    {
        _stm.SwitchState(GameStates.LOSE);
    }
}
