using CustomArgs;
using UnityEngine;

public class GUIEvent : GameEvent<GUIArgs>
{
    private GameObject eventGui;
    private GUIArgs.EGuiEvent eventGuiType;

    public GUIEvent(GameObject gui, GUIArgs.EGuiEvent type) {
        this.eventGui = gui;
        this.eventGuiType = type;
    }
    
    public override void Dispatch(object sender, CustomArgs.GUIArgs e) {
        if (e.gui == this.eventGui && e.type == this.eventGuiType) {
            Debug.Log("Event dispatched!");
        }
    }
}