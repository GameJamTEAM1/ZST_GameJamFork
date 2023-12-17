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
                $"Szalony akt pos�a {manager.m_pCurrentParty.GetRandomMemberFullName()} - podpali� agitacyjn� choink� bo�onarodzeniow� w Sejmie.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(75f, 325f) / 10000;
                }
            ),
            new EventProvider(
                $"Antysyjonistczny pose� {manager.m_pCurrentParty.GetRandomMemberFullName()} zgasi� Chanuk� w Sejmie i zaatakowa� postronn� �yd�wk�.",
                $"Lorem ipsum",
                () => {
                    int rng = UnityEngine.Random.Range(0, 2);

                    if (rng == 0) manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(100f, 325f) / 10000;
                    else manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(67f, 250f) / 10000;
                }
            ),
            new EventProvider(
                $"Pose� {manager.m_pCurrentParty.GetRandomMemberFullName()} jest skorumpowany i przyjmuje �ap�wki. Najnowsze ustalenia ze �ledztwa dziennikarskiego.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(67f, 325f) / 10000;
                }
            ),
            new EventProvider(
                $"Dzia�acz {manager.m_pCurrentParty.GetRandomRecruitFullName()} grozi� zamachem bombowym w Kancelarii Sejmu.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(50f, 200f) / 10000;
                }
            ),
            new EventProvider(
                $"Wyciek z bazy danych odkry� nie�cis�o�ci w naszym bud�ecie i mo�liw� pr�b� prania brudnych pieni�dzy.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(67f, 350f) / 10000;
                }
            ),
            new EventProvider(
                $"Nasz lokalne struktury w mniejszym mie�cie domagaj� si� wi�kszego finansowania.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PP -= (int)Math.Round((decimal)manager.m_pCurrentParty.PP / 5);
                }
            ),
            new EventProvider(
                $"Wp�ywowy celebryta miesza nasz� parti� z b�otem.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(33f, 150f) / 10000;
                }
            ),
            new EventProvider(
                $"Lokalna wp�ywowa organizacja daje nam swoje wsparcie.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(100F, 200f) / 10000;
                    manager.m_pCurrentParty.PP += (manager.m_pCurrentParty.PP / 4);
                }
            ),
            new EventProvider(
                $"Portal internetowy publikuje szokuj�ce fakty na temat naszej opozycji, zyskujemy na tym g�osy.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(150f, 300f) / 10000;
                }
            ),
            new EventProvider(
                $"Jeden z postulat�w naszej partii staje si� viralem w Internecie, ludzie nas kochaj�.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(67f, 300f) / 10000;
                }
            ),
            new EventProvider(
                $"Niedawny spot wyborczy zdoby� niewyobra�aln� popularno��.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(67f, 250f) / 10000;
                }
            ),
            new EventProvider(
                $"Nasz dzia�acz {manager.m_pCurrentParty.GetRandomRecruitFullName()} wygrywa wp�ywow� i szanowan� nagrod� w swoim fachu.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(50f, 200f) / 10000;
                }
            ),
            new EventProvider(
                $"Pose� {manager.m_pCurrentParty.GetRandomRecruitFullName()} ca�kowicie zniszczy� konkurencj� we wczorajszej debacie wieczornej.",
                $"Lorem ipsum",
                () => {
                    manager.m_pCurrentParty.PPS += UnityEngine.Random.Range(67f, 350f) / 10000;
                }
            ),
            new EventProvider(
                $"Nasza partia popar�a najnowsz� ustaw�. Efekty spodziewane s� za kilka tygodni.",
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
                                Debug.Log("Pora�ka! Jaki� czas temu poparli�my ustaw�, kt�ra teraz znacz�co pogorszy�a jako� �ycia obywateli.");
                                manager.m_pCurrentParty.PPS -= UnityEngine.Random.Range(225f, 600f) / 10000;
                            }
                            else {
                                Debug.Log("Suckes! Jaki� czas temu poparli�my ustaw�, kt�ra teraz okaza�a si� by� strza�em w dziesi�tk�.");
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
