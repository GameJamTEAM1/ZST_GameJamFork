using System;
using UnityEngine;

public class GenericEvent : GameEvent<CustomArgs.DateChangedArgs> {
    public override void Dispatch(object sender, CustomArgs.DateChangedArgs e) {
        //TODO
        Debug.Log("Generic event manager");
    }
}
