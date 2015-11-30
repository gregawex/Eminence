private var color_time : float=0;
var color_switch :boolean;
var Delete_time: float = 0.5;

function Start ()
{
Destroy (gameObject,Delete_time);//0.5ç§’å¾Œåˆªé™¤ç‰©ä»¶
}


function Update () 
{
		if(color_switch == true)
		{
		color_time += Time.deltaTime * 3;
		GetComponent.<Renderer>().material.color =Color.Lerp(Color(0.4,0.4,0.4,0.4), Color.black, color_time);
		}
}