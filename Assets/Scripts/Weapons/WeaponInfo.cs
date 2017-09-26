using UnityEngine;
using System.Collections;

public class WeaponInfo : MonoBehaviour {
    public enum WeaponFamily {weapon_Base, weapon_pumpshotgun, weapon_shotgun, weapon_melee}
    public string WeaponName;
    public string WeaponType;
    public string AmmoType;
    public Transform w_Model;
    public Vector3 DefaultPosition;
    public Vector3 DefaultRotation;
    public Transform Player;
    public WeaponFamily FamilyType;


    public void Draw (){

        switch (FamilyType)
        {
            case WeaponFamily.weapon_Base:
            GetComponent<weapon_Base>().Draw();
            break;

            case WeaponFamily.weapon_pumpshotgun:
            GetComponent<weapon_pumpshotgun>().Draw();
            break;

            case WeaponFamily.weapon_shotgun:
            GetComponent<weapon_shotgun>().Draw();
            break;

            case WeaponFamily.weapon_melee:
            //GetComponent<weapon_melee>().Draw();
            break;

        }
    
    }
}