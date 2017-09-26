using UnityEngine;
using System.Collections;

public class weapon_shotgun : MonoBehaviour
{
    [Header ("Weapon Stats")]
    public string WeaponName;
    public float Firerate; // 1 / (60 / [firerate])
    public float Damage;
    public AudioClip ShootSound;
    public AudioClip DrawSound;
    public AudioClip DryFireSound;
    public float EffectiveRange;
    public float AbsoluteRange;
    int clip;
    public int MagSize;
    public float MagAnimDelay;
    public string AmmoType;

    [Header ("Components")]
    public Transform BloodParticle;
    public Transform ImpactParticle;
    public Transform Muzzle;
    public Transform MuzzleSmoke;
    private bool  CanShoot = true;
    public Inventory Inventory;
    public Transform Plycamera;
    public float RecoilX;
    public float RecoilY;
    public Transform RecoilController;
    public Transform Player;
    public HUD HUDController;
    private float Spread;
    public float defSpread;
    public float IronSpread;
    private sway Sway;
    public bool CanIronsight;
    public float IronSightAmount;

    void Start (){
        RecoilController = GameObject.Find("recoilController").transform;
        Plycamera = GameObject.Find("Main Camera").transform;
        HUDController = GameObject.Find("GUI").GetComponent<HUD>();
        Sway = GetComponent<sway>();
        Spread = defSpread;
    
    
    }

    public void Draw (){
        Player = GetComponent<WeaponInfo>().Player;
        Inventory = Player.GetComponent<Inventory>();
        HUDController = GameObject.Find("GUI").GetComponent<HUD>();
        CanIronsight = true;


    }


    void Update (){
        if ( Input.GetButton("Fire") && CanShoot == true )
        {
            Shoot();
	
        }
        if (Input.GetAxis("Trigger") == 1 && CanShoot == true)
        {
            Shoot();
        }
        if (Input.GetButtonDown("Fire2") && CanShoot == true )
        {
            IronSight();
        }
        if (Input.GetAxis("Trigger") == -1 && CanShoot == true)
        {
            IronSight();
        }
	    if (Input.GetButtonDown("Reload") && CanShoot == true)
	    {
	        Reload();
	    }
        if (GetComponent<Animation>().clip == null)
        {
            GetComponent<Animation>().CrossFade(WeaponName+"_idle");
        }

        if (HUDController != null)
        {
            HUDController.Clip.text = clip.ToString();
        }
    }

    void IronSight (){
        if (CanIronsight)
        {
            if (!Sway.isIron)
            {
                Player.GetComponent<player>().Character.movement.maxForwardSpeed = (Player.GetComponent<player>().MoveSpeed * 0.25f);
                Sway.isIron = true;
                Plycamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(65, IronSightAmount, Time.time * 0.1f);
                Spread = IronSpread;
            }
            else
            {
                Player.GetComponent<player>().Character.movement.maxForwardSpeed = Player.GetComponent<player>().MoveSpeed;
                Sway.isIron = false;
                Plycamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(IronSightAmount, 65, Time.time * 0.2f);
                Spread = defSpread;
            }
        }
        StartCoroutine(_ironCooldown());

    }

    IEnumerator _ironCooldown ()
    {
        CanIronsight = false;
        yield return new WaitForSeconds(0.75f);
        CanIronsight = true;
    }


    void Shoot (){
        if (clip > 0)
        {	
            CanShoot = false;
            Ray ray;
            RaycastHit hit;
            Vector3 tempVec;
            int hitcount;
        
            for (int i = 0; i < 8; i++) 
            {
                tempVec = Vector3.Slerp(Camera.main.transform.forward, Random.onUnitSphere, Spread);
                ray = new Ray(Camera.main.transform.position, tempVec);
                LayerMask playerLayer= 1 << 9 | 1 << 2;
                playerLayer = ~playerLayer;
                if (Physics.Raycast(ray, out hit, AbsoluteRange, playerLayer))
                {
                    Quaternion roto= Quaternion.FromToRotation(Vector3.up, hit.normal);
                
                    if (hit.transform.tag == "zombie")
                    {
                        Transform enemy = hit.transform;          
                        enemy.GetComponent<zombie>().TakeHealth(CalculateDamage(Damage, hit.distance), hit.collider.transform, "__shotgun");
                        Instantiate(BloodParticle, hit.point, roto);
                
                    }
                    else
                    {
                        Instantiate(ImpactParticle, hit.point, roto);
                    }
            }
    }
	        ShootEffect();
	        clip -= 1;


	        GetComponent<Animation>().Play(WeaponName+"_fire");

            StartCoroutine(_shotCooldown());
	    }
	    else
	    {
		    GetComponent<AudioSource>().PlayOneShot(DryFireSound);
	    }
    }

    IEnumerator _shotCooldown()
    {
        yield return new WaitForSeconds(1 / (Firerate / 60));

        GetComponent<Animation>().Stop();

        CanShoot = true;
    }

    void Reload (){
	    if (GetComponent<WeaponInfo>().WeaponType != "Special")
	    {
	        CanShoot = false;
	        if (Sway.isIron)
	        {
	            IronSight();
	        }
	        if (Inventory != null)
	        {
	            if (Inventory.Ammo[AmmoType] > 0)
	            {
	                int requiredBullets= MagSize - clip;
		
	                if (Inventory.Ammo[AmmoType] > requiredBullets)
	                {
	                    LoadGun(requiredBullets);
	                    Inventory.Ammo[AmmoType] -= requiredBullets;
	                }
	                else
	                {
	                    LoadGun(Inventory.Ammo[AmmoType]);
	                    Inventory.Ammo[AmmoType] = 0;
	                }
	            }
	            else
	            {
	                CanShoot = true;
	            }
	        }
	        else
	        {
	            Draw();
	            Reload();
	        }
	    }
    }

    void LoadGun (int ammo)
    {
        StartCoroutine(_loadGun(ammo));
    }

    IEnumerator _loadGun (int ammo){
	    GetComponent<Animation>().Play(WeaponName+"_reload");
	    yield return new WaitForSeconds (MagAnimDelay);
	    clip += ammo;
	    CanShoot = true;
    }

    float CalculateDamage (float dmg, float shotDistance){
	    if (shotDistance > EffectiveRange)
	    {
		    float FallOff= shotDistance - EffectiveRange;
		    float FallOffPerCent=  FallOff / AbsoluteRange;
		    return (Damage -(dmg * FallOffPerCent));	
	    }
	    else
	    {
		    return Damage;
	    }
    }

    void ShootEffect()
    {
        StartCoroutine(_shootEffect());
    }

    IEnumerator _shootEffect ()
    {
        //RecoilController.GetComponent<recoil>().recoiling(RecoilX,RecoilY);
        GetComponent<AudioSource>().PlayOneShot(ShootSound);
        Muzzle.transform.Rotate (0,0, Random.Range(30, 180), Space.Self);
        Muzzle.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        Muzzle.gameObject.SetActive(false);

    }

}