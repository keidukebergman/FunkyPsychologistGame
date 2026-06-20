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
        if (m_input.SpacebarDown)
        {
            m_game.ChangeState(5);
        }
    }

    public override void Exit()
    {
        m_hud.Restore();
        m_game.UnlockableManager.Restore();
        m_game.FatigueManager.ResetFatigue();
        m_game.ClientManager.ReInit();
        m_game.TVSwitch.RestartUnlockableOrder();
    }
}
