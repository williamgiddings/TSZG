var LightAttack : Attacks;
private var BaseScript;
private var timeSinceLast : float;


function Start ()
{
    BaseScript = transform.root.GetComponent("zombie");
}

function OnTriggerStay (col : Collider)
{
    if (col.transform.tag == "Player")
    {
        if (Time.time > (timeSinceLast + LightAttack.Cooldown))
        {
            Attack(col.transform, LightAttack);
        }
    }
}

	
function Attack (target, type)
{
    BaseScript.Attacking = true;
    transform.root.GetComponent.<Animation>().clip = type.AttackAnim;
    transform.root.GetComponent.<Animation>().Play();
    transform.root.GetComponent.<AudioSource>().PlayOneShot(type.Sound);
    target.GetComponent("player").TakeHealth(type.Damage, 0);
    timeSinceLast = Time.time;
    yield WaitForSeconds (type.AttackAnim.length);
    BaseScript.Attacking = false;
}
	
class Attacks extends System.Object
{
    var Damage : int;
    var AttackAnim : AnimationClip;
    var Cooldown : float;
    var Sound : AudioClip;

}