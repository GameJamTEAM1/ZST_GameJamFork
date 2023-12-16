using System;

public abstract class GameEvent<T> {
    public abstract void Dispatch(object sender, T e);
}
