using UnityEngine;
using System.Collections;

public class ActorPhysics : MonoBehaviour {

	CharacterController ctrl;

	public float gravity = 0.01f;

	void Awake()
	{
		this.ctrl = GetComponent<CharacterController>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		float downwardsVel = ctrl.velocity.y;

		if(downwardsVel < 1)
		{
			ctrl.Move(Vector3.down * gravity );
		}
		else
		{
			ctrl.Move(Vector3.down * gravity * downwardsVel);
		}
		//transform.position += (Vector3.down * gravity);
	
	}
}
