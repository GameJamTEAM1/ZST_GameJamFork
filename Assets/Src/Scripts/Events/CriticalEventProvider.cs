using System;
using UnityEngine;

public class CriticalEventProvider : MonoBehaviour {
    [SerializeField]
    private GameObject gameManagerObject;
    public EventProvider[] list;

    private void Start() {
        var manager = gameManagerObject.GetComponent<GameManager>();

        list = new EventProvider[] {
            new EventProvider(
                $"Szalony akt pos³a {manager.m_pCurrentParty.GetRandomMemberFullName()} - podpali³ agitacyjn¹ choinkê bo¿onarodzeniow¹ w Sejmie.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(75f, 325f) / 10000;
                }
            ),
            new EventProvider(
                $"Antysyjonistczny pose³ {manager.m_pCurrentParty.GetRandomMemberFullName()} zgasi³ Chanukê w Sejmie i zaatakowa³ postronn¹ ¯ydówkê.",
                $"Lorem ipsum",
                () => {
                    int rng = UnityEngine.Random.Range(0, 2);

                    if (rng == 0) manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(100f, 325f) / 10000;
                    else manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(67f, 250f) / 10000;
                }
            ),
            new EventProvider(
                $"Pose³ {manager.m_pCurrentParty.GetRandomMemberFullName()} jest skorumpowany i przyjmuje ³apówki. Najnowsze ustalenia ze œledztwa dziennikarskiego.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(67f, 325f) / 10000;
                }
            ),
            new EventProvider(
                $"Dzia³acz {manager.m_pCurrentParty.GetRandomRecruitFullName()} grozi³ zamachem bombowym w Kancelarii Sejmu.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(50f, 200f) / 10000;
                }
            ),
            new EventProvider(
                $"Wyciek z bazy danych odkry³ nieœcis³oœci w naszym bud¿ecie i mo¿liw¹ próbê prania brudnych pieniêdzy.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(67f, 350f) / 10000;
                }
            ),
            new EventProvider(
                $"Nasz lokalne struktury w mniejszym mieœcie domagaj¹ siê wiêkszego finansowania.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PP -= (int)Math.Round((decimal)manager.m_pCurrentParty.PP / 5);
                }
            ),
            new EventProvider(
                $"Wp³ywowy celebryta miesza nasz¹ partiê z b³otem.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(33f, 150f) / 10000;
                }
            ),
            new EventProvider(
                $"Lokalna wp³ywowa organizacja daje nam swoje wsparcie.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(100F, 200f) / 10000;
                    manager.m_pCurrentParty.PP += (manager.m_pCurrentParty.PP / 4);
                }
            ),
            new EventProvider(
                $"Portal internetowy publikuje szokuj¹ce fakty na temat naszej opozycji, zyskujemy na tym g³osy.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(150f, 300f) / 10000;
                }
            ),
            new EventProvider(
                $"Jeden z postulatów naszej partii staje siê viralem w Internecie, ludzie nas kochaj¹.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(67f, 300f) / 10000;
                }
            ),
            new EventProvider(
                $"Niedawny spot wyborczy zdoby³ niewyobra¿aln¹ popularnoœæ.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(67f, 250f) / 10000;
                }
            ),
            new EventProvider(
                $"Nasz dzia³acz {manager.m_pCurrentParty.GetRandomRecruitFullName()} wygrywa wp³ywow¹ i szanowan¹ nagrodê w swoim fachu.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(50f, 200f) / 10000;
                }
            ),
            new EventProvider(
                $"Pose³ {manager.m_pCurrentParty.GetRandomRecruitFullName()} ca³kowicie zniszczy³ konkurencjê we wczorajszej debacie wieczornej.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(67f, 350f) / 10000;
                }
            ),
            new EventProvider(
                $"Nasza partia popar³a najnowsz¹ ustawê. Efekty spodziewane s¹ za kilka tygodni.",
                $"Lorem ipsum",
                () => {
                    GameDate date = new GameDate(manager.gameDate.Year, manager.gameDate.Month, manager.gameDate.Week);

                    int a = UnityEngine.Random.Range(0, 4);
                    int b = UnityEngine.Random.Range(2, 5);

                    for (int i = 0; i < a; i++) {
                        date.AddWeek();
                    }

                    for (int i = 0; i < b; i++) {
                        date.AddMonth();
                    }

                    Dispatcher.DateChanged += (s, e) => {
                        if (e.gameDate == date) {
                            int rng = UnityEngine.Random.Range(0, 2);

                            if (rng == 0) {
                                Debug.Log("Pora¿ka! Jakiœ czas temu poparliœmy ustawê, która teraz znacz¹co pogorszy³a jakoœ ¿ycia obywateli.");
                                manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(225f, 600f) / 10000;
                            }
                            else {
                                Debug.Log("Suckes! Jakiœ czas temu poparliœmy ustawê, która teraz okaza³a siê byæ strza³em w dziesi¹tkê.");
                                manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(225f, 600f) / 10000;
                            }
                        }
                    };
                }
            )
        };
    }

    public EventProvider ProvideRandomEvent() {
        return list[UnityEngine.Random.Range(0, list.Length)];
    }
}
