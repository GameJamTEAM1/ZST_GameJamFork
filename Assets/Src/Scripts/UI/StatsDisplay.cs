using System;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour
{
    public Text points;
    public Text percentVotes;
    public Text numberOfMembers;

    // Start is called before the first frame update
    void Start()
    {
        Dispatcher.DateChanged += (s, e) =>
        {
            CParty party = GameManager.Instance.m_pCurrentParty;
            points.text = "Przywileje partyjne: " + party.PP;
            percentVotes.text = "Poparcie w sonda¿ach: " + party.PPS;
            if(party.Members() != null)
                numberOfMembers.text = "Miejsca w senacie: " + party.Members().Count;
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
