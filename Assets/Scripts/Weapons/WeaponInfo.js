enum WeaponFamily {weapon_Base, weapon_pumpshotgun, weapon_shotgun, weapon_melee}
var WeaponName : String;
var WeaponType : String;
var AmmoType : String;
var w_Model : Transform;
var DefaultPosition : Vector3;
var DefaultRotation : Vector3;
var Player : Transform;
var FamilyType : WeaponFamily;


function Draw ()
{

    switch (FamilyType)
    {
        case WeaponFamily.weapon_Base:
        GetComponent("weapon_Base").Draw();
        break;

        case WeaponFamily.weapon_pumpshotgun:
        GetComponent("weapon_pumpshotgun").Draw();
        break;

        case WeaponFamily.weapon_shotgun:
        GetComponent("weapon_shotgun").Draw();
        break;

        case WeaponFamily.weapon_melee:
        GetComponent("weapon_melee").Draw();
        break;

    }
    
}