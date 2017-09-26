@Header ("Weapon Stats")
var WeaponName : String;
var Firerate : float; // 1 / (60 / [firerate])
var Damage : float;
var ShootSound : AudioClip;
var DrawSound : AudioClip;
var DryFireSound : AudioClip;
var EffectiveRange : float;
var AbsoluteRange : float;
var clip : int;
var MagSize : int;
var MagAnimDelay : float;
var AmmoType : String;

@Header ("Components")
var BloodParticle : Transform;
var ImpactParticle : Transform;
var Muzzle : Transform;
var MuzzleSmoke : Transform;
private var CanShoot : boolean = true;
var Inventory : Component;
var Plycamera : Transform;
var Recoil : float;
var RecoilSpeed : float;
var RecoverSpeed : float;
var RecoilController : Transform;
var Player : Transform;
private var Spread : float;
var defSpread : float;
var IronSpread : float;
var HUDController : Component;
var CanIronsight : boolean;
var IronSightAmount : float;
private var Sway : MonoBehaviour;

function Start ()
{
    RecoilController = GameObject.Find("recoilController").transform;
    Plycamera = GameObject.Find("Main Camera").transform;
    HUDController = GameObject.Find("GUI").GetComponent("HUD");
    Sway = GetComponent("sway");
    Spread = defSpread;
    
}

function Draw ()
{ 
    Player = GetComponent("WeaponInfo").Player;
    Inventory = Player.GetComponent("Inventory");
    HUDController = GameObject.Find("GUI").GetComponent("HUD");
    CanIronsight = true;
    

}

function Update ()
{
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
    if (HUDController != null)
    {
        HUDController.Clip.text = clip.ToString();
    }
}

function IronSight ()
{
    if (CanIronsight)
    {
        if (!Sway.isIron)
        {
            Player.GetComponent("player").Character.movement.maxForwardSpeed = (Player.GetComponent("player").MoveSpeed * 0.25);
            Sway.isIron = true;
            Plycamera.GetComponent.<Camera>().fieldOfView = Mathf.Lerp(65, IronSightAmount, Time.time * 0.1);
            Spread = IronSpread;
        }
        else
        {
            Player.GetComponent("player").Character.movement.maxForwardSpeed = Player.GetComponent("player").MoveSpeed;
            Sway.isIron = false;
            Plycamera.GetComponent.<Camera>().fieldOfView = Mathf.Lerp(IronSightAmount, 65, Time.time * 0.2);
            Spread = defSpread;
        }
    }
    CanIronsight = false;
    yield WaitForSeconds(0.75);
    CanIronsight = true;

}

function Shoot ()
{
	
    if (clip > 0)
	{	
        GetComponent.<Animation>().Stop();
        CanShoot = false;
        
        var hit : RaycastHit;
        tempVec = Vector3.Slerp(Camera.main.transform.forward, Random.onUnitSphere, Spread);
        ray = new Ray(Camera.main.transform.position, tempVec);
        var playerLayer = 1 << 9 | 1 << 2;
        playerLayer = ~playerLayer;
        
        if (Physics.Raycast(ray, hit, AbsoluteRange, playerLayer))
        {
            var roto = Quaternion.FromToRotation(Vector3.up, hit.normal);
           
            if (hit.transform.tag == "zombie")
            {
                enemy = hit.transform;          
                enemy.GetComponent("zombie").TakeHealth(CalculateDamage(Damage, hit.distance), hit.collider.transform, "__regular");
                Instantiate(BloodParticle, hit.point, roto);
                
            }
            else
            {
                Instantiate(ImpactParticle, hit.point, roto);
            }
	        
	    }
	    ShootEffect();
	    clip -= 1;


	    GetComponent.<Animation>().Play(WeaponName+"_fire");
	    yield WaitForSeconds (1/(Firerate/60));


	    CanShoot = true;
	}
	else
	{
		GetComponent.<AudioSource>().PlayOneShot(DryFireSound);
	}
}

function Reload ()
{
    if (GetComponent("WeaponInfo").WeaponType != "Special")
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
                var requiredBullets = MagSize - clip;
		
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

function LoadGun (ammo)
{
	GetComponent.<Animation>().Play(WeaponName+"_reload");
	yield WaitForSeconds (MagAnimDelay);
	clip += ammo;
	CanShoot = true;
}

function CalculateDamage (dmg, shotDistance)
{
	if (shotDistance > EffectiveRange)
	{
		var FallOff = shotDistance - EffectiveRange;
		var FallOffPerCent =  FallOff / AbsoluteRange;
		return (Damage -(dmg * FallOffPerCent));	
	}
	else
	{
		return Damage;
	}
}

function ShootEffect ()
{
    RecoilController.GetComponent("recoil").recoiling(Recoil, RecoilSpeed, RecoverSpeed);
    RecoilController.GetComponent("recoil").recoil += Recoil;
    GetComponent.<AudioSource>().PlayOneShot(ShootSound);
    Muzzle.transform.Rotate (0,0, Random.Range(30, 180), Space.Self);
    
    if (MuzzleSmoke != null)
    {
        MuzzleSmoke.gameObject.SetActive(true);
        Muzzle.gameObject.SetActive(true);
        yield WaitForSeconds(0.15);
        Muzzle.gameObject.SetActive(false);
        MuzzleSmoke.gameObject.SetActive(false);

    }
    else
    {
        Muzzle.gameObject.SetActive(true);
        yield WaitForSeconds(0.15);
        Muzzle.gameObject.SetActive(false);
    }
    


}
