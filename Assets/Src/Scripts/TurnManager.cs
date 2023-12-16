using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager {
    private class EventInfo {
        public float since;
        public float multiplier;
 
        public EventInfo(byte since, float multiplier) {
            this.since = since;
            this.multiplier = multiplier;
        }
    }
    
    private GameManager GM;
    private EventInfo majorEvent = new EventInfo(0, 0);
    private EventInfo criticalEvent = new EventInfo(0, 0);

    private const uint POLL = 8;
    private const uint ELECTIONS_ANNOUNCEMENT = 34;
    private const uint ELECTIONS = 40;

    public TurnManager() {
        GM = GameManager.Instance;
        Dispatcher.DateChanged += (s, e) => {
            if (e.turns % POLL == 0 && e.turns % ELECTIONS != 0) {
                Debug.Log("Sondaż!");
            }

            if (e.turns % ELECTIONS_ANNOUNCEMENT == 0) {
                Debug.Log("Niedługo wybory!");
            }

            if (e.turns % ELECTIONS == 0) {
                Debug.Log("Wybory!!!");
            }
        };
    }

    public void NextTurn()
    {
        GM.NextTurn();
        GenerateEvent();
    }

    private void GenerateEvent() {
        Debug.Log("GENERATE EVENT");
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
            int random = UnityEngine.Random.Range(1, 100);

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
                Debug.Log("Zwykły event!");
                break;
            case EventType.MAJOR:
                Debug.Log("Ważny event!");
                break;
            case EventType.CRITICAL:
                Debug.Log("Krytyczny event!");
                break;
        }
    }
}
