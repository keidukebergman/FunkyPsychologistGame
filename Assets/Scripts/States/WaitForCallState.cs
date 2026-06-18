using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Wait State", menuName = "Scriptables/Game States/Wait", order = 0)]
public class WaitForCallState : GameState
{
    [SerializeField] private float _waitForCallDuration = 5f;
    [SerializeField] private float _pickupDelay = .5f;

    private float _waitTime;
    private float _pickupDelayTime;
    private bool _pickedUpPhone;

    private ClickableTelephone _telephone;

    public override void Initialize(GameManager game, Player player, PlayerInput input)
    {
        base.Initialize(game, player, input);

        _telephone = game.Telephone;
    }

    public override void Enter()
    {
        _telephone.OnClick += OnTelephonePickup;

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
                if (_telephone.Held)
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
        _telephone.OnClick -= OnTelephonePickup;
        _telephone.Held = false;
    }

    private void OnTelephonePickup()
    {
        _telephone.Held = true;
    }

    private void PickupDelay()
    {
        if (_pickupDelayTime <= 0)
            m_game.ChangeState(1);
        else
            _pickupDelayTime -= Time.deltaTime;
    }
}
