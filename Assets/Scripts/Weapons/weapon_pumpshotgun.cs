using UnityEngine;
using System.Collections;

public class weapon_pumpshotgun : MonoBehaviour {
    string WeaponName;
    float Firerate; // 1 / (60 / [firerate])
    float BulletForce;
    AudioClip ShootSound;
    AudioClip DrawSound;
    float Effective;
    int AbsoluteRange;
    //AudioClip ReloadSound;
    bool  isPump;
    float PumpDelay;
    int Damage;
    float Recoil;
    float Spread;

    Transform Muzzle;
    private bool  CanShoot = true;
    Transform Plycamera;
    Transform BloodParticle;
    Transform ImpactParticle;
    Inventory Inventory;
    Transform HUD;
    private HUD HUDController;


    public void Draw (){
        HUDController = HUD.GetComponent<HUD>();
        GetComponent<Animation>().CrossFade(WeaponName+"_draw");
        GetComponent<AudioSource>().PlayOneShot(DrawSound);
        if (isPump)
        {
            Pump();
        }

    }


    void Pump (){
        GetComponent<Animation>().CrossFade(WeaponName+"_pump");
        GetComponent<AudioSource>().PlayOneShot(DrawSound);
    }

    void Idle (){
        GetComponent<Animation>().CrossFade(WeaponName+"_idle");
    }


    void Update (){
        if ( Input.GetMouseButton(0) && CanShoot == true )
        {
            Shoot();
            CanShoot = false;
	
        }
  
    }


    void Shoot ()
    {
        StartCoroutine(_shoot());
    }

    IEnumerator _shoot (){
        Ray ray;
        RaycastHit hit;
        Vector3 tempVec;
        for (int i = 0; i < 8; i++) 
        {
            tempVec = Vector3.Slerp(Camera.main.transform.forward, Random.onUnitSphere, Spread);
            ray = new Ray(Camera.main.transform.position, tempVec);
            if (Physics.Raycast(ray, out hit, AbsoluteRange))
            {
                Quaternion roto= Quaternion.FromToRotation(Vector3.up, hit.normal);
                if (hit.transform.tag == "zombie")
                {
                    Transform enemy = hit.collider.transform;
                    //enemy.GetComponent<zombie>().TakeHealth(Damage);
                    Instantiate(BloodParticle, hit.point, roto);
                }
                else
                {
	                Instantiate(ImpactParticle, hit.point, roto);
                }

                
            }

     
        }
    
        GetComponent<Animation>().Play(WeaponName+"_fire");
        ShootEffect();
        yield return new WaitForSeconds(PumpDelay);
        if (isPump)
        {
            Pump();
        }
        //yield WaitForSeconds (1/(Firerate/60));
        CanShoot = true;
        Idle();
    }


    void ShootEffect ()
    {
        GetComponent<AudioSource>().PlayOneShot(ShootSound);
        Muzzle.gameObject.SetActive(true);
        //yield return new WaitForSeconds(0.5f);
        Muzzle.gameObject.SetActive(false);

    }

}