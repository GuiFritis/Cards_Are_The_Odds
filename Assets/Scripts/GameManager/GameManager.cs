using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.StateMachine;

public class GameManager : MonoBehaviour
{
    private StateMachineBase<GameStates> _stm;

    void Awake()
    {
        InitStateMachine();   
    }

    private void InitStateMachine()
    {
        _stm = new();
        _stm.Init();
        _stm.RegisterStates(GameStates.PLAYER_TURN, new GameStatePlayerTurn());
        _stm.RegisterStates(GameStates.ENEMY_TURN, new GameStateEnemyTurn());
    }
}
