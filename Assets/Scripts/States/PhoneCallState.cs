using UnityEngine;

[CreateAssetMenu(fileName = "Call State", menuName = "Scriptables/Game States/Call", order = 0)]
public class PhoneCallState : GameState
{
    [SerializeField] private float _placeDelay = .5f;
    
    CallManager m_callManager;
    ClientManager m_clientManager;

    private float m_placeDelayTime;
    private bool m_placingPhone;

    public override void Initialize(GameManager game, PlayerInput player)
    {
        base.Initialize(game, player);
        m_callManager = game.CallManager;
        m_clientManager = game.ClientManager;
    }

    public override void Enter()
    {
        var client = m_clientManager.PullClient();

        m_callManager.PlayCall(client);

        m_placingPhone = false;
        m_placeDelayTime = _placeDelay;
    }

    public override void Tick()
    {
        if (!m_placingPhone)
        {
            if (m_callManager.CallOver)
            {
                m_placingPhone = true;
                m_callManager.PlaceSFX();
            }
        }
        else
        {
            if (m_placeDelayTime <= 0)
            {
                m_game.ChangeState(0);
            }
            else
                m_placeDelayTime -= Time.deltaTime;
        }
    }

    public override void Exit()
    {

    }
}
