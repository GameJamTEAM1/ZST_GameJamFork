using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CPartyMember
{
    public enum EPastOccupation
    {
        None = -1,
        Doctor,
        MilVeteran,
        Teacher,
        Engineer,
        ZUS,
        Lawyer,
    }

    public static string[] PastOccupationStrings = new[]
    {
        "Lekarz", "Weteran Wojskowy", "Nauczyciel", "Inżynier", "Audytor ZUS", "Prawnik"
    };

    public enum EPersonality
    {
        None = -1,
        EasilyAgitated,
        Controversial,
        GreatSpeaker,
        Stutterer,
        NonWise,
        Calm,
        Sleepy,
        HardWorking,
        Lazy,
    }

    public static string[] PersonalityStrings = new[]
    {
        "Szybko się denerwuje: Szansa na blamaż podczas wywiadu zwiększa się.",
        "Kontrowersyjny: Jego negatywne działania mają **duży** efekt na sondaże.",
        "Świetny mówca: Wszelkie działania związane z wygłaszaniem mają automatycznie lepszy pozytywny efekt.",
        "Jąkajła: Wszelkie działania związane z wygłaszaniem maja automatycznie gorszy negatywny efekt.",
        "Lekkomyślny: Szansa na odwalenie czegoś dziwnego w czasie interwencji poselskiej jest większa.",
        "Stoik: Każdy wywiad i interwencja ma mały wpływ na sondaż, ale szansa na negatywne efekty jest niższa.",
        "Śpioch: Szansa na niepojawienie się na dane działanie jest większa.",
        "Pracowity: Szansa na pozytywne efekty raportu komisjii sejmowych jest większa.",
        "Leniwy: Szansa na negatywne efekty raportu komisjii sejmowych jest większa."
    };
    
    private string m_szFirstName, m_szLastName, m_szDescription;
    private Sprite m_pPortrait;
    private uint PP_Cost;
    private EPersonality Personality;
    private EPastOccupation PastOccupation;
    
    public CPartyMember(string szFirstName, string szLastName)
    {
        m_szFirstName = szFirstName;
        m_szLastName = szLastName;
        m_pPortrait = null;
    }
    
    public CPartyMember(string szFirstName, string szLastName, Sprite pPortrait)
    {
        m_szFirstName = szFirstName;
        m_szLastName = szLastName;
        m_pPortrait = pPortrait;
    }

    public CPartyMember(string szFirstName, string szLastName, string szDescription, Sprite pPortrait)
    {
        m_szFirstName = szFirstName;
        m_szLastName = szLastName;
        m_szDescription = szDescription;
        m_pPortrait = pPortrait;
        Personality = (EPersonality)Random.Range(0, ((int)EPersonality.Lazy)+1);
        PastOccupation = (EPastOccupation)Random.Range(0, ((int)EPastOccupation.Lawyer)+1);
    }

    public Sprite Portrait()
    {
        return m_pPortrait;
    }

    public EPersonality GetPersonality()
    {
        return Personality;
    }

    public EPastOccupation GetPastOccupation()
    {
        return PastOccupation;
    }

    public string FirstName()
    {
        return m_szFirstName;
    }

    public string LastName()
    {
        return m_szLastName;
    }

    public string Description()
    {
        return m_szDescription;
    }

    public bool Equals(string szFirstName, string szLastName)
    {
        return m_szFirstName.Equals(szFirstName) && m_szLastName.Equals(szLastName);
    }

    public override string ToString()
    {
        return $"name: {m_szFirstName} {m_szLastName}, personality: {PersonalityStrings[(int)Personality]}, pastOcc: {PastOccupationStrings[(int)PastOccupation]}";
    }
}

public class CParty
{
    public enum PartyName {
        KO,
        PIS,
        PSL,
        PL2050,
        KONFEDERACJA,
        LEWICA
    }
    
    public enum PartyType {
        LEFT,
        CENTER,
        RIGHT
    }
    
    private PartyName m_eName;
    private PartyType m_eType;
    private uint partyPrivilges;
    private byte polePartySupport;
    private byte realPartySupport;
    private List<CPartyMember> m_pMembers;

    private List<CPartyMember> m_pRecruitList;

    public CParty(PartyName eName, PartyType eType)
    {
        m_pMembers = new List<CPartyMember>();
        m_pRecruitList = new List<CPartyMember>();
        m_eName = eName;
        m_eType = eType;
        PP = 100;
        PPS = 100;
        RPS = 100;
    }
    
    public uint PP {
        set {
            this.partyPrivilges = value;
        }
        get {
            return partyPrivilges;
        }
    }

    public byte PPS {
        set {
            this.polePartySupport = value;
        }
        get {
            return this.polePartySupport;
        }
    }

    public byte RPS {
        set {
            this.realPartySupport = value;
        }
        get {
            return this.realPartySupport;
        }
    }

    public string Name()
    {
        return m_eName.ToString();
    }

    public List<CPartyMember> Members()
    {
        return m_pMembers;
    }
    
    public List<CPartyMember> Recruits()
    {
        return m_pRecruitList;
    }

    public bool HasMember(string szFirstName, string szLastName)
    {
        return m_pMembers.Contains(GetMember(szFirstName, szLastName));
    }
    
    public bool HasMember(CPartyMember pMember)
    {
        return m_pMembers.Contains(pMember);
    }
    
    public bool HasRecruit(string szFirstName, string szLastName)
    {
        return m_pRecruitList.Contains(GetRecruit(szFirstName, szLastName));
    }
    
    public bool HasRecruit(CPartyMember pRecruit)
    {
        return m_pRecruitList.Contains(pRecruit);
    }
    
    public CPartyMember GetMember(string szFirstName, string szLastName)
    {
        foreach (CPartyMember pMember in m_pMembers) if (pMember.Equals(szFirstName, szLastName)) return pMember;
        return null;
    }

    public bool AddMember(CPartyMember pMember)
    {
        if (HasMember(pMember)) return false;
        Debug.Log($"Adding {pMember.FirstName()} {pMember.LastName()} to {Name()}");
        m_pMembers.Add(pMember);
        return true;
    }

    public bool AddRecruit(CPartyMember pMember)
    {
        if (HasRecruit(pMember)) return false;
        Debug.Log($"Recruiting {pMember.FirstName()} {pMember.LastName()} for {Name()}");
        Debug.Log($"Recruit: {pMember}");
        m_pRecruitList.Add(pMember);
        return true;
    }

    public CPartyMember GetRecruit(string szFirstName, string szLastName)
    {
        foreach (CPartyMember pRecruit in m_pRecruitList) if (pRecruit.Equals(szFirstName, szLastName)) return pRecruit;
        return null;
    }
    
    public void RemoveRecruit(string szFirstName, string szLastName)
    {
        RemoveRecruit(GetRecruit(szFirstName, szLastName));
    }
    
    public void RemoveRecruit(CPartyMember pRecruit)
    {
        m_pRecruitList.Remove(pRecruit);
    }
    
    public void RemoveMember(string szFirstName, string szLastName)
    {
        RemoveMember(GetMember(szFirstName, szLastName));
    }

    public void RemoveMember(CPartyMember pMember)
    {
        m_pMembers.Remove(pMember);
    }
}