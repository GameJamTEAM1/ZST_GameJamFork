using System;
using UnityEngine;

public class MajorEventProvider : MonoBehaviour {
    [SerializeField]
    private GameObject gameManagerObject;
    public EventProvider[] list;

    private void Start() {
        var manager = gameManagerObject.GetComponent<GameManager>();

        list = new EventProvider[] {
            new EventProvider(
                $"Pose³ {manager.m_pCurrentParty.GetRandomMemberFullName()} wda³ siê w bójkê z pos³em opozycyjnej partii.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(67f, 250f) / 10000;
                }
            ),
            new EventProvider(
                $"Pose³ {manager.m_pCurrentParty.GetRandomMemberFullName()} wda³ siê w szarpaninê z policj¹.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(50f, 200f) / 10000;
                }
            ),
            new EventProvider(
                $"Pose³ {manager.m_pCurrentParty.GetRandomMemberFullName()} katastrofalnie poradzi³ sobie w ostatniej debacie telewizyjnej.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(50f, 200f) / 10000;
                }
            ),
            new EventProvider(
                $"Pose³ {manager.m_pCurrentParty.GetRandomMemberFullName()} skandalicznie siê zachowa³ w czasie lokalnego wydarzenia.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(75f, 125f) / 10000;
                }
            ),
            new EventProvider(
                $"Jeden z naszych pos³ów, {manager.m_pCurrentParty.GetRandomMemberFullName()}, po kontrowersyjnej wypowiedzi zosta³ wyprowadzony przez Stra¿ Marsza³kowsk¹.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(67f, 150f) / 10000;
                }
            ),
            new EventProvider(
                $"Wp³ywowy portal internetowy publikuje niepochlebne informacje o naszej partii.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(50f, 150f) / 10000;
                }
            ),
            new EventProvider(
                $"Komisja sejmowa publikuje niezadawalaj¹cy raport z prac naszych pos³ów.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(0, 100f) / 10000;
                }
            ),
            new EventProvider(
                $"Reforma proponowana przez nasz¹ partiê okaza³a siê umiarkowanym sukcesem.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(50f, 250f) / 10000;
                }
            ),
            new EventProvider(
                $"Komisja sejmowa publikuje dobrze przyjêty raport z ciê¿kiej pracy naszych pos³ów.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(50f, 150f) / 10000;
                }
            ),
            new EventProvider(
                $"Popularny influencer oficjalnie wspiera lokalnego dzia³acza {manager.m_pCurrentParty.GetRandomRecruitFullName()}",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(67f, 133f) / 10000;
                }
            ),
            new EventProvider(
                $"Wp³ywowy portal internetowy promuje niedawny projekt ustawy naszej partii.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(67f, 133f) / 10000;
                }
            ),
            new EventProvider(
                $"Niedawny wyrok s¹du ostatecznie oczysza naszego pos³a, {manager.m_pCurrentParty.GetRandomMemberFullName()}.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(33f, 100f) / 10000;
                }
            ),
            new EventProvider(
                $"Bohaterska interwencja pos³a {manager.m_pCurrentParty.GetRandomMemberFullName()}, który uratowa³ kota s¹siada z drzewa.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(50f, 150f) / 10000;
                }
            ),
        };
    }

    public EventProvider ProvideRandomEvent() {
        return list[UnityEngine.Random.Range(0, list.Length)];
    }
}
