using ScriptableObjectFramework.Events;
using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewAppEvent", menuName = "Scriptable Objects/Events/App")]
public class AppEventBacking : BaseEventBacking<App, AppUnityEvent> { }

public class AppUnityEvent : UnityEvent<App> { }

[Serializable]
public class AppEvent : BaseEvent<App, AppEventBacking, AppEventHandler> { }