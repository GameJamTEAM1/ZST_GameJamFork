using System;
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
        m_pPortrait = null;
    }
    
    public CPartyMember(string szFirstName, string szLastName, Sprite pPortrait)
    {
        m_szFirstName = szFirstName;
        m_szLastName = szLastName;
        m_pPortrait = pPortrait;
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