enum TypeItem {food, ammo, weapon}
enum TypeAmmo {ammo_rifle, ammo_pistol, ammo_smg, ammo_shotgun, ammo_grenade, ammo_caredrop, ammo_sniper, ammo_rocket}
var AmmoAmount : int;
var ItemName : String;
var ItemType : TypeItem;
var AddHealth : int;
var AddStamina : int;
var AddHunger : int;
var AmmoType : TypeAmmo;
var Weapon : Transform;
var InteractSound : AudioClip;

function Interact (ply)
{
	var PlayerHealth = ply.GetComponent("player");
	var PlayerInventory = ply.GetComponent("Inventory");
	
	GetComponent.<AudioSource>().PlayOneShot(InteractSound);

	switch (ItemType)
	{
		case TypeItem.food:
		FoodFunction(PlayerHealth);
		break;
		
		case TypeItem.ammo:
		AmmoFunction(PlayerInventory);
		break;
		
		case TypeItem.weapon:
		WeaponFunction(PlayerInventory);
		break;
	}

	
}

function FoodFunction (Health)
{
	Health.DrainStamina(-AddStamina,1);
	Health.DrainHunger(-AddHunger,1);
	Health.TakeHealth(-AddHealth,1);
	transform.gameObject.tag = "Untagged";
	transform.GetComponent.<MeshRenderer>().enabled = false;
	yield WaitForSeconds (InteractSound.length);
	Destroy(gameObject);
}

function AmmoFunction(Inventory)
{
    Inventory.Ammo[AmmoType.ToString()] += AmmoAmount;
    transform.gameObject.tag = "Untagged";
    transform.GetComponent.<MeshRenderer>().enabled = false;
    yield WaitForSeconds (InteractSound.length);
    Destroy(gameObject);
}

function WeaponFunction(WeaponInventory)
{
	WeaponInventory.SwitchWeaponTo(Weapon, transform.position, transform.rotation); //new weapon, item pos, item rot.
	Destroy(gameObject);
}