var MoveAmount : float = 1;
var MoveSpeed : float = 2;
var GUN : GameObject;
var MoveOnX : float;
var MoveOnY : float;
var DefaultPos : Vector3; 
var NewGunPos : Vector3;
var ironsightPos : Vector3;
var isIron : boolean;
var ironSpeed : float;

 
 
 
 
function Update ()
{
    if (!isIron)
    {
        MoveOnX = Input.GetAxis("Mouse X") * MoveAmount;
        MoveOnY = Input.GetAxis("Mouse Y") * MoveAmount;
        NewGunPos = new Vector3 (DefaultPos.x+MoveOnX, DefaultPos.y+MoveOnY, DefaultPos.z);
        GUN.transform.localPosition = Vector3.Lerp(GUN.transform.localPosition, NewGunPos, MoveSpeed);
    }
    else
    {
        MoveOnX = Input.GetAxis("Mouse X") * (MoveAmount/2);
        MoveOnY = Input.GetAxis("Mouse Y") * (MoveAmount/2);
        NewGunPos = new Vector3 (ironsightPos.x+MoveOnX, ironsightPos.y+MoveOnY, ironsightPos.z);
        GUN.transform.localPosition = Vector3.Lerp(GUN.transform.localPosition, NewGunPos, ironSpeed);
    }
}
 
 