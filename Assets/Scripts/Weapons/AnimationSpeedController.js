var AnimationSpeeds : float[];

function Start ()
{
    var iteration = 0;
    for (var state : AnimationState in GetComponent.<Animation>())
    {
        
        transform.GetComponent.<Animation>()[state.name].speed = AnimationSpeeds[iteration];
        iteration += 1;
    }
    iteration = 0;
}