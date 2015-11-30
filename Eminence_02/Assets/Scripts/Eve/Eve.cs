
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//==========================================================================================
/*
███████╗██╗   ██╗███████╗
██╔════╝██║   ██║██╔════╝	Fun fact: the exact same code runs in one of the Zynga titles :P
█████╗  ██║   ██║█████╗  
██╔══╝  ╚██╗ ██╔╝██╔══╝  	By Greg Farkas
███████╗ ╚████╔╝ ███████╗
╚══════╝  ╚═══╝  ╚══════╝
 */
//===========================================================================
public static class Eve 
{
	
	public static Dictionary<Type, Dictionary<string, List<EventPack>>> pool 			= new Dictionary<Type, Dictionary<string, List<EventPack>>>();
	static Dictionary<string, List<Type>> registry 										= new Dictionary<string, List<Type>>();
	static List<EventPack> persistantPack 												= new List<EventPack>();


	public static void Listen<T>(Action<BaseEvent> action, EventListener listener) where T : BaseEvent
	{
		DoListen (false, typeof(T), action , listener);
	}


	public static void ClearAll()
	{
		pool.Clear();
		registry.Clear();

		foreach(EventPack p in persistantPack)
		{
			DoListen(false, p.type, p.action, p.listener);
		}
	}
	/// <summary>
	/// Internal subscription logic.
	/// </summary>
	private static void DoListen(bool listenOnce, Type eventType, Action<BaseEvent> action, EventListener listener )
	{

		 
		//Validate the type. It has to be derived from BaseEvent.	
		Type listenerType = listener.GetType();
		string listenerName = listenerType.FullName;

		
		//if(eventType.BaseType != typeof(BaseEvent)){Debug.LogError ("Invalid event type passed in to Eve: "+eventType+". Make sure the event is derived from BaseEvent"); return; }
		//if(!ValidateEventType(eventType, typeof(BaseEvent))){Debug.LogError ("Invalid event type passed in to Eve: "+eventType+". Make sure the event is derived from BaseEvent"); return; }

		//Check whether this event has been cached. If not, cache it.
		
		//Event has been cached.
		if(pool.ContainsKey(eventType))
		{
			
			// Validate whether this listener is subscribed to this event already. If so, discard it.
			//(A listener can't be subscribed to the same event more than once)
			
			//The listener type is already subscrbibed .
			if(pool[eventType].ContainsKey(listenerName))
			{
				
				//Make sure this listener hasn't been subscribed yet
				foreach(EventPack ep in pool[eventType][listenerName])
				{
					if(ep.listener == listener) return; 
				}
				
				//Subscribe.
				pool[eventType][listenerName].Add(new EventPack(listenOnce, action, eventType, listener)) ;
				
				
			}
			//The listener type is not subscribed yet.
			else
			{
				pool[eventType].Add(listenerName, new List<EventPack>());
				List<EventPack> evp = pool[eventType][listenerName];
				evp.Add(new EventPack(listenOnce, action, eventType, listener));
				
				
				//Register the listener
				Register (listener, listenerName, eventType);
			}
			
		}
		//Event hasn't been cached; cache it.
		else
		{
			Dictionary<string, List<EventPack>> dict = new Dictionary<string, List<EventPack>>();
			
			List<EventPack> evp = new List<EventPack>();
			EventPack ep = new EventPack(listenOnce, action, eventType, listener);
			evp.Add(ep);
			dict[listenerName] = evp;
			
			pool.Add(eventType, dict);
			
			//Register the listener
			Register (listener, listenerName, eventType);
			
		}
	}
		
	/*private static bool ValidateEventType(this Type t, Type baseType)
	{
		Type cur = t.BaseType;

	    while (cur != null)
	    {
	        if (cur.Equals(baseType))
	        {
	            return true;
	        }
	
	        cur = cur.BaseType;
	    }
	
	    return false;		
	}*/
	
	/// <summary>
	/// Unsubscribe the listener from a certain eventType.
	/// </summary>
	/// 
	public static void Stop(Type eventType,  EventListener listener)
	{
		string listenerName = listener.GetType().FullName;
		
		
		EventPack foundPack = ListenerToPack(eventType, listener);
		if(foundPack == null) return;
		


		pool[eventType][listenerName].Remove(foundPack);
		
		
		//If there are no listeners subscribed to this event type, remove the event type from the pool.
		if(pool[eventType][listenerName].Count == 0)
		{
			pool[eventType].Remove(listenerName);
			if (pool[eventType].Count == 0)
			{
				pool.Remove(eventType);
			}
		}
	}

	public static void CancelListener(EventListener listener)
	{

	}
	
	public static void CancelListener(EventListener listener, string listenerName)
	{
		Unregister (listener, listenerName);
	}

	/// <summary>
	/// Register the listener. The registry keeps track of all the event types this listener is subscribed to.
	/// NOTE: The use of registry saves a lot of iterations when a listener wants to unsubscribe from all the events it's listening to (eg. OnDestroy).
	/// Think of the registry as a backwards lookup table.
	/// </summary>
	private static void Register(EventListener listener, string listenerName, Type eventType)
	{
		
		if(registry.ContainsKey(listenerName))
		{
			//Add the event type to the existing registry
			//In foreasch(int i=0 ; i<< 0){ RESPOND; }
			List<Type> typeList = registry[listenerName];
			typeList.Add(eventType);
		}
		else
		{
			//Register the listener
			
			List<Type> typeList = new List<Type>();
			typeList.Add(eventType);
			registry.Add(listenerName, typeList);
		}
	}
	
