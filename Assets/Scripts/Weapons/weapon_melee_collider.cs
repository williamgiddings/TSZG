using UnityEngine;
using System.Collections;

public class weapon_melee_collider : MonoBehaviour {

bool  swinging;
Transform BaseWeapon;
Transform BloodParticle;
Transform ImpactParticle;


void Update (){
    swinging = BaseWeapon.GetComponent<weapon_melee>().swinging;   
}

void OnTriggerEnter ( Collider col  ){
    
    if (swinging)
    {
        
        BaseWeapon.GetComponent<weapon_melee>().swinging = false;
        if (col.gameObject.tag == "zombie")
        {
           // BaseWeapon.GetComponent<weapon_melee>().Hit(col.transform, true, BaseWeapon.GetComponent<weapon_melee>().HitMode, null);
            
        }
        else
        {
           // BaseWeapon.GetComponent<weapon_melee>().Hit(col.transform, false, BaseWeapon.GetComponent<weapon_melee>().HitMode, null);
            
        }
    }
}

}