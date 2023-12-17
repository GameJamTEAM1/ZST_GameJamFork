using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPartyMember
{
    private string m_szFirstName, m_szLastName;
    private Sprite m_pPortrait;
    
    public CPartyMember(string szFirstName, string szLastName)
    {
        m_szFirstName = szFirstName;
        m_szLastName = szLastName;
        m_pPortrait = GameManager.Instance.RandomPortrait();
    }

    public Sprite Portrait()
    {
        return m_pPortrait;
    }

    public string FirstName()
    {
        return m_szFirstName;
    }

    public string LastName()
    {
        return m_szLastName;
    }

    public bool Equals(string szFirstName, string szLastName)
    {
        return m_szFirstName.Equals(szFirstName) && m_szLastName.Equals(szLastName);
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
    private int partyPrivilges;
    private float polePartySupport;
    private float realPartySupport;
    private List<CPartyMember> m_pMembers;
    private List<CPartyMember> m_pRecruitList;
    // TODO: Add party's statistics ~GabrielV

    public CParty(PartyName eName, PartyType eType)
    {
        m_pMembers = new List<CPartyMember>();
        m_pRecruitList = new List<CPartyMember>();
        m_eName = eName;
        m_eType = eType;
        PP = 100;
        PPS = 0.1f;
        RPS = 0.1f;

        m_pMembers.Add(new CPartyMember("test", "testowaty"));
        m_pRecruitList.Add(new CPartyMember("test2", "testowaty2"));
    }
    
    public int PP {
        set {
            this.partyPrivilges = value;
        }
        get {
            return partyPrivilges;
        }
    }

    public float PPS {
        set {
            if (value > 1f) this.polePartySupport = 1;
            if (value < 0f) this.polePartySupport = 0;
            
            this.polePartySupport = value;
        }
        get {
            return this.polePartySupport;
        }
    }

    public float RPS {
        set {
            if (value > 1f) this.realPartySupport = 1;
            if (value < 0f) this.realPartySupport = 0;

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
    
    public bool HasRecruit(string szFirstName, string szLastName)
    {
        return m_pRecruitList.Contains(GetRecruit(szFirstName, szLastName));
    }
    
    public CPartyMember GetMember(string szFirstName, string szLastName)
    {
        foreach (CPartyMember pMember in m_pMembers) if (pMember.Equals(szFirstName, szLastName)) return pMember;
        return null;
    }

    public void AddMember(CPartyMember pMember)
    {
        m_pMembers.Add(pMember);
    }

    public void AddRecruit(CPartyMember pMember)
    {
        m_pRecruitList.Add(pMember);
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

    public CPartyMember GetRandomMember() {
        if (this.m_pMembers.Count == 0) return null;

        var index = Random.Range(0, this.m_pMembers.Count);

        return this.m_pMembers[index];
    }

    public CPartyMember GetRandomRecruit() {
        if (this.m_pRecruitList.Count == 0) return null;

        var index = Random.Range(0, this.m_pRecruitList.Count);

        return this.m_pRecruitList[Random.Range(0, this.m_pRecruitList.Count)];
    }

    public string GetRandomMemberFullName() {
        var member = GetRandomMember();

        return $"{member.FirstName()} {member.LastName()}";
    }

    public string GetRandomRecruitFullName() {
        var member = GetRandomRecruit();

        return $"{member.FirstName()} {member.LastName()}";
    }
}