	/// <summary>
	/// Unregisters the listener. This makes the listener unsubscribe from all the events and gets deleted from the cache.
	/// </summary>
	private static void Unregister(EventListener listener, string listenerName)
	{
		
		if(!registry.ContainsKey(listenerName)) return;
		
		List<Type> types = registry[listenerName];
		
		foreach(Type t in types)
		{
			Debug.Log ("#### In Unregister");
			Stop (t, listener);
		}
		
		registry.Remove(listenerName);
	}
	
	private static List<EventPack> GetPackList(Type type, string listenerName)
	{
		
		if(!pool.ContainsKey(type)) return null;
		Dictionary<string, List<EventPack>> dict = pool[type];
		
		if(dict.ContainsKey(listenerName)) return dict[listenerName];
		
		
		return null;
	}
	
	private static EventPack ListenerToPack(Type type, EventListener listener)
	{
		List<EventPack> packs = GetPackList (type, listener.GetType().FullName);

		if (packs != null)
		{
			foreach(EventPack pack in packs)
			{
				if(pack.listener == listener) return pack;
			}
		}
		
		return null;
	}

	public static void TriggerEvent(BaseEvent ev, out object response)
	{
		response = DoTriggerEvent (ev);
	}

	public static void TriggerEvent(BaseEvent ev)
	{
		DoTriggerEvent (ev);
	}
	
	/// <summary>
	/// Triggers an event. All listeners who are subscribed to this event will fire.
	/// </summary>
	private static object DoTriggerEvent(BaseEvent ev)
	{

		foreach(KeyValuePair<Type, Dictionary<string, List<EventPack>>> kvp in pool)
		{

			GregBugger.Log("["+kvp.Key+"] ");

			foreach(KeyValuePair<string, List<EventPack>> k in kvp.Value)
			{
				GregBugger.Log ("  -"+k.Key);

				foreach(EventPack ep in k.Value)
				{
					GregBugger.Log ("     -"+ep.listener.ToString()+", "+ep.type.Name);
				}
			}
		
		}


		object response = null;

		List<DeletePack> deletables = null;
		
		Type t = ev.GetType();
		
		if(pool.ContainsKey(t))
		{
			
			foreach(KeyValuePair<string, List<EventPack>> kvp in pool[t])
			{
				foreach(EventPack ep in kvp.Value)
				{

					bool invalidAction = false;


					
					//If the action has lost its target it gets deleted instead of being invoked. 
					//The target is most likely lost if the EventListener object is destroyed but the programmer didn't cancel the listener on destroy.
					if(ep.listener.ToString() != "null") 
					{
//						if(ep.type == typeof(FiredDraggedWeapon) && ep.action.Method.Name == "DragTargetShot")
//						{
//							Debug.Log("#### Invoking valid FiredDraggedWeapon DragTargetShot");
//						}
						ep.action.Invoke(ev);
						//response = ev.Response
					}
					else 
					{
//						if(ep.type == typeof(FiredDraggedWeapon) && ep.action.Method.Name == "DragTargetShot")
//						{
//							// HACK TO HELP RUN DOWN WHY THIS IS NOT BEING LISTENED TO
//							Debug.Log("#### Event listener has been set to NULL - invalidAction!");
//						}
						invalidAction = true;
					}
	
					if(ep.deleteAfterInvoke || invalidAction)
					{
						if(deletables == null) deletables = new List<DeletePack>();

						//try
						//{

						deletables.Add(new DeletePack(t, ep, kvp.Key));
//						if(ep.type == typeof(FiredDraggedWeapon) && ep.action.Method.Name == "DragTargetShot")
//						{
//							// HACK TO HELP RUN DOWN WHY THIS IS NOT BEING LISTENED TO
//							Debug.Log("#### Been asked to delete a FiredDraggedWeapon DragTargetShot");
//						}
						//}
						/*catch(System.Exception e)
						{telepathethic
							foreach(KeyValuePair<string, EventListener> k in listeners)
							{
								Debug.LogError(" listener element: "+k.Key);
							} 
						}*/
					}
				}
			}
		}
		else
		{
			//Noone's listening to this event
		}
		
		if(deletables != null)
		{
			
			foreach(DeletePack dp in deletables)
			{
				Stop (dp.eventType, dp.pack.listener);
			}
		}

		return response;
	}
	
	
	/// <summary>
	/// Struct that holds event data together.
	/// </summary>
	public class EventPack
	{
		public Action<BaseEvent> action;
		public Type type;
		public EventListener listener;
		public bool deleteAfterInvoke;
		
		public EventPack(bool deleteAfterInvoke, Action<BaseEvent> action, Type type, EventListener listener)
		{
			this.action = action;
			this.type = type;
			this.deleteAfterInvoke = deleteAfterInvoke;
			this.listener = listener;
			
		}
		
		
	}
	
	/// <summary>
	/// Struct that holds temporary deletion info together.
	/// </summary>	
	private struct DeletePack
	{
		public Type eventType;
		public EventPack pack;
		public string listenerName;
		
		public DeletePack ( Type eventType, EventPack pack, string listenerName)
		{
			this.eventType = eventType;
			this.pack = pack;
			this.listenerName = listenerName;

		
		}
	}
}

public class EventArguments
{
	Type targetType;
	public Type TargetType { get { return targetType; } }
	
	public EventArguments(Type targetType) 
	{
		this.targetType = targetType; 


	}
	
	public T GetArgsObj<T>(EventArguments input) where T : EventArguments
	{
		return (T) Convert.ChangeType(input, typeof(T));
	}
}


public interface EventListener
{
	
}

