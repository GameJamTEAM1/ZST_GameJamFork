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

    public TurnManager TM { get; private set; }
    public GameDate gameDate = new GameDate();
    public uint turns = 0;
    
    #region Party Management

    private string[] MaleFirstNames = new[]
    {
        "Jan",
        "Maciej",
        "Bartosz",
        "Jakub",
        "Adrian",
        "Grzegorz",
        "Adam",
        "Michał",
        "Artur",
        "Radomir",
        "Marcel",
        "Paweł",
        "Tomasz",
        "Maksymilian",
        "Gabriel",
        "Konrad",
        "Jacek",
    };
    
    private string[] FemaleFirstNames = new[]
    {
        "Magdalena",
        "Jolanta",
        "Małgorzata",
        "Anita",
        "Marcelina",
        "Ludmiła",
        "Martalena",
        "Natalia",
        "Wiesława",
        "Cecylia",
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
        "Koszowski",
        "Kempa",
        "Bieniek",
        "Kaczmarczyk",
        "Bienio",
        "Marciniak",
        "Kaczmarski",
        "Murański",
        "Skowron",
        "Stowron",
        "Fiodorow",
        "Jabłonowski",
        "Jaworek",
    };

    private string[] characterDescriptions = new[]
    {
        "Pracowita i uparta jak nikt, chce osiągnąć swój zamierzony cel za wszelką cene, nawet jeśli znaczy to spacer po trupach.Po mimo krótkim stażu politycznym (10 lat), lecz niezwykle intensywnym, zaczyna objawiać sie u niej powoli wypalenie zawodowe, co nie zmienia jej zaciętości w osiąganiu celów.",
        "Leniwy, przez swój wiek i 25 lat na uczelni czasem zapomni jak się nazywa i gdzie się schodzi z mównicy, ale kiedy już na nią wstąpi to sam Piotr Skarga się chowa ze swoimi kazaniami. Genialny mówca, może nie zdobywa serc wyborców, ale jego oratorskie umiejętności przypominaja posłom gdzie ich miejsce.",
        "Cichy, niezbyt wygadany działacz mniejszości litewskiej nie porywa za sobą tłumów ale ma zapewniany żelazny elektorat mniejszości liteswkiej dla, której działał przez lata. Wszelkie wywiady to nie jego broszka, akcent utrudnia mu zdobycie popularności zarówno u posłów jak i wyborców, jednak polscy litwini zawsze będą go wspierać.",
        "Weterynarz z zawodu, myśliwy z zamiłowania. Lata spędzone w lesie i w gabinecie weterynarskim ukształtowały jego twardy charakter. Potężna postura i silny charakter powoduje że na kazdym wydarzeniu na którym się pojawi natychmiast dominuje swoich przeciwników. Z tak silną osobowością się nie debatuje.",
        "Młody, lekkomyślny i piekielnie ładny. Ten człowiek to idealny przykład jak samym wyglądem można zwojować tłumy. Jest dosy wyszczekany, a czasem wręcz bezczelny, jednak jego posągowe rysy w czasie wywiadu rekompensują wszystkie farmazony które mówi.",
        "Szanowany inżynier budowlany, prosty, swojski chłop; Czasem jednak podczsas wywiadu może mu wystać słoma z butów, jednak jest to jeden z lepszych specjalistów w swoim fachu.",
        "Młoda, drobna specjalistka od PR i wizerunku medialnego. Wie jak dobrze zaprezentować partie a co ważniejsze siebie. Niezbyt sobie radzi z papierkową robotą, ale jednym spojrzeniem, jednym słowem jest w stanie zjednać sobie kamery i światło, tak aby uzyskać jak najlepszy wynik.",
        "Szanowany neurochirurg, mówi żargonem i często jego rozmówcy nie za bardzo rozumieją przesłanie tego co mówi, ale to co go wyróżnia to absolutny i niezbywalny autorytet w środowisku lekarskim. Jego fachowy i specjalistyczny język utrudnia mu zjednanie sobie wyborców, jednak jego olbrzymia wiedza i autorytet powoduje że cieszy się on sporym poparciem wśród kolegów po fachu.",
        "Profesor prawa, prawdziwy buldog i policyjny bulterier. Pod jej okeim nie prześlizgnął się żaden cwaniak starający się oszukać na podatkach, po kryjomu wziąc łapówke czy też dostać nieco lepsze finansowanie od państwa. Niezrównana sobie specjalistka, określana przez osadzonych jako \"Audytorski Esesman\", praca w komisjach to jej główne środowisko, jednak jej popularna twarz kojarzona z profesjonalizmem i bezwzględnością może z jednej strony wielu wyborców przyciągnąć, zarówno jak i odepchnąć od partii.",
        "Głośny, kontrowersyjny i niewykształcony idiota, pomimo swoich często skandalicznych wypowiedzi, ludzie (szczególnie w internecie) kochają go za bycie bezpośrednim, jednak często ta bezpośredniość stoi w opozycji z powagą izby sejmowej. Lokalne działania, obecność na protestach to coś co woli znacznie bardziej niż głosowania i posiedzenia kolejnych komisjii.",
    };
    private List<CPartyMember> m_pRecruits;
    
    [Header("UI")]
    [SerializeField] private Sprite[] m_pMalePortraits;
    [SerializeField] private Sprite[] m_pFemalePortraits;

    [SerializeField] private GameObject m_pRecruitPanel;
    
    /// <summary>
    /// Current player's Political Party
    /// </summary>
    private CParty m_pCurrentParty;
    
    public void CreateParty(CParty.PartyName name, CParty.PartyType type)
    {
        m_pCurrentParty = new CParty(name, type);
    }

    public bool RecruitToParty(CPartyMember pRecruit)
    {
        return m_pCurrentParty.AddRecruit(pRecruit);
    }
    
    public Sprite RandomMalePortrait()
    {
        return m_pMalePortraits[Random.Range(0, m_pMalePortraits.Length)];
    }
    
    public Sprite RandomFemalePortrait()
    {
        return m_pFemalePortraits[Random.Range(0, m_pFemalePortraits.Length)];
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
        m_pRecruits = new List<CPartyMember>();

        for (int i = 0; i < 3; i++)
        {
            string firstName = "", lastName = lastNames[Random.Range(0, lastNames.Length)];
            Sprite pPortrait = RandomMalePortrait();
            if (Random.value < 0.5)
            {
                do
                {
                    firstName = MaleFirstNames[Random.Range(0, MaleFirstNames.Length)];
                    lastName = lastNames[Random.Range(0, lastNames.Length)];
                    pPortrait = RandomMalePortrait();
                } while (m_pRecruits.Contains(new CPartyMember(firstName, lastName, pPortrait)));

            }
            else
            {
                do
                {
                    firstName = FemaleFirstNames[Random.Range(0, FemaleFirstNames.Length)];
                    lastName = lastNames[Random.Range(0, lastNames.Length)];
                    pPortrait = RandomFemalePortrait();
                } while (m_pRecruits.Contains(new CPartyMember(firstName, lastName, pPortrait)));
            }

            m_pRecruits.Add(new CPartyMember(firstName, lastName, characterDescriptions[Random.Range(0, characterDescriptions.Length)], pPortrait));
        }

        TM = new TurnManager();
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
        if (e.gui.transform.parent.name.Equals("RecruitCanvas")) HandleCrucifixScreen(e.gui);
    }

    private void HandleCrucifixScreen(GameObject gui)
    {
        Debug.Log("Opened Recruit Canvas");
        for (int i = 0; i < 3; i++)
        {
            CPartyMember pRecruit = m_pRecruits[i];
            string name = string.Concat("Person", (i + 1));
            Transform person = gui.transform.Find(name);
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
            txt.text = string.Concat(pRecruit.FirstName(), " ", pRecruit.LastName(), "\n", pRecruit.Description());

            Button btn = recruitBtnT.GetComponent<Button>();
            btn.onClick.AddListener(delegate
            {
                if (RecruitToParty(pRecruit))
                {
                    btn.transform.Find("Text").GetComponent<Text>().text = "Recruited";
                }
            });
        }
    }

    public void OpenRecruitPanel()
    {
        Dispatcher.DispatchGuiStateChanged(this, new GUIArgs(m_pRecruitPanel, GUIArgs.EGuiEvent.OPEN));
    }
    
    private void LogDate(Object s, DateChangedArgs e)
    {
        Debug.Log($"T:{gameDate.Week}/M:{gameDate.Month}/R:{gameDate.Year} T:{this.turns}!");
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            TM.NextTurn();
        }
    }

    public void NextTurn() {
        gameDate.AddWeek();
        this.turns++;
        
        Dispatcher.DispatchDateChanged(this, new CustomArgs.DateChangedArgs(this.gameDate, this.turns));
    }
}
