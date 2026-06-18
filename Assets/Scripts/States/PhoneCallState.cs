using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(fileName = "Call State", menuName = "Scriptables/Game States/Call", order = 0)]
public class PhoneCallState : GameState
{
    [SerializeField] private float _placeDelay = .5f;
    [Space]
    [SerializeField] private float _mhmmSatisfactionBoost = .02f;

    CallManager m_callManager;
    ClientManager m_clientManager;
    ChoiceManager m_choiceManager;

    FatigueManager m_fatigueManager;

    private float m_placeDelayTime;
    private bool m_placingPhone;

    private bool FellAsleep { get; set; }

    public override void Initialize(GameManager game, Player player, PlayerInput input)
    {
        base.Initialize(game, player, input);
        m_callManager = game.CallManager;
        m_clientManager = game.ClientManager;
        m_choiceManager = game.ChoiceManager;

        m_fatigueManager = player.GetComponentInChildren<FatigueManager>();
        m_fatigueManager.OnSleep += OnSleep;
        FellAsleep = false;

        m_callManager.OnSpeechFinished += OnSpeechFinished;
    }

    public override void Enter()
    {
        var client = m_clientManager.PullClient();

        m_callManager.PlayCall(client);
        m_choiceManager.Prepare(client);

        m_placingPhone = false;
        m_placeDelayTime = _placeDelay;
    }

    public override void Tick()
    {
        if (!m_placingPhone)
        {
            if (m_callManager.CallOver)
            {
                OnCallOver();
            }
            else
            {
                DuringPhoneCall();
            }
        }
        else PlaceDelay();
    }

    public override void Exit()
    {
        m_callManager.StopCall();
    }

    private void DuringPhoneCall()
    {
        if (FellAsleep)
        {
            m_callManager.PlaceSFX();
            m_hud.OnChoiceMade();
            m_game.ChangeState(3);
            return;
        }

        if (m_callManager.InDecision)
        {
            var path = m_choiceManager.ListenToChoice(m_input);

            if (path.choice >= 0)
            {
                m_hud.OnChoiceMade();
                if (path.approved)
                    m_callManager.OnSatisfied(.5f);
                m_callManager.ProceedCall();
            }
        }
        else
        {
            m_callManager.Tick();

            if (m_input.MouseRightDown)
            {
                if (m_player.Nod())
                    m_callManager.OnSatisfied(_mhmmSatisfactionBoost);
            }
        }
    }
    private void OnSpeechFinished(int progress)
    {
        string[] choices = null;
        if (progress == 0)
        {
            choices = m_choiceManager.PresentFirstChoice();
        }
        else if (progress == 1)
        {
            choices = m_choiceManager.PresentSecondChoice();
        }
        m_hud.OnSpeechFinished(progress, choices);
    }

    private void OnSleep()
    {
        FellAsleep = true;
    }

    private void OnCallOver()
    {
        m_clientManager.ConfirmClientIsSatisfied(m_callManager.ClientSatisfaction);

        m_placingPhone = true;
        m_callManager.PlaceSFX();
    }

    private void PlaceDelay()
    {
        if (m_placeDelayTime <= 0)
        {
            if (m_clientManager.NoMoreClients)
                m_game.ChangeState(2);
            else
                m_game.ChangeState(4);
        }
        else
            m_placeDelayTime -= Time.deltaTime;
    }
}
