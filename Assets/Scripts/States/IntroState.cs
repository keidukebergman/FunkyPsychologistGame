using UnityEngine;

[CreateAssetMenu(fileName = "Intro State", menuName = "Scriptables/Game States/Intro", order = 0)]
public class IntroState : GameState
{
    [SerializeField] private float m_introStateDuration = 4;

    private float m_stateTime;

    public override void Enter()
    {
        m_hud.PlayIntro();
        m_stateTime = Time.time + m_introStateDuration;
    }

    public override void Tick()
    {
        if (Time.time >= m_stateTime)
        {
            m_game.ChangeState(0);
        }
    }

    public override void Exit()
    {

    }
}
