using UnityEngine;

public abstract class GameState : ScriptableObject
{
    protected GameManager m_game;

    protected PlayerInput m_input;
    protected Player m_player;

    protected HUDManager m_hud;
    protected SFXManager m_sfxManager;


    public virtual void Initialize(GameManager game, Player player, PlayerInput input)
    {
        m_game = game;
        m_player = player;
        m_input = input;
        m_sfxManager = game.SFXManager;
        m_hud = game.HUDManager;
    }

    public abstract void Enter();
    public abstract void Tick();
    public abstract void Exit();
}