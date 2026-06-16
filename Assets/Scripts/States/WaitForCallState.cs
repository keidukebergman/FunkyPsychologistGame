using UnityEngine;

[CreateAssetMenu(fileName = "Wait State", menuName = "Scriptables/Game States/Wait", order = 0)]
public class WaitForCallState : GameState
{
    [SerializeField] private float _waitForCallDuration = 5f;
    [SerializeField] private float _pickupDelay = .5f;

    private float _waitTime;
    private float _pickupDelayTime;
    private bool _pickedUpPhone;

    public override void Enter()
    {
        _waitTime = _waitForCallDuration;
        _pickupDelayTime = _pickupDelay;

        _pickedUpPhone = false;
    }

    public override void Tick()
    {

        if (_pickedUpPhone)
            PickupDelay();
        else
        {

            if (_waitTime <= 0)
            {
                if (m_player.MouseLeftDown)
                {
                    m_game.CallManager.PickUpSFX();
                    _pickedUpPhone = true;
                }
            }
            else
            {
                _waitTime -= Time.deltaTime;
                if (_waitTime <= 0)
                {
                    m_game.CallManager.PlayRinging();
                }
            }
        }
    }

    public override void Exit()
    {

    }

    private void PickupDelay()
    {
        if (_pickupDelayTime <= 0)
            m_game.ChangeState(1);
        else
            _pickupDelayTime -= Time.deltaTime;
    }
}
