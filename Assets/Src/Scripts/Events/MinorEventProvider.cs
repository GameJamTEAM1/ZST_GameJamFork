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
                $"Pose³ {manager.m_pCurrentParty.GetRandomMemberFullName()} wypowiada siê na temat nowej ustawy.",
                $"Lorem ipsum",
                null
            ),
            new EventProvider(
                $"Dzia³acz {manager.m_pCurrentParty.GetRandomRecruitFullName()} oœmiesza siê na forum ekonomicznym.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(0, 67f) / 10000;
                }
            ),
            new EventProvider(
                $"Pose³ {manager.m_pCurrentParty.GetRandomMemberFullName()} dokona³ nies³usznej interwencji poselskiej.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(0, 125f) / 10000;
                }
            ),
            new EventProvider(
                $"Pose³ {manager.m_pCurrentParty.GetRandomMemberFullName()} dokona³ s³usznej i pomocnej interwencji poselskiej.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(50f, 133f) / 10000;
                }
            ),
            new EventProvider(
                $"Rzecznik wypowiada siê na temat partii w portalu internetowym.",
                $"Lorem ipsum",
                null
            ),
            new EventProvider(
                $"Pose³ {manager.m_pCurrentParty.GetRandomMemberFullName()} Ÿle wypad³ w krótkim wywiadzie przeprowadzonym po wyjœciu z sali plenarnej.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(0, 67f) / 10000;
                }
            ),
            new EventProvider(
                $"Pose³ {manager.m_pCurrentParty.GetRandomMemberFullName()} œwietnie wypad³ w trakcie krótkiego reporta¿u po zakoñczeniu obrad.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(25f, 100f) / 10000;
                }
            ),
            new EventProvider(
                $"Pose³ {manager.m_pCurrentParty.GetRandomMemberFullName()} zapostowa³ w Internecie zdjêcie z wakacji.",
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
