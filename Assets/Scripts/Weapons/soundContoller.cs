using UnityEngine;
using System.Collections;

public class soundContoller : MonoBehaviour {
    public AudioClip MagRelease;
    public AudioClip MagOut;
    public AudioClip MagIn;
    public AudioClip MagPush;
    public AudioClip BoltRelease;
    public AudioClip BoltBack;
    public AudioClip Whoosh;
    public AudioClip Misc1;
    public AudioClip Misc2;
    private AudioSource AudioEmmiter;


    void Start (){
        AudioEmmiter = GetComponent<AudioSource>();
    }

    void _MagRelease (){
        AudioEmmiter.PlayOneShot(MagRelease);
    }

    void _MagOut (){
        AudioEmmiter.PlayOneShot(MagOut);
    }

    void _MagIn (){
        AudioEmmiter.PlayOneShot(MagIn);
    }

    void _MagPush (){
        AudioEmmiter.PlayOneShot(MagPush);
    }

    void _BoltRelease (){
        AudioEmmiter.PlayOneShot(BoltRelease);
    }

    void _BoltBack (){
        AudioEmmiter.PlayOneShot(BoltBack);
    }

    void _Whoosh (){
        AudioEmmiter.PlayOneShot(Whoosh);
    }

    void _Misc1 (){
        AudioEmmiter.PlayOneShot(Misc1);
    }

    void _Misc2 (){
        AudioEmmiter.PlayOneShot(Misc2);
    }
}