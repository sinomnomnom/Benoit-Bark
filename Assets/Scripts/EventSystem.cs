using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem
{
    private List<Event> events = new();

    public void TriggerEvent(Event e)
    {
        foreach (Action action in e.actions)
        {
            action.Invoke();
        }
    }

    public Event GetEvent(String eventName)
    {
        foreach (Event e in events)
        {
            if (e.name == eventName)
            {
                return e;
            }
        }

        return null;
    }

    public void TriggerEvent(String eventName)
    {
        TriggerEvent(GetEvent(eventName));
    }

    public Event AddEvent(String eventName)
    {
        Event e = new Event(eventName);
        events.Add(e);
        return e;
    }

    public  void RemoveEvent(String eventName)
    {
        events.Remove(GetEvent(eventName));
    }

}

public class Event
{
    public string name;

    public List<Action> actions = new();

    public void AddAction(Action action)
    {
        actions.Add(action);
    }
    public void RemoveAction(Action action)
    {
        actions.Remove(action);
    }

    public Event(String eventName)
    {
        name = eventName;
    }

}
