using System;
using UnityEngine;
using System.Collections;

public class BuzzUnit
{
	//----------------------------------------------------------------------------
	// Public Variables:
	//----------------------------------------------------------------------------
	
	public float start;
	public float delay;
	public Action callback;
	public string tag;
	
	//----------------------------------------------------------------------------
	// Private Variables:
	//----------------------------------------------------------------------------
	
	private Action blink;
	private float blinkDelay;
	private float lastBlink;
	private bool invert;
	
	//----------------------------------------------------------------------------
	// Properties:
	//----------------------------------------------------------------------------
	
	public float Elapsed { get { return Time.time - start; } }
	public float Perc 
	{
		get 
		{ 
			if(!invert)
				return ( Elapsed / delay); 
			else 
				return 1 - (Elapsed / delay);
		} 
	}
	
	//----------------------------------------------------------------------------
	// Constructors:
	//----------------------------------------------------------------------------
	
	public BuzzUnit (string tag, float delay, Action a, bool invert)
	{
		this.tag = tag;
		this.callback = a;
		this.delay = delay;
		this.start = Time.time;
		this.invert = invert;
	}
	
	public BuzzUnit (string tag, float delay, Action a, Action blink, float blinkDelay) : this(tag, delay, a, false)
	{
		this.blink = blink;
		this.blinkDelay = blinkDelay;
	}
	
	//----------------------------------------------------------------------------
	// Public Methods:
	//----------------------------------------------------------------------------
	
	public bool TimeHit()
	{
		if (blink != null) {
			if (Time.time - lastBlink > blinkDelay) {
				lastBlink = Time.time;
				
				blink.Invoke();
			}
		}
		
		if (Time.time - start > delay){
			return true;
		}
		
		return false;
	}
}