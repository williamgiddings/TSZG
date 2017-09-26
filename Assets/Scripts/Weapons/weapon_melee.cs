using UnityEngine;
using System.Collections;

public class weapon_melee : MonoBehaviour
{
    [Header ("Weapon Info")]
    string WeaponName;
    string WeaponType;
    float Firerate; // 1 / (60 / [firerate])
    float Damage;
    AudioClip[] ImpactSounds;
    float AbsoluteRange;
    int MagSize;
    string AmmoType;
    bool  Melee = true;

    [Header ("Components")]
    public bool swinging;
    Transform Body;
    private bool  CanShoot = true;
    Inventory Inventory;
    private GameObject target; 
    public int HitMode;


    void Awake (){
        GetComponent<Animation>().Play(WeaponName+"_draw");
    }

    void Update (){
        if (CanShoot == true )
        {
            if (Input.GetButtonDown("Fire")) 
            {
                Shoot(0);    
                CanShoot = false;
            }
            else if (Input.GetAxis("Trigger") == 1 && CanShoot == true)
            {
                Shoot(0);
                CanShoot = false;
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                Shoot(1);    
                CanShoot = false;
            }
            else if (Input.GetAxis("Trigger") == -1 && CanShoot == true)
            {
                Shoot(1);    
                CanShoot = false;
            }
	
        }
        if (GetComponent<Animation>().clip == null)
        {
            GetComponent<Animation>().CrossFade(WeaponName+"_idle");
        }
    }

    public void Shoot (int mode)
    {
        StartCoroutine(_shoot(mode));
    }

    IEnumerator _shoot (int Mode){
        HitMode = Mode;
	
	    if (Mode == 0)
        {
            GetComponent<Animation>()[WeaponName + "_fire_01"].speed = 2;
            swinging = true;
            GetComponent<Animation>().Play(WeaponName+"_fire_01");
            yield return new WaitForSeconds (GetComponent<Animation>()[WeaponName + "_fire_01"].length/1.5f);
            StartCoroutine(Delay(((1/(Firerate/60))-(GetComponent<Animation>()[WeaponName + "_fire_01"].length/1.5f))*1.2f));
            swinging = false;
        }
        else
	    {
	        GetComponent<Animation>()[WeaponName + "_fire_02"].speed = 1.1f;
	        swinging = true;
	        GetComponent<Animation>().Play(WeaponName+"_fire_02");
	        yield return new WaitForSeconds (GetComponent<Animation>()[WeaponName + "_fire_02"].length/1.25f);
	        StartCoroutine(Delay(((1/(Firerate/60))-(GetComponent<Animation>()[WeaponName + "_fire_02"].length/1.25f))*1.75f));
	        swinging = false;

	    
	    }

    
    }

    IEnumerator Delay (float delay){

        yield return new WaitForSeconds (delay);
	    
        CanShoot = true;
    }


    public void Hit (Transform zom, Transform hit, attacks type, Transform impact){
        GetComponent<AudioSource>().PlayOneShot(ImpactSounds[Random.Range(0, ImpactSounds.Length)]);

        if (hit)
	    {
		    if (HitMode == 0)
		    {
		        GetComponent<Animation>()[WeaponName + "_fire_01"].speed = -1;
                zom.GetComponent<zombie>().TakeHealth(Damage, impact, "__melee");

		    }
		    else
		    {
		        GetComponent<Animation>()[WeaponName + "_fire_02"].speed = -1.25f;
                zom.GetComponent<zombie>().TakeHealth(Damage*1.75f, impact, "__melee");
		    }
	    }
	    else
	    {
	        GetComponent<Animation>()[WeaponName + "_fire_01"].speed = -0.5f;
	        GetComponent<Animation>()[WeaponName + "_fire_02"].speed = -0.75f;
	        //GetComponent.<Animation>().Stop();
	    }
    }



}