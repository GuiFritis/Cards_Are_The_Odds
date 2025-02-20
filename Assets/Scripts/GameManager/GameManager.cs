using System.Collections.Generic;
using UnityEngine;
using Utils.Singleton;
using Utils.StateMachine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Character _player;
    public Character GetPlayer => _player;
    [SerializeField] private Character _enemy;
    public Character GetEnemy => _enemy;
    [SerializeField] private List<Enemy> _enemiesList = new();
    private int _enemyIndex = 0;
    private StateMachineBase<GameStates> _stm;
    public static System.Action OnEnterPlayerTurn;
    public static System.Action OnExitPlayerTurn;
    public static System.Action<Character> OnEnemySpawned;

    protected override void Awake()
    {
        base.Awake();
        Character.OnTurnEnd += TurnShift;
        _player.Health.OnDeath += GameOver;
        _enemy.Health.OnDeath += NextEnemy;
    }

    void Start()
    {        
        InitStateMachine();   
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

    private void NextEnemy(HealthBase hp)
    {
        _enemyIndex++;
        if(_enemyIndex >= _enemiesList.Count)
        {
            _stm.SwitchState(GameStates.WIN);
        }
        else
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        _enemy = _enemiesList[_enemyIndex].GetComponent<Character>();
        OnEnemySpawned?.Invoke(_enemy);
    }

    private void GameOver(HealthBase hp)
    {
        _stm.SwitchState(GameStates.LOSE);
    }
}
