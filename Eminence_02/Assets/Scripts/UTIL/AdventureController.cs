using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Character))]
public class AdventureController: MonoBehaviour {

	public bool isUserControlled = true;

	public bool snapToWalls = true;
	public bool limitToWall = true;
	public float jumpSpeed = 4.5f;
	
	private Animator anim;
	private float timer;
	private CharacterController controller;
	private bool covered;
	private float jumpHeight;
	private bool heightRecorded;
	private float offGroundDamp = 0.15f;
	private float offGroundTimer;

	const float turnSpeed = 5f;



	[HideInInspector]
	public AIBot bot;

	[HideInInspector]
	public CharacterController characterCtrl;

	Vector3 targetPos;

	public enum State { IDLE, WALKING }
	State state;
	
	void Start () {
		anim = this.transform.GetComponent<Animator>();
		controller = GetComponent<CharacterController>();

		bot = GetComponent<AIBot> ();
		characterCtrl = GetComponent<CharacterController> ();
	}

	void LateUpdate()
	{
		if (lockPos) {
			this.transform.position = this.transform.position + Vector3.forward * 0.01f;
				}
	}

 
	bool lockPos ;
	Vector3 lockVec;
	void Update () {

		if (Input.GetKeyDown ("a")) {

			lockPos = true;
			lockVec = transform.position;

			GetComponent<Animator>().Play("Test");
			//return;
				}

		if (lockPos)
						return;

		float vertical = 0f;
		float horizontal = 0f;


		 
		if (state == State.IDLE) {

					vertical = 0f;
				} else if (state == State.WALKING) {
					 
					vertical = 1f;

				}




		
		#region Movement
		//Get Input
		// horizontal = Input.GetAxis("Horizontal");
		// vertical = Input.GetAxis("Vertical");
		
		//Detect intended direction and relay to Animator
		if (horizontal > 0.05f){
			anim.SetBool("Right", true);
			anim.SetBool("Left", false);
		}

		if (horizontal < -0.05f){
			anim.SetBool("Right", false);
			anim.SetBool("Left", true);
		}
		
		//If under the effect of cover, check for limitations and positioning requirements, like snapping to the wall
		/*if (anim.GetBool("Cover")){
			if(limitToWall){
				Vector3 newPos = transform.position + transform.right * 0.6f;
				newPos.y += 0.8f;
				Vector3 newPos2 = transform.position + -transform.right * 0.6f;
				newPos2.y += 0.8f;
				
				if (!Physics.Raycast(newPos, transform.forward, 1f)){
					horizontal = Mathf.Clamp(horizontal, 0f, -1f);
				}
				if (!Physics.Raycast(newPos2, transform.forward, 1f)){
					horizontal = Mathf.Clamp(horizontal, 0f, 1f);
				}
			}
			if (snapToWalls){
				Vector3 newPos = transform.position;
				newPos.y += 0.8f;
				RaycastHit hit;
				Physics.Raycast(newPos, transform.forward, out hit, 1);
				transform.forward = -hit.normal;
			}
		}*/
		
		//Apply input values to speed and direction after being filtered
		anim.SetFloat("Speed", vertical);
		anim.SetFloat("Direction", horizontal);
		
		//Procedural rotation input, applied while moving or still. This allows turning without the need for turning animations.
		if (vertical > 0.05f){
			if(anim.GetFloat("Direction") > 0.05f)
				this.transform.Rotate(Vector3.up * (Time.deltaTime + turnSpeed), Space.World);
			if(anim.GetFloat("Direction") < -0.05f)
				this.transform.Rotate(Vector3.up * (Time.deltaTime + -turnSpeed), Space.World);
		}
		if (vertical < 0.05f){
			if(anim.GetFloat("Direction") > 0.05f){
				this.transform.Rotate(Vector3.up * (Time.deltaTime + turnSpeed), Space.World);
			}
			if(anim.GetFloat("Direction") < -0.05f){
				this.transform.Rotate(Vector3.up * (Time.deltaTime + -turnSpeed), Space.World);
			}
		}
		#endregion
		
		#region Cover Tests
		//ENTER cover by colliding with a wall
		/*if ((controller.collisionFlags & CollisionFlags.Sides) != 0 && anim.GetBool("Cover") == false){
			Vector3 newPos = transform.position;
			newPos.y += 1.8f;
			RaycastHit hit;
			
			if (Physics.Raycast(newPos, transform.forward, 1)){
				anim.SetBool("Cover", true);
			} else{
				anim.SetBool("Crouched", true);
				anim.SetBool("Cover", true);
			}
			newPos.y -= 1f;
			Physics.Raycast(newPos, transform.forward, out hit, 4);
			transform.forward = -hit.normal;
		}*/
		
		//EXIT cover by backing out of it.
		/*if (anim.GetBool("Cover") == true && vertical < - 0.05f){
			anim.SetBool("Cover", false);
			StartCoroutine (TriggerAnimatorBool ("ForceState") );
		}*/
		#endregion

		/*
		if (Input.GetButtonDown("Jump")){
			StartCoroutine ( TriggerAnimatorBool("Jump"));
			StartCoroutine ( Jump());
		}*/
		
		/*if(controller.isGrounded){
			offGroundTimer = 0;
			if (heightRecorded){
				anim.SetFloat("FallDistance", jumpHeight - transform.position.y);
				heightRecorded = false;
			}
			anim.SetBool("Grounded", true);
		}
		else {
			if(offGroundTimer > offGroundDamp){
				offGroundTimer = 0;
				anim.SetBool("Grounded", false);
				if(!heightRecorded){
					jumpHeight = transform.position.y;
					heightRecorded = true;
				}
			} else {
				offGroundTimer += Time.deltaTime;
			}
		}
		*/
		
		// Idle timer: if there is no input from the vertical or horizontal, the timer rises until it
		// triggers the alternate idle animation, then resets. The timer will pause of the character begins moving.
		if (vertical == 0 && horizontal == 0){
			timer += Time.deltaTime;
			if(timer > 10f){
				anim.SetInteger("AltIdle", Random.Range(0,2));
				StartCoroutine (TriggerAnimatorBool ("ChangeIdle") ); 
				timer = 0;
			}
		}
	}

	public void SetState(State state)
	{
		this.state = state;
	}

 
	
	///Triggers the bool of the provided name in the animator.
	///The bool is only active for a single frame to prevent looping.
	private IEnumerator TriggerAnimatorBool (string name){
		anim.SetBool(name, true);
		yield return null;
		anim.SetBool(name, false);
	}
	
	private IEnumerator Jump (){
		float timer = 0;
		while(timer < 0.5f){
			timer += Time.deltaTime;
			controller.Move((Vector3.up * jumpSpeed) * Time.deltaTime);
			yield return null;
		}
	}
}
