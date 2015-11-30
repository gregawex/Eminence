var target : Transform;
var Rotation : float;
/*function Update() 
{
	transform.Rotate(Vector3.up * Time.deltaTime*100);
}*/

function OnMouseOver ()
{
	if(Rotation  == 1)
	{
		target.Rotate(Vector3.up * Time.deltaTime*150);
	}
	if(Rotation  == 2)
	{
		target.Rotate(Vector3.up * Time.deltaTime*-150);
	}
}