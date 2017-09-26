using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour {
    public float MaxHealth;
    public int Weight; //Arbitrary max 10
    public int MoveSpeed;
    public AudioClip[] InjureSounds;
    public AudioClip[] BreathingSounds;

    public float Health;
    public float Stamina = 100.0f;
    public float Hunger = 100.0f;
    public bool Sprinting;
    public CharacterMotor Character;
    public Image HealthBar;
    public Image StaminaBar;
    public Image Heart;
    public Transform gui;


    void Start (){
        Health = MaxHealth;
        Character = GetComponent<CharacterMotor>();
    }

    
    void Update (){
	    if (Input.GetKey(KeyCode.LeftShift))
	    {
	        Sprint();
	    }
	    else
	    {
		    Sprinting = false;
	    }
	    if (Sprinting)
	    {
	        Character.movement.maxForwardSpeed = (MoveSpeed * 1.5f);
	    }
	    else
	    {
	        Character.movement.maxForwardSpeed = MoveSpeed;
            RegenStamina();
        }
	    HealthBar.fillAmount = Health / 100;
	    StaminaBar.fillAmount = Stamina / 100;
	    StartCoroutine(AddHunger());
        

    }

    IEnumerator AddHunger (){
        yield return new WaitForSeconds (0.24f);
        DrainHunger(0.05f, 0);
    }

    void RegenStamina ()
    {
        float time = 0f;

        while (time > 5f)
        {
            Stamina = Mathf.Lerp(Stamina, 100f, (time / 5f));
            time += Time.deltaTime;
        }
    }

    void Sprint ()
    {
        StartCoroutine(_sprint());
    }

    IEnumerator _sprint (){
	    if (Stamina > 0f)
	    {	
		    yield return new WaitForSeconds((0.1f)/Weight);
		    DrainStamina(5, 0);
		    Sprinting = true;
	    }
        else
        {
            Sprinting = false;
	    }


	
    }

    public void TakeHealth (float dmg, int Flag){

        Heart.GetComponent<Animation>().Play("UIHeartBeat");

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

    public void DrainHunger (float h, int Flag){
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

    public void DrainStamina (float Stm, int Flag){
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
}