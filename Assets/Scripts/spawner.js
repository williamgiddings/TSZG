var wave : int = 0;
var zombies : Transform[];
var Bosses : Transform[];
var Alive = new Array();
var Spawns : Transform[];
var MaxZombies : int;
private var SpawnedZombies : int;
var GUIObject : Transform;
private var GUI : Component;


function Start () 
{
    GUI = GUIObject.GetComponent("HUD");
    IncrementWave();
}


function IncrementWave ()
{
    SpawnedZombies = 0;
    wave += 1;
    MaxZombies += 3;
    Spawn(MaxZombies);
    GUI.RoundCounter.transform.GetComponent(Animation).Play("NewRound");

}


function Spawn (iteration)
{
    for (SpawnedZombies = 0; SpawnedZombies < iteration; SpawnedZombies++)
    {
        var zomb = Instantiate(zombies[Random.Range(0,zombies.length)], Spawns[Random.Range(0, Spawns.length)].transform.position, transform.rotation);
        zomb.gameObject.name == "zombie";
        zomb.GetComponent("zombie").spawner = transform;
        Alive.Push(zomb);

        yield WaitForSeconds(2);
    }
}

function FixedUpdate ()
{
    if (wave > 0)
    {
        if (SpawnedZombies == MaxZombies && Alive.length == 0)
        {
            IncrementWave();
        }
    }
    GUI.RoundCounterNum.text = wave.ToString();;
}
