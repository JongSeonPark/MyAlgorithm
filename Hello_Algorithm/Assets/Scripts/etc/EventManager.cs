using System;
using System.Collections;
using System.Collections.Generic;

public class EventManager : Singleton<EventManager>
{
    Dictionary<Type, Action<IEventBase>> events = new Dictionary<Type, Action<IEventBase>>();
    Dictionary<Delegate, Action<IEventBase>> eventLookups = new Dictionary<Delegate, Action<IEventBase>>();

    public void AddListener<T>(Action<T> evt) where T : IEventBase
    {
        if (!eventLookups.ContainsKey(evt))
        {
            Action<IEventBase> newAction = (e) => evt((T)e);

            if (events.TryGetValue(typeof(T), out var action))
                events[typeof(T)] = action += newAction;
            else
                events[typeof(T)] = newAction;

            eventLookups.Add(evt, newAction);
        }
    }

    public void RemoveListener<T>(Action<T> evt) where T : IEventBase
    {
        if (eventLookups.TryGetValue(evt, out var action))
        {
            if (events.TryGetValue(typeof(T), out var tempAction))
            {
                tempAction -= action;
                if (tempAction == null)
                    events.Remove(typeof(T));
                else
                    events[typeof(T)] = tempAction;
            }

            eventLookups.Remove(evt);
        }
    }

    public void SendEvent<T>(T data) where T : IEventBase
    {
        if (events.TryGetValue(typeof(T), out var action))
        {
            action.Invoke(data);
        }
    }
}


public interface IEventBase 
{

}