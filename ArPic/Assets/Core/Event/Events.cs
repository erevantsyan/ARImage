using System;
using System.Collections.Generic;
using UnityEngine;

public readonly struct EventPrefabInitialitionMovie : IEvent
{
    public readonly Vector3 _pos;

    public EventPrefabInitialitionMovie(Vector3 pos)
    {
        _pos = pos;   
    }
}

// Old
public readonly struct EventGetByID : IEvent
{
    public readonly string _id;
    public readonly Action<Transform> _callback;

    public EventGetByID(string id, Action<Transform> callback)
    {
        _id = id;
        _callback = callback;
    }
}

public readonly struct EventUIDebug : IEvent
{
    public readonly string data;

    public EventUIDebug(string msg)
    {
        data = msg;
    }
}



public readonly struct EventARSetActive : IEvent
{
    public readonly bool data;

    public EventARSetActive(bool val)
    {
        data = val;
    }
}

public readonly struct EventCamUpdRay : IEvent
{
    public readonly Ray ray;

    public EventCamUpdRay(Ray val)
    {
        ray = val;
    }
}

public readonly struct EventCamTap : IEvent
{
    public readonly Ray ray;
    public readonly Vector2 pos;

    public EventCamTap(Vector2 pos, Ray ray)
    {
        this.ray = ray;
        this.pos = pos;
    }
}

public readonly struct EventCamHoldTapRay : IEvent
{
    public readonly Ray ray;

    public EventCamHoldTapRay(Ray val)
    {
        ray = val;
    }
}

public readonly struct EventARUpdatePose : IEvent
{
    public readonly bool isValid;
    public readonly Pose pose;

    public EventARUpdatePose(bool isValid, Pose pose)
    {
        this.isValid = isValid;
        this.pose = pose;
    }
}

public readonly struct EventSetDance : IEvent
{
    public readonly int data;

    public EventSetDance(int val)
    {
        data = val;
    }
}

public readonly struct EventSetMoveTo : IEvent
{
    public readonly Pose pose;

    public EventSetMoveTo(Pose pose)
    {
        this.pose = pose;
    }
}

public readonly struct EventLoadUpdate : IEvent
{
    public readonly float data;
    
    public EventLoadUpdate(float val)
    {
        data = val;
    }
}
public readonly struct EventLoadDone : IEvent
{
    
}

public readonly struct EventSelectCharacter : IEvent
{
    public readonly string data;

    public EventSelectCharacter(string val)
    {
        data = val;
    }
}

public readonly struct EventImageRequest : IEvent
{
    public readonly string url;
    public readonly Action<Texture2D> response;

    public EventImageRequest(string url, Action<Texture2D> response)
    {
        this.url = url;
        this.response = response;
    }
}

public readonly struct EventWebInit : IEvent
{
    public readonly string name;
    public readonly JSONObject data;

    public EventWebInit(string name, JSONObject data)
    {
        this.name = name;
        this.data = data;
    }
}

public enum EventUIType { none, open, close};
public readonly struct EventUI : IEvent
{
    public readonly string id;
    public readonly EventUIType type;
    public readonly object data;

    public EventUI(string id, object data = null, EventUIType type = EventUIType.none)
    {
        this.id = id;
        this.type = type;
        this.data = data;
    }
}