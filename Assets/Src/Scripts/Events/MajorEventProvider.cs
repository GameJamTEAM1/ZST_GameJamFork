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
                $"Pose� {manager.m_pCurrentParty.GetRandomMemberFullName()} wda� si� w b�jk� z pos�em opozycyjnej partii.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(67f, 250f) / 10000;
                }
            ),
            new EventProvider(
                $"Pose� {manager.m_pCurrentParty.GetRandomMemberFullName()} wda� si� w szarpanin� z policj�.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(50f, 200f) / 10000;
                }
            ),
            new EventProvider(
                $"Pose� {manager.m_pCurrentParty.GetRandomMemberFullName()} katastrofalnie poradzi� sobie w ostatniej debacie telewizyjnej.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(50f, 200f) / 10000;
                }
            ),
            new EventProvider(
                $"Pose� {manager.m_pCurrentParty.GetRandomMemberFullName()} skandalicznie si� zachowa� w czasie lokalnego wydarzenia.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(75f, 125f) / 10000;
                }
            ),
            new EventProvider(
                $"Jeden z naszych pos��w, {manager.m_pCurrentParty.GetRandomMemberFullName()}, po kontrowersyjnej wypowiedzi zosta� wyprowadzony przez Stra� Marsza�kowsk�.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(67f, 150f) / 10000;
                }
            ),
            new EventProvider(
                $"Wp�ywowy portal internetowy publikuje niepochlebne informacje o naszej partii.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(50f, 150f) / 10000;
                }
            ),
            new EventProvider(
                $"Komisja sejmowa publikuje niezadawalaj�cy raport z prac naszych pos��w.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(0, 100f) / 10000;
                }
            ),
            new EventProvider(
                $"Reforma proponowana przez nasz� parti� okaza�a si� umiarkowanym sukcesem.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(50f, 250f) / 10000;
                }
            ),
            new EventProvider(
                $"Komisja sejmowa publikuje dobrze przyj�ty raport z ci�kiej pracy naszych pos��w.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(50f, 150f) / 10000;
                }
            ),
            new EventProvider(
                $"Popularny influencer oficjalnie wspiera lokalnego dzia�acza {manager.m_pCurrentParty.GetRandomRecruitFullName()}",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(67f, 133f) / 10000;
                }
            ),
            new EventProvider(
                $"Wp�ywowy portal internetowy promuje niedawny projekt ustawy naszej partii.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(67f, 133f) / 10000;
                }
            ),
            new EventProvider(
                $"Niedawny wyrok s�du ostatecznie oczysza naszego pos�a, {manager.m_pCurrentParty.GetRandomMemberFullName()}.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(33f, 100f) / 10000;
                }
            ),
            new EventProvider(
                $"Bohaterska interwencja pos�a {manager.m_pCurrentParty.GetRandomMemberFullName()}, kt�ry uratowa� kota s�siada z drzewa.",
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
