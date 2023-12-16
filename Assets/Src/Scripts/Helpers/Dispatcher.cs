using System;

public static class Dispatcher {
    public static event EventHandler<CustomArgs.DateChangedArgs> DateChanged;
    
    public static void DispatchDateChanged(object sender, CustomArgs.DateChangedArgs e) {
        DateChanged.Invoke(sender, e);
    }

    public static event EventHandler<CustomArgs.GUIArgs> GuiStateChanged;
    public static void DispatchGuiStateChanged(object sender, CustomArgs.GUIArgs e) {
        GuiStateChanged.Invoke(sender, e);
    }
}
