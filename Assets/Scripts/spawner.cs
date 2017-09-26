using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawner : MonoBehaviour {
    public int wave = 0;
    public Transform[] zombies;
    public Transform[] Bosses;
    public List<Transform> Alive = new List<Transform>();
    public Transform[] Spawns;
    public int MaxZombies;
    private int SpawnedZombies;
    public Transform GUIObject;
    private HUD GUI;


    void Start (){
        GUI = GUIObject.GetComponent<HUD>();
        IncrementWave();
    }


    void IncrementWave (){
        SpawnedZombies = 0;
        wave += 1;
        MaxZombies += 3;
        Spawn(MaxZombies);
        GUI.RoundCounter.transform.GetComponent<Animation>().Play("NewRound");

    }

    public void Spawn (int iteration)
    {
        StartCoroutine(_spawn(iteration));
    }

    IEnumerator _spawn (int iteration){
        for (SpawnedZombies = 0; SpawnedZombies < iteration; SpawnedZombies++)
        {
            Transform zomb= Instantiate(zombies[Random.Range(0,zombies.Length)], Spawns[Random.Range(0, Spawns.Length)].transform.position, transform.rotation);
            zomb.gameObject.name = "zombie";
            zomb.GetComponent<zombie>().spawner = transform;
            Alive.Add(zomb);

            yield return new WaitForSeconds(2);
        }
    }

    void FixedUpdate (){
        if (wave > 0)
        {
            if (SpawnedZombies == MaxZombies && Alive.Count == 0)
            {
                IncrementWave();
            }
        }
        GUI.RoundCounterNum.text = wave.ToString();;
    }

}