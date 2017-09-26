var MagRelease : AudioClip;
var MagOut : AudioClip;
var MagIn : AudioClip;
var MagPush : AudioClip;
var BoltRelease : AudioClip;
var BoltBack : AudioClip;
var Whoosh : AudioClip;
var Misc1 : AudioClip;
var Misc2 : AudioClip;
private var AudioEmmiter : AudioSource;


function Start ()
{
    AudioEmmiter = GetComponent.<AudioSource>();
}

function _MagRelease ()
{
    AudioEmmiter.PlayOneShot(MagRelease);
}

function _MagOut ()
{
    AudioEmmiter.PlayOneShot(MagOut);
}

function _MagIn ()
{
    AudioEmmiter.PlayOneShot(MagIn);
}

function _MagPush ()
{
    AudioEmmiter.PlayOneShot(MagPush);
}

function _BoltRelease ()
{
    AudioEmmiter.PlayOneShot(BoltRelease);
}

function _BoltBack ()
{
    AudioEmmiter.PlayOneShot(BoltBack);
}

function _Whoosh ()
{
    AudioEmmiter.PlayOneShot(Whoosh);
}

function _Misc1 ()
{
    AudioEmmiter.PlayOneShot(Misc1);
}

function _Misc2 ()
{
    AudioEmmiter.PlayOneShot(Misc2);
}