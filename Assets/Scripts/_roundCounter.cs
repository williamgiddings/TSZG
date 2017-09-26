using UnityEngine;
using System.Collections;

public class _roundCounter : MonoBehaviour
{
    AudioClip NewRound;
    AudioClip BossRound;


    void PlayNewRound (){

        GetComponent<AudioSource>().PlayOneShot(NewRound);
    }
    void PlayBossRound (){

        GetComponent<AudioSource>().PlayOneShot(BossRound);
    }
}