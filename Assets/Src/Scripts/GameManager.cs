using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    
    public GameDate gameDate = new GameDate();
    
    #region Party Management
    [SerializeField] private Sprite[] m_pPortraits;
    
    /// <summary>
    /// Current player's Political Party
    /// </summary>
    public CParty m_pCurrentParty;
    
    public void CreateParty(CParty.PartyName name, CParty.PartyType type)
    {
        m_pCurrentParty = new CParty(name, type);
    }
    
    public Sprite RandomPortrait()
    {
        return GameManager.Instance.Portraits()[Random.Range(0, GameManager.Instance.Portraits().Length)];
    }
    #endregion

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        Instance = this;
        DontDestroyOnLoad(this);
    }
    
    private void Start() {
        CreateParty(CParty.PartyName.KONFEDERACJA, CParty.PartyType.RIGHT);

        var ev = new DateEvent(new GameDate(2024, 3, 1));

        Dispatcher.DateChanged += ev.Dispatch;
        Dispatcher.DateChanged += (s, e) => Debug.Log($"Hello World from T:{gameDate.Week}/M:{gameDate.Month}/R:{gameDate.Year}!");
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            this.NextTurn();
        }
    }

    public void NextTurn() {
        gameDate.AddWeek();
        
        Dispatcher.DispatchDateChanged(this, new CustomArgs.DateChangedArgs(this.gameDate));
    }
    
    public Sprite[] Portraits()
    {
        return m_pPortraits;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Hub");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
