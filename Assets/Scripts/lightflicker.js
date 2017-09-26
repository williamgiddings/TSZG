var flickerspeedMin : float;
var flickerspeedMax : float;
var MinIntensity : float;
var MaxIntensity : float;

private var changing : boolean;

function Update ()
{
    if (!changing)
    {
        LightChange();
    }
}

function LightChange ()
{
    changing = true;
    GetComponent.<Light>().intensity = (Random.Range(MinIntensity, MaxIntensity));
    yield WaitForSeconds(Random.Range(flickerspeedMin, flickerspeedMax));
    changing = false;
}