using UnityEngine;

public abstract class GameState : ScriptableObject
{
    protected GameManager m_game;

    protected PlayerInput m_player;


    public virtual void Initialize(GameManager game, PlayerInput player)
    {
        m_game = game;
        m_player = player;
    }

    public abstract void Enter();
    public abstract void Tick();
    public abstract void Exit();
}