using UnityEngine;

[CreateAssetMenu(fileName = "Commercial State", menuName = "Scriptables/Game States/Commercial", order = 0)]
public class CommercialState : GameState
{

    ProgramSwitchScript switchScript;

    [SerializeField] private float _stateDuration = 10;
    [SerializeField] private float _commercialDelay = 5;

    private float _stateTime;
    private float _comercialDelayTime;

    public override void Initialize(GameManager game, Player player, PlayerInput input)
    {
        base.Initialize(game, player, input);

        switchScript = game.SwitchScript;
    }

    public override void Enter()
    {
        switchScript.SwitchToNews();

        _stateTime = Time.time + _stateDuration;
        _comercialDelayTime = Time.time + _commercialDelay;
    }

    public override void Tick()
    {

        if (Time.time >= _stateTime)
        {
            m_game.ChangeState(0);
            return;
        }
        if (Time.time >= _comercialDelayTime)
        {
            switchScript.SwitchToCommercial();
        }
        
    }

    public override void Exit()
    {

    }
}
