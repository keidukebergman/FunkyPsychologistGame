using UnityEngine;

[CreateAssetMenu(fileName = "Commercial State", menuName = "Scriptables/Game States/Commercial", order = 0)]
public class CommercialState : GameState
{

    ProgramSwitchScript switchScript;

    [SerializeField] private float _stateDuration = 10;
    [SerializeField] private float _commercialDelay = 5;

    private float _stateTime;
    private float _comercialDelayTime;

    private bool _playsCommercial = false;

    public override void Initialize(GameManager game, Player player, PlayerInput input)
    {
        base.Initialize(game, player, input);

        switchScript = game.TVSwitch;
    }

    public override void Enter()
    {
        switchScript.SwitchToNews();

        _stateTime = Time.time + _stateDuration;
        _comercialDelayTime = Time.time + _commercialDelay;

        _playsCommercial = false;
    }

    public override void Tick()
    {

        if (_playsCommercial)
        {
            if (Time.time >= _stateTime)
            {
                m_game.ChangeState(0);
                return;
            }
        }
        else
        {
            if (Time.time >= _comercialDelayTime)
            {
                switchScript.SwitchToCommercial();
                _playsCommercial = true;
            }
        }
    }

    public override void Exit()
    {

    }
}
