using System;
using MiscUtil;
using UnityEngine;

namespace CustomArgs {
    public class DateChangedArgs : EventArgs {
        public GameDate gameDate;
        public uint turns;

        public DateChangedArgs(GameDate gameDate, uint turns) {
            this.gameDate = gameDate;
            this.turns = turns;
        }
    }

    public class GUIArgs : EventArgs
    {
        public enum EGuiEvent
        {
            NONE = -1,
            OPEN,
            CLOSE
        }
        
        public GameObject gui;
        public EGuiEvent type;

        public GUIArgs(GameObject gui, EGuiEvent type)
        {
            this.gui = gui;
            this.type = type;
        }
    }
}
