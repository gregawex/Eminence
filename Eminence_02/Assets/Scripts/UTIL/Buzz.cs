using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Buzz 
{
	static List<BuzzUnit> buzzes;

	static List<BuzzUnit> pendingBuzzes;
	static List<BuzzUnit> cancelNoInvokes;

	public static BuzzUnit [] Buzzes { 
		get { 
			if (buzzes == null) {
				buzzes = new List<BuzzUnit> ();
			}
				
			return buzzes.ToArray (); } 
	}

	public static BuzzUnit Set(string tag, float delay, Action a)
	{
		if (pendingBuzzes == null)
			pendingBuzzes = new List<BuzzUnit> ();

		BuzzUnit bu = new BuzzUnit(tag, delay, a, false);
		pendingBuzzes.Add(bu);

		return bu;
	}

	public static BuzzUnit SetInvert(string tag, float delay, Action a)
	{
		if (pendingBuzzes == null)
			pendingBuzzes = new List<BuzzUnit> ();
		
		BuzzUnit bu = new BuzzUnit(tag, delay, a, true);
		pendingBuzzes.Add(bu);
		
		return bu;
	}

	public static BuzzUnit Set(string tag, float delay, Action a, Action blink, float blinkDelay)
	{
		if (pendingBuzzes == null)
			pendingBuzzes = new List<BuzzUnit> ();

		BuzzUnit bu = new BuzzUnit(tag, delay, a, blink, blinkDelay);

		pendingBuzzes.Add(bu);

		return bu;
	}

	public static void BuzzKill(BuzzUnit buzzunit)
	{
		if (cancelNoInvokes == null)
			cancelNoInvokes = new List<BuzzUnit> ();

		cancelNoInvokes.Add (buzzunit);
	}

	public static void Update()
	{
		List<BuzzUnit> deletables = null;

		if (buzzes == null)
			buzzes = new List<BuzzUnit> ();


		foreach (BuzzUnit b in buzzes) {

			if(b.TimeHit())
			{
				b.callback.Invoke();

				if(deletables == null) deletables = new List<BuzzUnit>();
				deletables.Add(b);

			 
			}
		}

		if(deletables != null)
			foreach (BuzzUnit b in deletables) {
				buzzes.Remove(b);
		}

		if(pendingBuzzes != null)
		{
			buzzes.AddRange(pendingBuzzes);
			pendingBuzzes.Clear();
			pendingBuzzes = null;
		}

		if (cancelNoInvokes != null) {
			foreach(BuzzUnit b in cancelNoInvokes)
			{

				buzzes.Remove(b);
			 
			}

			cancelNoInvokes.Clear ();
			cancelNoInvokes = null;
		}
	}

	

 
}

/*
public class BuzzUnit
{
	public float start;
	public float delay;
	public Action callback;
	public string tag;

	Action blink;
	float blinkDelay;

	public float Elapsed { get { return Time.time - start; } }
	public float Perc { get { return (Elapsed / delay); } }
	
	public BuzzUnit(string tag, float delay, Action a)
	{
		this.tag = tag;
		this.callback = a;
		this.delay = delay;
		this.start = Time.time;
	}
	public BuzzUnit(string tag, float delay, Action a, Action blink, float blinkDelay)
		:this(tag, delay, a)
	{
		this.blink = blink;
		this.blinkDelay = blinkDelay;
	}

	float lastBlink;
	public bool TimeHit()
	{
		if (blink != null) 
		{
			if (Time.time - lastBlink > blinkDelay) 
			{
				lastBlink = Time.time;


				blink.Invoke();

			}
		}


		if (Time.time - start > delay)
			return true;
		
		return false;
	}



	
}

*/
