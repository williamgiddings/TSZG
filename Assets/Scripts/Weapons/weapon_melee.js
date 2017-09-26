@Header ("Weapon Info")
var WeaponName : String;
var WeaponType : String;
var Firerate : float; // 1 / (60 / [firerate])
var Damage : float;
var ImpactSounds : AudioClip[];
var AbsoluteRange : float;
var MagSize : int;
var AmmoType : String;
var Melee : boolean = true;

@Header ("Components")
var swinging : boolean;
var Body : Transform;
private var CanShoot : boolean = true;
var Inventory;
private var target : GameObject; 
private var HitMode : int;


function Awake ()
{
    GetComponent.<Animation>().Play(WeaponName+"_draw");
}

function Update ()
{
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
    if (!GetComponent.<Animation>().IsPlaying)
    {
        GetComponent.<Animation>().CrossFade(WeaponName+"_idle");
    }
}

function Shoot (Mode)
{
    HitMode = Mode;
	
	if (Mode == 0)
    {
        GetComponent.<Animation>()[WeaponName + "_fire_01"].speed = 2;
        swinging = true;
        GetComponent.<Animation>().Play(WeaponName+"_fire_01");
        yield WaitForSeconds (GetComponent.<Animation>()[WeaponName + "_fire_01"].length/1.5);
        Delay(((1/(Firerate/60))-(GetComponent.<Animation>()[WeaponName + "_fire_01"].length/1.5))*1.2);
        swinging = false;
    }
    else
	{
	    GetComponent.<Animation>()[WeaponName + "_fire_02"].speed = 1.1;
	    swinging = true;
	    GetComponent.<Animation>().Play(WeaponName+"_fire_02");
	    yield WaitForSeconds (GetComponent.<Animation>()[WeaponName + "_fire_02"].length/1.25);
	    Delay(((1/(Firerate/60))-(GetComponent.<Animation>()[WeaponName + "_fire_02"].length/1.25))*1.75);
	    swinging = false;

	    
	}

    
}

function Delay (delay)
{
    yield WaitForSeconds (delay);
	    
    CanShoot = true;
}


function Hit (zombie, hit, type, impact)
{
    GetComponent.<AudioSource>().PlayOneShot(ImpactSounds[Random.Range(0, ImpactSounds.length)]);

    if (hit)
	{
		if (HitMode == 0)
		{
		    GetComponent.<Animation>()[WeaponName + "_fire_01"].speed = -1;
		    zombie.GetComponent("zombie").TakeHealth(Damage, impact, "__melee");

		}
		else
		{
		    GetComponent.<Animation>()[WeaponName + "_fire_02"].speed = -1.25;
		    zombie.GetComponent("zombie").TakeHealth(Damage*1.75, impact, "__melee");
		}
	}
	else
	{
	    GetComponent.<Animation>()[WeaponName + "_fire_01"].speed = -0.5;
	    GetComponent.<Animation>()[WeaponName + "_fire_02"].speed = -0.75;
	    //GetComponent.<Animation>().Stop();
	}
}


