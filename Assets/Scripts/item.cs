using UnityEngine;
using System.Collections;

public class item : MonoBehaviour {
    public enum TypeItem {food, ammo, weapon}
    public enum TypeAmmo {ammo_rifle, ammo_pistol, ammo_smg, ammo_shotgun, ammo_grenade, ammo_caredrop, ammo_sniper, ammo_rocket}
    public int AmmoAmount;
    public string ItemName;
    public TypeItem ItemType;
    public int AddHealth;
    public int AddStamina;
    public int AddHunger;
    public TypeAmmo AmmoType;
    public Transform Weapon;
    public AudioClip InteractSound;

    public void Interact (Transform ply){
        player PlayerHealth = ply.GetComponent<player>();
        Inventory PlayerInventory = ply.GetComponent<Inventory>();
	
	    GetComponent<AudioSource>().PlayOneShot(InteractSound);

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

    void FoodFunction (player Health){
	    Health.DrainStamina(-AddStamina,1);
	    Health.DrainHunger(-AddHunger,1);
	    Health.TakeHealth(-AddHealth,1);
	    transform.gameObject.tag = "Untagged";
	    transform.GetComponent<MeshRenderer>().enabled = false;
	    Destroy(gameObject, InteractSound.length);
    }

    void AmmoFunction (Inventory inv){
        inv.Ammo[AmmoType.ToString()] += AmmoAmount;
        transform.gameObject.tag = "Untagged";
        transform.GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject, InteractSound.length);
    }

    void WeaponFunction (Inventory WeaponInventory){
	    WeaponInventory.SwitchWeaponTo(Weapon, transform.position, transform.rotation); //new weapon, item pos, item rot.
	    Destroy(gameObject);
    }
}