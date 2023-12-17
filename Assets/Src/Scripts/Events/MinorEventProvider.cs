using System;
using UnityEngine;

public class MinorEventProvider : MonoBehaviour {
    [SerializeField]
    private GameObject gameManagerObject;
    public EventProvider[] list;

    private void Start() {
        var manager = gameManagerObject.GetComponent<GameManager>();

        list = new EventProvider[] {
            new EventProvider(
                $"Pose� {manager.m_pCurrentParty.GetRandomMemberFullName()} wypowiada si� na temat nowej ustawy.",
                $"Lorem ipsum",
                null
            ),
            new EventProvider(
                $"Dzia�acz {manager.m_pCurrentParty.GetRandomRecruitFullName()} o�miesza si� na forum ekonomicznym.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(0, 67f) / 10000;
                }
            ),
            new EventProvider(
                $"Pose� {manager.m_pCurrentParty.GetRandomMemberFullName()} dokona� nies�usznej interwencji poselskiej.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(0, 125f) / 10000;
                }
            ),
            new EventProvider(
                $"Pose� {manager.m_pCurrentParty.GetRandomMemberFullName()} dokona� s�usznej i pomocnej interwencji poselskiej.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(50f, 133f) / 10000;
                }
            ),
            new EventProvider(
                $"Rzecznik wypowiada si� na temat partii w portalu internetowym.",
                $"Lorem ipsum",
                null
            ),
            new EventProvider(
                $"Pose� {manager.m_pCurrentParty.GetRandomMemberFullName()} �le wypad� w kr�tkim wywiadzie przeprowadzonym po wyj�ciu z sali plenarnej.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(0, 67f) / 10000;
                }
            ),
            new EventProvider(
                $"Pose� {manager.m_pCurrentParty.GetRandomMemberFullName()} �wietnie wypad� w trakcie kr�tkiego reporta�u po zako�czeniu obrad.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(25f, 100f) / 10000;
                }
            ),
            new EventProvider(
                $"Pose� {manager.m_pCurrentParty.GetRandomMemberFullName()} zapostowa� w Internecie zdj�cie z wakacji.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(0, 50f) / 10000;
                }
            ),
        };
    }

    public EventProvider ProvideRandomEvent() {
        var toProvide = list[UnityEngine.Random.Range(0, list.Length)];

        if (!(toProvide.action is null)) toProvide.action();

        return toProvide;
    }
}
