using System;
using System.Collections.Generic;
using CustomArgs;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    
    public GameDate gameDate = new GameDate();
    public uint turns = 0;
    
    #region Party Management

    private string[] firstNames = new[]
    {
        "Jan",
        "Maciej",
        "Bartosz",
        "Magdalena",
        "Jakub",
        "Adrian",
        "Jolanta",
        "Grzegorz",
        "Małgorzata",
        "Adam",
        "Michał",
        "Artur",
        "Radomir",
        "Anita",
        "Marcelina",
        "Marcel",
        "Paweł",
        "Tomasz",
        "Maksymilian",
    };

    private string[] lastNames = new[]
    {
        "Sitek",
        "Mądry",
        "Gadziński",
        "Sosna",
        "Kukla",
        "Nowak",
        "Kowalski",
        "Bosak",
        "Braun",
        "Kaczyński",
        "Tusk",
        "Morawiecki",
        "Hołownia",
        "Rossa",
        "Korwin",
        "Tic",
    };
    private CPartyMember[] m_pRecruits;
    
    [Header("UI")]
    [SerializeField] private Sprite[] m_pPortraits;

    [SerializeField] private GameObject m_pRecruitPanel;
    
    /// <summary>
    /// Current player's Political Party
    /// </summary>
    private CParty m_pCurrentParty;
    
    public void CreateParty(CParty.PartyName name, CParty.PartyType type)
    {
        m_pCurrentParty = new CParty(name, type);
    }

    public void RecruitToParty(CPartyMember pRecruit)
    {
        m_pCurrentParty.AddMember(pRecruit);
    }
    
    public Sprite RandomPortrait()
    {
        return Instance.Portraits()[Random.Range(0, Instance.Portraits().Length)];
    }
    #endregion

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        Instance = this;
        DontDestroyOnLoad(this);
    }
    
    private void Start() {
        CreateParty(CParty.PartyName.LEWICA, CParty.PartyType.RIGHT);
        m_pRecruits = new CPartyMember[3];

        for (int i = 0; i < 3; i++)
        {
            string firstName = firstNames[Random.Range(0, firstNames.Length)], lastName = lastNames[Random.Range(0, lastNames.Length)];

            while (m_pCurrentParty.HasMember(firstName, lastName))
            {
                firstName = firstNames[Random.Range(0, firstNames.Length)];
                lastName = lastNames[Random.Range(0, lastNames.Length)];
            }

            m_pRecruits[i] = new CPartyMember(firstName, lastName);
        }
    }

    private void OnEnable()
    {
        Dispatcher.DateChanged += LogDate;
        Dispatcher.GuiStateChanged += OnGuiStateChange;
    }

    private void OnDisable()
    {
        Dispatcher.DateChanged -= LogDate;
        Dispatcher.GuiStateChanged -= OnGuiStateChange;
    }

    private void OnGuiStateChange(Object s, GUIArgs e)
    {
        e.gui.SetActive(e.type == GUIArgs.EGuiEvent.OPEN);
        if (e.gui.transform.parent.name.Equals("RecruitCanvas"))
        {
            Debug.Log("Opened Recruit Canvas");
            for (int i = 0; i < 3; i++)
            {
                CPartyMember pRecruit = m_pRecruits[i];
                string name = string.Concat("Person", (i + 1));
                Transform person = e.gui.transform.Find(name);
                if (person == null)
                {
                    Debug.LogError($"Could not find {name}");
                    return;
                }

                Transform image = person.Find(String.Concat(name, " Image")),
                    desc = person.Find(String.Concat(name, " Description")),
                    recruitBtnT = person.Find(String.Concat("Button", i + 1));
                Image portrait = image.GetComponent<Image>();
                portrait.sprite = pRecruit.Portrait();

                Text txt = desc.GetComponent<Text>();
                txt.text = string.Concat(pRecruit.FirstName(), " ", pRecruit.LastName());

                Button btn = recruitBtnT.GetComponent<Button>();
                btn.onClick.AddListener(delegate
                {
                    RecruitToParty(pRecruit);
                });
            }
        }
    }

    public void OpenRecruitPanel()
    {
        Dispatcher.DispatchGuiStateChanged(this, new GUIArgs(m_pRecruitPanel, GUIArgs.EGuiEvent.OPEN));
    }
    
    private void LogDate(Object s, DateChangedArgs e)
    {
        Debug.Log($"T:{{gameDate.Week}}/M:{{gameDate.Month}}/R:{{gameDate.Year}} T:{{this.turns}}!");
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            this.NextTurn();
        }
    }

    public void NextTurn() {
        gameDate.AddWeek();
        this.turns++;
        
        Dispatcher.DispatchDateChanged(this, new CustomArgs.DateChangedArgs(this.gameDate, this.turns));
    }
    
    public Sprite[] Portraits()
    {
        return m_pPortraits;
    }
}
