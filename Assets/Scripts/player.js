var MaxHealth : float;
var Weight : int; //Arbitrary max 10
var MoveSpeed : int;
var InjureSounds : AudioClip[];
var BreathingSounds : AudioClip[];

var Health : float;
var Stamina : float = 100.0;
var Hunger : float = 100.0;
var Sprinting : boolean;
var Character;
var HealthBar : Transform;
var StaminaBar : Transform;
var gui : Transform;

function Start ()
{
    Health = MaxHealth;
    Character = GetComponent.<CharacterMotor>();
}


function Update ()
{
	if (Input.GetKey(KeyCode.LeftShift))
	{
	    Sprint();
	}
	else
	{
		Sprinting = false;
	}
	if (Sprinting == true)
	{
	    Character.movement.maxForwardSpeed = (MoveSpeed * 1.5);
	}
	else
	{
	    Character.movement.maxForwardSpeed = MoveSpeed;
	}
	HealthBar.GetComponent.<UI.Image>().fillAmount = Health / 100;
	StaminaBar.GetComponent.<UI.Image>().fillAmount = Stamina / 100;
	AddHunger();

}

function AddHunger()
{
    yield WaitForSeconds (0.24);
    DrainHunger(0.05, 0);
}

function Sprint ()
{
	if (Stamina > 0f)
	{	
		yield WaitForSeconds((0.1)/Weight);
		DrainStamina(5, 0);
		Sprinting = true;
	}
    else
    {
        Sprinting = false;
	}


	
}

function TakeHealth (dmg, Flag)
{
	if (Flag == 0) //Take
	{
	    if ((Health - dmg) < 1f)
	    {
	        Health = 0;
	    }
		else
		{
			Health -= dmg;
		}
	}
	else //Add
	{
		if ((Health - dmg) > MaxHealth)
		{
			Health = MaxHealth;
		}
		else
		{
			Health -= dmg;
		}
	}
}

function DrainHunger (h, Flag)
{
	if (Flag == 0) //Reduce
	{
		if ((Hunger - h)<1f)
		{
			Hunger = 0f;
		}
		else
		{
			Hunger -= h * Time.deltaTime;
		}
	}
	else
	{
		if ((Hunger - h)>100f)
		{
			Hunger = 100f;
		}
		else
		{
			Hunger -= h;
		}
	}
}

function DrainStamina (Stm, Flag)
{
	if (Flag == 0)
	{
		if ((Stamina - Stm) < 1f)
		{
			Stamina = 0f;
		}
		else
		{
			Stamina -= Stm * Time.deltaTime;
		}
	}
	else
	{
		if ((Stamina - Stm) > 100f)
		{
			Stamina = 100f;
		}
		else
		{
			Stamina -= Stm;
		}
	}
}