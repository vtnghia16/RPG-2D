using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class chuyển đổi trạng thái của nhân vật
public class PlayerStateMachine 
{
    public PlayerState currentState { get; private set; }

    // Hàm khởi tạo trạng thái nhân vật
    public void Initialize(PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(PlayerState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
