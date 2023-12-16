using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class DateEvent : GameEvent<CustomArgs.DateChangedArgs> {
    private GameDate eventDate;

    public DateEvent(GameDate date) {
        this.eventDate = date;
    }
    
    public override void Dispatch(object sender, CustomArgs.DateChangedArgs e) {
        if (e.gameDate == this.eventDate) {
            Debug.Log("Event dispatched!");
        }
    }
}
