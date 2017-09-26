var NewRound : AudioClip;
var BossRound : AudioClip;


function PlayNewRound ()
{

    GetComponent.<AudioSource>().PlayOneShot(NewRound);
}
function PlayBossRound ()
{

    GetComponent.<AudioSource>().PlayOneShot(BossRound);
}