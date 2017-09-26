var Delay : float;

function Start ()
{
    yield WaitForSeconds(Delay);
    Destroy(gameObject);

}