using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState[] m_gameStates;
    private int m_startingGameState = 0;
    private GameState m_gameState;
    private int m_gameStateID;

    private Player m_player;
    private PlayerInput m_input;
    private CallManager m_callManager;
    private ClientManager m_clientManager;
    private ChoiceManager m_choiceManager;
    private SFXManager m_sfxManager;
    private HUDManager m_HUDManager;

    public CallManager CallManager => m_callManager;
    public ClientManager ClientManager => m_clientManager;
    public ChoiceManager ChoiceManager => m_choiceManager;
    public SFXManager SFXManager => m_sfxManager;
    public HUDManager HUDManager => m_HUDManager;
    

    void Awake()
    {
        m_input = new PlayerInput();


        m_player = FindAnyObjectByType<Player>();
        m_callManager = FindAnyObjectByType<CallManager>();
        m_clientManager = FindAnyObjectByType<ClientManager>();
        m_choiceManager = FindAnyObjectByType<ChoiceManager>();
        m_sfxManager = FindAnyObjectByType<SFXManager>();
        m_HUDManager = FindAnyObjectByType<HUDManager>();

        foreach (var state in m_gameStates)
            state.Initialize(this, m_player, m_input);
    }

    private void Start()
    {
        OnStartGame();
    }

    private void Update()
    {
        m_gameState.Tick();
        m_input.Tick();
    }

    public void OnStartGame()
    {
        //m_player.OnStart();

        m_gameStateID = m_startingGameState;
        m_gameState = m_gameStates[m_gameStateID];
        ChangeState(m_gameStateID);

        //HUD.Enable(true);
    }

    public void ChangeState(int i)
    {
        if (m_gameState != null)
            m_gameState.Exit();

        m_gameState = m_gameStates[i];

        m_gameState.Enter();


        Debug.Log("Entering " + m_gameState.name);
    }
}
