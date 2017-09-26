var Health : int;
var SightRange : int;
var AttackRange : float;
var AttackTypes = {"zombie_lightAttack" : [10, 5, 5, 7], "zombie_heavyAttack" : [40, 10, 2, 4], "zombie_lungeAttack" : [75, 30, 15, 25]}; // {<Attack Name> : [<Damage>, <Cooldown>, <Minimum Range>, <Maximum Range>]}
private var CanAttack = [1,1,1];
var AttackTime : int;
var Player : GameObject;
private var agent : UnityEngine.AI.NavMeshAgent;
private var PlayerDirection : Vector3;
private var DistanceToPlayer;
var IsAlive : boolean = true;
var DeathSounds : AudioClip[];
var AliveSounds : AudioClip[];
private var CanMakeSound : boolean = true;
var spawner : Transform;
var state : int; // 0 = idle, 1 = chasing, 2 = searching
var PatrolPoints : GameObject[];
var DefaultSpeed : float;
var Patrolling : boolean;
var Anim : Transform;
var IdleAnimation : AnimationClip;
var RunAnimation : AnimationClip;
var WalkAnimation : AnimationClip;
var SearchAnimation : AnimationClip;
var StaggerAnimation : AnimationClip; 
var lightAttackAnimation : AnimationClip;
var DeathAnimation : AnimationClip;
var RagdollPivots : Component[];
var RagdollPrefab : Transform;
var LastHitForce : float;
var lastHitLimb : Transform;
var Staggering : boolean;
var Attacking : boolean;



function Start ()
{
    Player = GameObject.Find("player");
    agent = transform.GetComponent.<UnityEngine.AI.NavMeshAgent>();
    PatrolPoints = GameObject.FindGameObjectsWithTag("patrolPoints");
  
}


function SoundMake ()
{
    CanMakeSound = false;
    GetComponent.<AudioSource>().PlayOneShot(AliveSounds[Random.Range(0, AliveSounds.length)]);
    yield WaitForSeconds(Random.Range(5, 15));
    CanMakeSound = true;
}


function FixedUpdate ()
{
    if (IsAlive)
    {    
        
        PlayerDirection = Player.transform.position - transform.position;
        DistanceToPlayer = Vector3.Distance (transform.position, Player.transform.position);
        
        if (Staggering || DistanceToPlayer <= 1.5)
        {
            agent.velocity = Vector3.zero;
        }

        
        if (CanMakeSound)
        {
            SoundMake();
        }
           
        
        var hit : RaycastHit;
        if (Physics.Raycast(transform.position, PlayerDirection, hit, SightRange))
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

function Patrol ()
{

    Patrolling = true;
    state = 0;
    agent.speed = 0;
    agent.SetDestination(PatrolPoints[Random.Range(0, PatrolPoints.length)].transform.position);
    agent.speed = DefaultSpeed * 0.3;

    
}

function LastKnownLocation (des)
{

    state = 2;
    agent.speed = DefaultSpeed * 0.75; 
    agent.SetDestination(des);
}

function StopNav()
{
    if (GetComponent.<UnityEngine.AI.NavMeshAgent>().enabled == true)
    {
        state = 0;
        agent.velocity = Vector3.zero;
        isMoving = false;
    }
}

function NavigateTo(ply)
{

    state = 1;
    agent.speed = DefaultSpeed * 1;
    agent.SetDestination(ply.transform.position);
    
    isMoving = true;
}



function TakeHealth (damage, limb, type)
{
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
    
    if (Health >= 1)
    {
        Health -= damage;
    }
    if (damage > (Health/2))
    {
        Staggering = true;
        Anim.GetComponent.<Animation>().clip = StaggerAnimation;
        Anim.GetComponent.<Animation>().Play(StaggerAnimation.name);
        yield WaitForSeconds (StaggerAnimation.length);
        Staggering = false;
    }

}

function Update ()
{
    if (Health <= 0 && IsAlive)
    {
        Die();
    }

    UpdateAnimations();
    
    
}

function UpdateAnimations ()
{
    if (agent.velocity == Vector3.zero)
    {
        Anim.GetComponent.<Animation>().clip = IdleAnimation;
    }
    
    if (IsAlive && !Staggering && !Attacking)
    {
        if (state == 0)
        {
            if (agent.velocity == Vector3.zero)
            {
                Anim.GetComponent.<Animation>().clip = IdleAnimation;
            }
            else
            {
                Anim.GetComponent.<Animation>().clip = WalkAnimation;
            }
        }
        else if (state == 1)
        {
            if (agent.velocity == Vector3.zero)
            {
                Anim.GetComponent.<Animation>().clip = IdleAnimation;
            }
            else
            {
                Anim.GetComponent.<Animation>().clip = RunAnimation;
            }
        }
        else if (state == 2)
        {
            if (agent.velocity == Vector3.zero)
            {
                Anim.GetComponent.<Animation>().clip = IdleAnimation;
            }
            else
            {
                Anim.GetComponent.<Animation>().clip = RunAnimation;
            }
        }
        if (!Anim.GetComponent.<Animation>().IsPlaying(Anim.GetComponent.<Animation>().clip.name))
        {
            Anim.GetComponent.<Animation>().CrossFade(Anim.GetComponent.<Animation>().clip.name);
        }
    }
}


function Die ()
{
    GetComponent.<AudioSource>().PlayOneShot(DeathSounds[Random.Range(0, DeathSounds.length)]);
    StopNav();
    GetComponent.<UnityEngine.AI.NavMeshAgent>().enabled = false;
    spawner.GetComponent("spawner").Alive.remove(transform);
    IsAlive = false;
    var Rag = new Instantiate(RagdollPrefab, transform.position, transform.rotation);
    
    if (lastHitLimb.name != "body" && lastHitLimb.name != "lower_body")
    {
        Rag.Find("Armature/body/"+lastHitLimb.gameObject.name).GetComponent.<Rigidbody>().AddForce(-PlayerDirection * ((LastHitForce / DistanceToPlayer )*3));
    }
    else
    {
        Rag.Find("Armature/"+lastHitLimb.gameObject.name).GetComponent.<Rigidbody>().AddForce(-PlayerDirection * ((LastHitForce / DistanceToPlayer )*3));
    }
    Destroy(gameObject);


}


