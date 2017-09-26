using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public Dictionary<string, int> Ammo = new Dictionary<string, int>()
    {
        { "ammo_pistol", 0},
        { "ammo_smg", 0 },
        {"ammo_rifle", 0},
        {"ammo_shotgun", 0},
        {"ammo_sniper", 0},
        {"ammo_grenade", 0},
        {"ammo_rocket", 0},
        {"ammo_caredrop", 0}
    }; //Dictionary which stores the types of ammo the user has and how much of it they have.

    public string PrimaryWeapon; //The user's main weapon
    public Transform CurrentWeapon;
    public string SecondaryWeapon; //Backup weapon
    private string AmmoReserve;

    public Transform camer;

    private bool  PistolMode;
    private bool  CanInteract;
    public Transform TargetObject;
    public Text PickupGUI;
    public Transform HUD;
    private HUD HUDController;
    public Transform Hand;
    public Transform oldItem;
    public GUITexture reticle;
    public GUITexture handReticle;


    void Start (){
        HUDController = HUD.GetComponent<HUD>();
    }

    void Update (){
        if (Input.GetButtonDown("Interact"))
        {
            if (CanInteract && TargetObject != null)
            {
                TargetObject.GetComponent<item>().Interact(transform);
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

    public void SwitchWeaponTo (Transform wp, Vector3 pos, Quaternion rot){
        WeaponInfo WeaponInf = wp.GetComponent<WeaponInfo>();
        if (WeaponInf.WeaponType == "Primary" || WeaponInf.WeaponType == "Special" )
        {
            if (PrimaryWeapon != "")
            {
                oldItem = CurrentWeapon.GetComponent<WeaponInfo>().w_Model;
                PrimaryWeapon = null;
                Transform oldGun= Instantiate(oldItem, pos, rot);
                oldGun.name = "item";
                Destroy(CurrentWeapon.gameObject);
            
            }

            PrimaryWeapon = wp.name;
            WeaponInf.Player = transform;
            Transform newGun =  Instantiate(wp, Hand);
            newGun.localPosition = WeaponInf.DefaultPosition;
            newGun.localRotation = Quaternion.Euler(WeaponInf.DefaultRotation.x, WeaponInf.DefaultRotation.y, WeaponInf.DefaultRotation.z);
            newGun.name = WeaponInf.WeaponName;
            CurrentWeapon = newGun;
            WeaponInf.Draw();
            AmmoReserve = WeaponInf.AmmoType;
            newGun.GetComponent<Animation>().Play(WeaponInf.WeaponName+"_draw");        
        }
    }

    void FixedUpdate (){
        RaycastHit RayHit;
	
	    if (Physics.Raycast(camer.transform.position, camer.forward, out RayHit , 3))
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



}