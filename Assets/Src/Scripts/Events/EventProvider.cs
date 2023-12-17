using System;

public class EventProvider {
    public string header;
    public string description;
    public Action action;

    public EventProvider(string header, string description, Action action) {
        this.header = header;
        this.description = description;
        this.action = action;
    }
}
