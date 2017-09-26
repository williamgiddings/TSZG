
private var recoil : float = 0.0;
private var RecoverSpeed : float;
var isRecoiling : boolean;
private var Positive : boolean;
 
function recoiling(mRecoil, rSpeed, recover) 
{
    isRecoiling = true;
    RecoverSpeed = recover;
    if(recoil > 0 && transform.localRotation.x < 90)
    {
        var maxRecoil = Quaternion.Euler(-mRecoil, transform.localRotation.y, transform.localRotation.z);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, maxRecoil, Time.deltaTime * rSpeed);
        //transform.localRotation = Quaternion.Slerp(transform.localRotation, maxRecoil, Time.deltaTime * rSpeed);

        
    }
    isRecoiling = false;
   
}

function GetPlusMinus (rec)
{

    var PlMi : int;
    PlMi = Random.Range(0,1);
    if (PlMi == 0)
    {
        return (-rec)*0.25;
        Positive = false; 
    }
    else
    {
        return (rec)*0.25;
        Positive = true;
    }

}

function Update ()
{
    if (!isRecoiling)
    {
        var minRecoil = Quaternion.Euler(0, 0, transform.localRotation.z);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, minRecoil, Time.deltaTime * RecoverSpeed);
    }

}