using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {
    private class EventInfo {
        public float since;
        public float multiplier;

        public EventInfo(byte since, float multiplier) {
            this.since = since;
            this.multiplier = multiplier;
        }
    }

    [SerializeField]
    private GameObject gameManagerObject;
    private GameManager GM;
    private MinorEventProvider minorEventProvider;
    private MajorEventProvider majorEventProvider;
    private CriticalEventProvider criticalEventProvider;
    private EventInfo majorEvent = new EventInfo(0, 0);
    private EventInfo criticalEvent = new EventInfo(0, 0);

    private const uint POLL = 8;
    private const uint ELECTIONS_ANNOUNCEMENT = 34;
    private const uint ELECTIONS = 40;

    private void Start() {
        this.GM = this.gameManagerObject.GetComponent<GameManager>();

        minorEventProvider = this.GetComponent<MinorEventProvider>();
        majorEventProvider = this.GetComponent<MajorEventProvider>();
        criticalEventProvider = this.GetComponent<CriticalEventProvider>();

        Dispatcher.DateChanged += (s, e) => {
            if (e.turns % POLL == 0 && e.turns % ELECTIONS != 0) {
                GM.ECV.DispatchEventCanvas(new EventProvider($"Sonda¿.\n\nNasz wynik: {(GM.m_pCurrentParty.PPS * 100).ToString("0.00")}%", "", null));
            }

            if (e.turns % ELECTIONS_ANNOUNCEMENT == 0) {
                GM.ECV.DispatchEventCanvas(new EventProvider("Za nied³ugo przeprowadzone zostan¹ wybory do Sejmu.", "", null));
            }

            if (e.turns % ELECTIONS == 0) {
                GM.ECV.DispatchEventCanvas(new EventProvider("Wybory zosta³y przeprowadzone, a wyniki policzone.", "", null));
            }
        };
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            this.GM.NextTurn();

            GenerateEvent();
        }
    }

    private void GenerateEvent() {
        if (criticalEvent.since >= 24) {
            GetEvent(EventType.CRITICAL);

            majorEvent.since++;
            criticalEvent.since = 0;
            criticalEvent.multiplier = 0;
        }
        else if (majorEvent.since >= 4) {
            GetEvent(EventType.MAJOR);
            
            criticalEvent.since++;
            majorEvent.since = 0;
            majorEvent.multiplier = 0;
        }
        else {
            int random = UnityEngine.Random.Range(1, 100 + 1);

            if (Math.Floor(random + criticalEvent.multiplier) >= 98) {
                criticalEvent.since = 0;
                criticalEvent.multiplier = 0;

                GetEvent(EventType.CRITICAL);
            }
            else if (Math.Floor(random + majorEvent.multiplier) >= 85) {
                majorEvent.since = 0;
                majorEvent.multiplier = 0;

                GetEvent(EventType.MAJOR);
            }
            else {
                majorEvent.multiplier += 5;
                criticalEvent.multiplier += 0.5f;

                majorEvent.since++;
                criticalEvent.since++;

                GetEvent(EventType.MINOR);
            }
        }
    }

    private void GetEvent(EventType type) {
        switch(type) {
            case EventType.MINOR:
                var minor = minorEventProvider.ProvideRandomEvent();

                GM.ECV.DispatchEventCanvas(minor);
                break;
            case EventType.MAJOR:
                var major = majorEventProvider.ProvideRandomEvent();

                GM.ECV.DispatchEventCanvas(major);
                break;
            case EventType.CRITICAL:
                var critical = criticalEventProvider.ProvideRandomEvent();

                GM.ECV.DispatchEventCanvas(critical);
                break;
        }
    }
}
