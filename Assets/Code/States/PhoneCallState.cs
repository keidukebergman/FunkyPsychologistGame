using UnityEngine;

[CreateAssetMenu(fileName = "Call State", menuName = "Scriptables/Game States/Call", order = 0)]
public class PhoneCallState : GameState
{


    public override void Initialize(GameManager game, PlayerInput player)
    {
        base.Initialize(game, player);
    }

    public override void Enter()
    {

    }

    public override void Tick()
    {
        if (m_player.MouseLeftDown)
        {
            m_game.ChangeState(0);
        }
    }

    public override void Exit()
    {

    }
}
