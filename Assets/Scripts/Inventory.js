var Ammo = {
    "ammo_pistol" : 0,
    "ammo_smg" : 0,
    "ammo_rifle" : 0,
    "ammo_shotgun" : 0,
    "ammo_sniper" : 0,
    "ammo_grenade" : 0,
    "ammo_rocket" : 0,
    "ammo_caredrop" : 0
}; //Dictionary which stores the types of ammo the user has and how much of it they have.

var PrimaryWeapon : String; //The user's main weapon
var CurrentWeapon : Transform;
var SecondaryWeapon : String; //Backup weapon
private var AmmoReserve : String;

var camer : Transform;

private var PistolMode : boolean;
private var CanInteract : boolean;
var TargetObject : Transform;
var PickupGUI : UI.Text;
var HUD : Transform;
private var HUDController;
var Hand : Transform;
var oldItem : Transform;
var reticle : GUITexture;
var handReticle : GUITexture;


function Start ()
{
    HUDController = HUD.GetComponent("HUD");
}

function Update ()
{
    if (Input.GetButtonDown("Interact"))
    {
        if (CanInteract && TargetObject != null)
        {
            TargetObject.GetComponent(TargetObject.name).Interact(transform);
        }
    }
    if (AmmoReserve != null)
    {
        if (AmmoReserve == "SPECIAL")
        {
            HUDController.Reserve.text = "-";
        }
        else
        {
            HUDController.Reserve.text = Ammo[AmmoReserve].ToString();
        }
        
    }
    else
    {
        HUDController.Reserve.text = "";
    }


	
}

function SwitchWeaponTo (wp, pos, rot)
{
    var WeaponInf = wp.GetComponent("WeaponInfo");
    if (WeaponInf.WeaponType == "Primary" || WeaponInf.WeaponType == "Special" )
    {
        if (PrimaryWeapon != "")
        {
            oldItem = CurrentWeapon.GetComponent("WeaponInfo").w_Model;
            PrimaryWeapon = null;
            var oldGun = new Instantiate(oldItem, pos, rot);
            oldGun.name = "item";
            Destroy(CurrentWeapon.gameObject);
            
        }

        PrimaryWeapon = wp.name;
        WeaponInf.Player = transform;
        var newGun = new Instantiate(wp, Hand);
        newGun.localPosition = WeaponInf.DefaultPosition;
        newGun.localRotation = new Quaternion.Euler(WeaponInf.DefaultRotation.x, WeaponInf.DefaultRotation.y, WeaponInf.DefaultRotation.z);
        newGun.name = WeaponInf.WeaponName;
        CurrentWeapon = newGun;
        WeaponInf.Draw();
        AmmoReserve = WeaponInf.AmmoType;
        newGun.GetComponent(Animation).Play(WeaponInf.WeaponName+"_draw");        
    }
}

function FixedUpdate ()
{
    var RayHit : RaycastHit;
	
	if (Physics.Raycast(camer.transform.position, camer.forward,RayHit , 3))
	{
	    if (RayHit.collider.tag == "interactable")
	    {
	        TargetObject = RayHit.collider.transform;
	        CanInteract = true;
	        reticle.gameObject.SetActive(false);
	        handReticle.gameObject.SetActive(true);
	    }
	    else
	    {
	        TargetObject = null;
	        CanInteract = false;
	        reticle.gameObject.SetActive(true);
	        handReticle.gameObject.SetActive(false);
	    }
	}
	else
	{
	    TargetObject = null;
	    CanInteract = false;
	    reticle.gameObject.SetActive(true);
	    handReticle.gameObject.SetActive(false);
	}
	
}


