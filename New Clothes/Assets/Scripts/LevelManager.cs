using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Data")]
    public PauseInfo pauseInfo;
    public LevelState state = LevelState.PreLoad;
    public float timeElapsed = 0f;
    public Room playerLocation;
    public float playerFloorLevel = 1f;
    public CameraController cam;

    [Header("Level Settings")]
    public int clothesRequiredToWin;

    [Header("References")]
    public Transform playerTransform;
    public List<AIController> AIs = new List<AIController>();
    public List<Container> containers = new List<Container>();
    public List<Room> rooms = new List<Room>();

    [Header("Assets")]
    public AudioClip foundItemSound;

    public delegate void OnTrigger();
    public static event OnTrigger onInit;
    public static event OnTrigger onPostInit;
    public static event OnTrigger onPreGame;
    public static event OnTrigger onStartGame;

    public delegate void OnEndGame(bool isWin);
    public static event OnEndGame onEndGame;

    public delegate void OnPause(PauseInfo info);
    public static event OnPause onSetPause;

    public static LevelManager Instance;

    public Stealth playerStealth;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Initialize();
        StartPreGame();
        StartGame();
    }

    void Update()
    {
        if(InGameAndRunning())
        {
            timeElapsed += Time.deltaTime;
        }
    }

    void SetState(LevelState state)
    {
        this.state = state;
    }

    public void Initialize()
    {
        RaiseOnInit();
        SetState(LevelState.PostLoad);
        RaiseOnPostInit();
    }

    public void StartPreGame()
    {
        RaiseOnPreGame();
        SetState(LevelState.PreGame);
    }

    public void StartGame()
    {
        RaiseOnStartGame();
        SetState(LevelState.Game);
    }

    public void EndGame(bool isWin)
    {
        SetState(LevelState.PostGame);
        SetPause(true);

        if (isWin)
        {

        }

        RaiseOnEndGame(isWin);
    }

    public void RegisterCamera(CameraController cam)
    {
        this.cam = cam;
    }

    public void RegisterPlayerTransform(Transform player)
    {
        this.playerTransform = player;
        playerStealth = player.GetComponent<Stealth>();
    }

    public void RegisterAIController(AIController controller)
    {
        AIs.Add(controller);
    }

    public void RegisterContainer(Container container)
    {
        containers.Add(container);
    }

    public void RegisterRoom(Room room)
    {
        rooms.Add(room);
    }

    public void TogglePause()
    {

    }

    public void SetPause(PauseInfo info)
    {
        pauseInfo = info;
        RaiseOnSetPause(info);
    }

    public bool InGameAndRunning()
    {
        return !pauseInfo.isPaused && state == LevelState.Game;
    }

    public void SetPause(bool isPaused)
    {
        SetPause(new PauseInfo(isPaused));
    }

    public void SetInputPause(bool isPaused)
    {
        SetPause(new PauseInfo(false, true));
    }

    public void RaiseOnInit()
    {
        if (onInit != null)
        {
            onInit();
        }
    }

    public void RaiseOnSetPause(PauseInfo info)
    {
        if (onSetPause != null)
        {
            onSetPause(info);
        }
    }

    public void RaiseOnPostInit()
    {
        if (onPostInit != null)
        {
            onPostInit();
        }
    }

    public void RaiseOnPreGame()
    {
        if (onPreGame != null)
        {
            onPreGame();
        }
    }

    public void RaiseOnStartGame()
    {
        if (onStartGame != null)
        {
            onStartGame();
        }
    }

    public void RaiseOnEndGame(bool isWin)
    {
        if (onEndGame != null)
        {
            onEndGame(isWin);
        }
    }
}