var WeaponName : String;
var Firerate : float; // 1 / (60 / [firerate])
var BulletForce : float;
var ShootSound : AudioClip;
var DrawSound : AudioClip;
var Effective : float;
var AbsoluteRange : int;
//var ReloadSound : AudioClip;
var isPump : boolean;
var PumpDelay : float;
var Damage : int;
var Recoil : float;
var Spread : float;

var Muzzle : Transform;
private var CanShoot : boolean = true;
var Plycamera : Transform;
var BloodParticle : Transform;
var ImpactParticle : Transform;
var Inventory;
var HUD : Transform;
private var HUDController;


function Draw ()
{
    HUDController = HUD.GetComponent("HUD");
    GetComponent.<Animation>().CrossFade(WeaponName+"_draw");
    GetComponent.<AudioSource>().PlayOneShot(DrawSound);
    if (isPump)
    {
        Pump();
    }

}


function Pump ()
{
    GetComponent.<Animation>().CrossFade(WeaponName+"_pump");
    GetComponent.<AudioSource>().PlayOneShot(DrawSound);
}

function Idle ()
{
    GetComponent.<Animation>().CrossFade(WeaponName+"_idle");
}


function Update ()
{
    if ( Input.GetMouseButton(0) && CanShoot == true )
    {
        Shoot();
        CanShoot = false;
	
    }
  
}

function Shoot ()
{
    var ray : Ray;
    var hit : RaycastHit;
    var tempVec : Vector3;
    for (var i : int = 0; i < 8; i++) 
    {
        tempVec = Vector3.Slerp(Camera.main.transform.forward, Random.onUnitSphere, Spread);
        ray = new Ray(Camera.main.transform.position, tempVec);
        if (Physics.Raycast(ray, hit, AbsoluteRange))
        {
            var roto = Quaternion.FromToRotation(Vector3.up, hit.normal);
            if (hit.transform.tag == "zombie")
            {
                enemy = hit.collider.transform;          
                enemy.GetComponent("zombie").TakeHealth(Damage);
                Instantiate(BloodParticle, hit.point, roto);
            }
            else
            {
	            Instantiate(ImpactParticle, hit.point, roto);
            }

                
        }

     
    }
    
   
    GetComponent.<Animation>().Play(WeaponName+"_fire");
    ShootEffect();
    yield WaitForSeconds(PumpDelay);
    if (isPump)
    {
        Pump();
    }
    yield WaitForSeconds (1/(Firerate/60));
    CanShoot = true;
    Idle();
}

function ShootEffect ()
{
    GetComponent.<AudioSource>().PlayOneShot(ShootSound);
    Muzzle.gameObject.SetActive(true);
    yield WaitForSeconds(0.5);
    Muzzle.gameObject.SetActive(false);

}
