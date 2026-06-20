using UnityEngine;

[CreateAssetMenu(fileName = "Win State", menuName = "Scriptables/Game States/Win", order = 2)]
public class WinState : GameState
{
    public override void Enter()
    {
        m_hud.ShowVictory();
    }

    public override void Tick()
    {
        if (m_input.SpacebarDown) {
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
