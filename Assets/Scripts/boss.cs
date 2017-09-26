using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class boss : MonoBehaviour {
    int Health;
    string DisplayName;
    Dictionary<string, List<int>> Attacks =new Dictionary<string, List<int>>
    {
        {"light_attack", new List<int>(){50, 5 } },
        {"heavy_attack", new List<int>(){15, 20 } }

    }; //{<name : [<dps>, rechargeTime]}  


    private string Attack;
    bool  doneIntro = false;
    bool  updatingRotation;
    private Transform Player;
    float lookSpeed;

    void Start (){
	    updatingRotation = false; //Sets bool to stop the boss from looking at the player
	    Player = GameObject.Find("Player").transform; //Find the player object in the scene and assign it to a variable.
        StartCoroutine(_doIntro());

    }

    IEnumerator _doIntro()
    {
        //GetComponent.<Animation>().clip = "intro"; //Sets the current animation to the boss's intro.
        yield return new WaitForSeconds(GetComponent<Animation>().clip.length); // Waits until the animation is done
        doneIntro = true;
        updatingRotation = true;
    }

    void Update (){
	    if (updatingRotation)
	    {
		    UpdateRotation();
	    }
    }

    void UpdateRotation (){
	    Vector3 lookPos= Player.position - transform.position; //Gets the difference between the player's position and the boss's position.
	    Quaternion rotation= Quaternion.LookRotation(lookPos); //Creates rotation at position on th Y axis only.
	    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * lookSpeed); // Looks at rotation.
    }


}