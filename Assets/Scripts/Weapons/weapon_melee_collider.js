
var swinging : boolean;
var BaseWeapon : Transform;
var BloodParticle : Transform;
var ImpactParticle : Transform;


function Update ()
{
    swinging = BaseWeapon.GetComponent("weapon_melee").swinging;   
}

function OnTriggerEnter (col : Collider)
{
    
    if (swinging)
    {
        
        BaseWeapon.GetComponent("weapon_melee").swinging = false;
        if (col.gameObject.tag == "zombie")
        {
            BaseWeapon.GetComponent("weapon_melee").Hit(col.transform, true, BaseWeapon.GetComponent("weapon_melee").HitMode, null);
            
        }
        else
        {
            BaseWeapon.GetComponent("weapon_melee").Hit(col.transform, false, BaseWeapon.GetComponent("weapon_melee").HitMode, null);
            
        }
    }
}
