using UnityEngine;

[CreateAssetMenu(fileName = "Wait State", menuName = "Scriptables/Game States/Wait", order = 0)]
public class WaitForCallState : GameState
{
    [SerializeField] private float _waitForCallDuration = 5f;

    private float _waitTime;

    public override void Enter()
    {
        _waitTime = _waitForCallDuration;
    }

    public override void Tick()
    {
        _waitTime -= Time.deltaTime;

        if (_waitTime <= 0)
        {
            m_game.ChangeState(1);
        }
    }

    public override void Exit()
    {

    }
}
