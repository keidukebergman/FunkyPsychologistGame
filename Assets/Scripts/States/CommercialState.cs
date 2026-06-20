using System;
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
    private bool FellAsleep { get; set; }

    public override void Initialize(GameManager game, Player player, PlayerInput input)
    {
        base.Initialize(game, player, input);

        switchScript = game.TVSwitch;
        var fatigueManager = m_player.GetComponentInChildren<FatigueManager>();
        fatigueManager.OnSleep += OnSleep;
    }

    private void OnSleep()
    {
        FellAsleep = true;
    }

    public override void Enter()
    {
        //switchScript.SwitchToNews();

        _stateTime = Time.time + _stateDuration;
        _comercialDelayTime = Time.time + 0;// _commercialDelay;

        _playsCommercial = false;
        FellAsleep = false;
    }

    public override void Tick()
    {
        if (FellAsleep)
        {
            m_game.ChangeState(3);
            return;
        }

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
