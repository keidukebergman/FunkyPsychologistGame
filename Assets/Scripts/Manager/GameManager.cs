using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int m_startingGameState = 0;
    [Space]
    [SerializeField] private GameState[] m_gameStates;
    private GameState m_gameState;
    private int m_gameStateID;

    private Player m_player;
    private PlayerInput m_input;
    private CallManager m_callManager;
    private ClientManager m_clientManager;
    private ChoiceManager m_choiceManager;
    private SFXManager m_sfxManager;
    private HUDManager m_HUDManager;
    private UnlockableManager m_unlockableManager;
    private FatigueManager m_fatigueManager;

    private ClickableTelephone m_telephone;
    private ProgramSwitchScript m_switchScript;

    public CallManager CallManager => m_callManager;
    public ClientManager ClientManager => m_clientManager;
    public ChoiceManager ChoiceManager => m_choiceManager;
    public SFXManager SFXManager => m_sfxManager;
    public HUDManager HUDManager => m_HUDManager;
    public UnlockableManager UnlockableManager => m_unlockableManager;
    public FatigueManager FatigueManager => m_fatigueManager;

    public ProgramSwitchScript TVSwitch => m_switchScript;
    public ClickableTelephone Telephone => m_telephone;


    void Awake()
    {
        m_input = new PlayerInput();


        m_player = FindAnyObjectByType<Player>();
        m_callManager = FindAnyObjectByType<CallManager>();
        m_clientManager = FindAnyObjectByType<ClientManager>();
        m_choiceManager = FindAnyObjectByType<ChoiceManager>();
        m_sfxManager = FindAnyObjectByType<SFXManager>();
        m_HUDManager = FindAnyObjectByType<HUDManager>();
        m_unlockableManager = FindAnyObjectByType<UnlockableManager>();
        m_fatigueManager = FindAnyObjectByType<FatigueManager>();

        m_switchScript = FindAnyObjectByType<ProgramSwitchScript>();
        m_telephone = FindAnyObjectByType<ClickableTelephone>();

    }

    private void Start()
    {
        foreach (var state in m_gameStates)
            state.Initialize(this, m_player, m_input);
        OnStartGame();
    }

    private void Update()
    {
        m_gameState.Tick();
        m_input.Tick();

        if (Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            QuitGame();
        }
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

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
