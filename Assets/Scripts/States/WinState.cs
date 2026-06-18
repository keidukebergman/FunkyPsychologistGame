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

    }

    public override void Exit()
    {

    }
}
