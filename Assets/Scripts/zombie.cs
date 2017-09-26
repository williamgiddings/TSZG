using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class zombie : MonoBehaviour {

    public float Health;
    public int SightRange;
    public float AttackRange;
    public Dictionary<string, List<int>> AttackTypes = new Dictionary<string, List<int>> ()
    {
        {"zombie_lightAttack", new List<int>(){10, 5, 5, 7 } },
        {"zombie_heavyAttack" , new List<int>(){40, 10, 2, 4 } },
        {"zombie_lungeAttack" , new List<int>(){75, 30, 15, 25 } }

    }; // {<Attack Name> : [<Damage>, <Cooldown>, <Minimum Range>, <Maximum Range>]}

    private List<int> CanAttack = new List<int>() { 1, 1, 1 };
    public int AttackTime;
    public GameObject Player;
    private UnityEngine.AI.NavMeshAgent agent;
    private Vector3 PlayerDirection;
    private float DistanceToPlayer;
    public bool IsAlive = true;
    public AudioClip[] DeathSounds;
    public AudioClip[] AliveSounds;
    private bool  CanMakeSound = true;
    public Transform spawner;
    public int state; // 0 = idle, 1 = chasing, 2 = searching
    GameObject[] PatrolPoints;
    public float DefaultSpeed;
    public bool Patrolling;
    public Transform Anim;
    public AnimationClip IdleAnimation;
    public AnimationClip RunAnimation;
    public AnimationClip WalkAnimation;
    public AnimationClip SearchAnimation;
    public AnimationClip StaggerAnimation;
    public AnimationClip lightAttackAnimation;
    public AnimationClip DeathAnimation;
    public Component[] RagdollPivots;
    public Transform RagdollPrefab;
    public float LastHitForce;
    public Transform lastHitLimb;
    public bool Staggering;
    public bool  Attacking;



    void Start (){
        Player = GameObject.Find("player");
        agent = transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
        PatrolPoints = GameObject.FindGameObjectsWithTag("patrolPoints");
  
    }


    IEnumerator SoundMake (){
        CanMakeSound = false;
        GetComponent<AudioSource>().PlayOneShot(AliveSounds[Random.Range(0, AliveSounds.Length)]);
        yield return new WaitForSeconds(Random.Range(5, 15));
        CanMakeSound = true;
    }


    void FixedUpdate (){
        if (IsAlive)
        {    
        
            PlayerDirection = Player.transform.position - transform.position;
            DistanceToPlayer = Vector3.Distance (transform.position, Player.transform.position);
        
            if (Staggering || DistanceToPlayer <= 1.5f)
            {
                agent.velocity = Vector3.zero;
            }

        
            if (CanMakeSound)
            {
                SoundMake();
            }
           
        
            RaycastHit hit;
            if (Physics.Raycast(transform.position, PlayerDirection, out hit, SightRange))
            {
                if (hit.collider.gameObject.name == "player")
                {
                    NavigateTo(Player);
                
                }
                else
                {
                    if (state == 1)    
                    {
                        LastKnownLocation(agent.destination);
                    }
                    else if (state == 0 && Patrolling == false)
                    {
                        Patrol();
                    }
                }
            }
            if (Patrolling)
            {   
                if (state == 0 || state == 2)
                {
                    if (agent.remainingDistance <= 5)
                    {
                        Patrol();
                    }
                }
            }
       
        
        }
    }

    void Patrol (){

        Patrolling = true;
        state = 0;
        agent.speed = 0;
        agent.SetDestination(PatrolPoints[Random.Range(0, PatrolPoints.Length)].transform.position);
        agent.speed = DefaultSpeed * 0.3f;

    
    }

    void LastKnownLocation (Vector3 des){

        state = 2;
        agent.speed = DefaultSpeed * 0.75f; 
        agent.SetDestination(des);
    }

    void StopNav (){
        if (GetComponent<UnityEngine.AI.NavMeshAgent>().enabled == true)
        {
            state = 0;
            agent.velocity = Vector3.zero;
            //isMoving = false;
        }
    }

    public void NavigateTo (GameObject ply)
    {

        state = 1;
        agent.speed = DefaultSpeed * 1;
        agent.SetDestination(ply.transform.position);
    
        //isMoving = true;
    }



    public void TakeHealth (float damage, Transform limb, string type){
        if (type != "__shotgun")
        {
            LastHitForce = damage;
        }
        else
        {
            LastHitForce = (damage*8);
        }
    
        if (limb != null)
        {
            lastHitLimb = limb;
        }
        else
        {
            lastHitLimb =  transform.Find("Armature/body/head");
        }
    
        if (Health >= 1f)
        {
            Health -= damage;
        }
        if (damage > (Health/2f))
        {
            StartCoroutine(_stagger());
        }

    }

    IEnumerator _stagger ()
    {
        Staggering = true;
        Anim.GetComponent<Animation>().clip = StaggerAnimation;
        Anim.GetComponent<Animation>().Play(StaggerAnimation.name);
        yield return new WaitForSeconds(StaggerAnimation.length);
        Staggering = false;
    }

    void Update (){
        if (Health <= 0 && IsAlive)
        {
            Die();
        }

        UpdateAnimations();
    
    
    }

    void UpdateAnimations (){
        if (agent.velocity == Vector3.zero)
        {
            Anim.GetComponent<Animation>().clip = IdleAnimation;
        }
    
        if (IsAlive && !Staggering && !Attacking)
        {
            if (state == 0)
            {
                if (agent.velocity == Vector3.zero)
                {
                    Anim.GetComponent<Animation>().clip = IdleAnimation;
                }
                else
                {
                    Anim.GetComponent<Animation>().clip = WalkAnimation;
                }
            }
            else if (state == 1)
            {
                if (agent.velocity == Vector3.zero)
                {
                    Anim.GetComponent<Animation>().clip = IdleAnimation;
                }
                else
                {
                    Anim.GetComponent<Animation>().clip = RunAnimation;
                }
            }
            else if (state == 2)
            {
                if (agent.velocity == Vector3.zero)
                {
                    Anim.GetComponent<Animation>().clip = IdleAnimation;
                }
                else
                {
                    Anim.GetComponent<Animation>().clip = RunAnimation;
                }
            }
            if (!Anim.GetComponent<Animation>().IsPlaying(Anim.GetComponent<Animation>().clip.name))
            {
                Anim.GetComponent<Animation>().CrossFade(Anim.GetComponent<Animation>().clip.name);
            }
        }
    }


    void Die ()
{
        GetComponent<AudioSource>().PlayOneShot(DeathSounds[Random.Range(0, DeathSounds.Length)]);
        StopNav();
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        spawner.GetComponent<spawner>().Alive.Remove(transform);
        IsAlive = false;
        Transform Rag= Instantiate(RagdollPrefab, transform.position, transform.rotation);
    
        if (lastHitLimb.name != "body" && lastHitLimb.name != "lower_body")
        {
            Rag.Find("Armature/body/"+lastHitLimb.gameObject.name).GetComponent<Rigidbody>().AddForce(-PlayerDirection * ((LastHitForce / DistanceToPlayer )*3));
        }
        else
        {
            Rag.Find("Armature/"+lastHitLimb.gameObject.name).GetComponent<Rigidbody>().AddForce(-PlayerDirection * ((LastHitForce / DistanceToPlayer )*3));
        }
        Destroy(gameObject);
    }



}