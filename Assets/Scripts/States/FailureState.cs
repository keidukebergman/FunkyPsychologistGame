using UnityEngine;

[CreateAssetMenu(fileName = "Failure State", menuName = "Scriptables/Game States/Failure", order = 3)]
public class FailureState : GameState
{
    public override void Enter()
    {
        m_hud.ShowFailure();
    }

    public override void Tick()
    {

    }

    public override void Exit()
    {

    }
}
