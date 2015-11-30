private var assault : float =0.2;
var assault_mesh : GameObject;

function Update() 
{
	if (GetComponent.<Animation>().IsPlaying("assault"))
	{
	assault += Time.deltaTime *1;
	//Debug.Log("123");
		if(assault >= 0.2)
		{
		Instantiate(assault_mesh,transform.localPosition,transform.rotation);
		assault =0;
		}
	}
}