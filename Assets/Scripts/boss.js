var Health : int;
var DisplayName : String;
var Attacks = {"light_attack" : [50, 5], "heavy_attack" : [15, 20]}; //{<name : [<dps>, rechargeTime]}  
private var Attack : String;
var doneIntro : boolean = false;
var updatingRotation : boolean;
private var Player : Transform;
var lookSpeed : float;

function Start ()
{
	updatingRotation = false; //Sets bool to stop the boss from looking at the player
	Player = GameObject.Find("Player").transform; //Find the player object in the scene and assign it to a variable.
	//GetComponent.<Animation>().clip = "intro"; //Sets the current animation to the boss's intro.
	yield WaitForSeconds (GetComponent.<Animation>().clip.length); // Waits until the animation is done
	doneIntro = true;
	updatingRotation = true;

}

function Update ()
{
	if (updatingRotation)
	{
		UpdateRotation();
	}
}

function UpdateRotation ()
{
	var lookPos = Player.position - transform.position; //Gets the difference between the player's position and the boss's position.
	var rotation = Quaternion.LookRotation(lookPos); //Creates rotation at position on th Y axis only.
	transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * lookSpeed); // Looks at rotation.
}

