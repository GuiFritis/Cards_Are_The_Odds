using System.Collections.Generic;
using DG.Tweening;
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
    [SerializeField] private SpriteRenderer _enemySprite;
    [Header("UI")]
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;
    private int _enemyIndex = -1;
    private StateMachineBase<GameStates> _stm;
    public static System.Action OnEnterPlayerTurn;
    public static System.Action OnExitPlayerTurn;
    public static System.Action<Character> OnEnemySpawned;

    protected override void Awake()
    {
        base.Awake();
        Character.OnTurnEnd += TurnShift;
        _player.Health.OnDeath += GameOver;
    }

    void Start()
    {   
        _enemySprite.color = Color.clear;
        NextEnemy();
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

    private void EnemyDeath(HealthBase hp)
    {
        _enemySprite.DOColor(Color.clear, 1f).OnComplete(
            () => {
                _enemy.gameObject.SetActive(false);
                NextEnemy();
            }
        );
    }

    private void NextEnemy()
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
        _enemySprite.sprite = _enemiesList[_enemyIndex].EnemySprite;
        _enemySprite.DOColor(Color.white, 1f).OnComplete(
            () => {
                _enemy = _enemiesList[_enemyIndex].GetComponent<Character>();
                _enemy = Instantiate(_enemy, transform.parent);
                _enemy.transform.SetSiblingIndex(transform.GetSiblingIndex()-1);
                _enemy.Health.OnDeath += EnemyDeath;
                _enemy.Health.OnDamage += FlashSprite;
                OnEnemySpawned?.Invoke(_enemy);
                _player.Cleanse();
                _stm.SwitchState(GameStates.PLAYER_TURN);
            }
        );
    }

    private void FlashSprite(HealthBase hp, int damage)
    {
        if(damage > 0)
        {
            _enemySprite.DOKill();
            _enemySprite.color = Color.white;
            _enemySprite.DOColor(Color.red, .15f).SetLoops(2, LoopType.Yoyo);
        }
    }

    #region WIN
    public void Win()
    {
        _winScreen.SetActive(true);
    }
    #endregion


    #region LOSE
    private void GameOver(HealthBase hp)
    {
        _stm.SwitchState(GameStates.LOSE);
    }

    public void LoseGame()
    {
        _loseScreen.SetActive(true);
    }
    #endregion
}
