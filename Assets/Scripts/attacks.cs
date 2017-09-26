using UnityEngine;
using System.Collections;

public class attacks : MonoBehaviour {

    public Attacks LightAttack;
    private zombie BaseScript;
    private float timeSinceLast;


    void Start() {
        BaseScript = transform.root.GetComponent<zombie>();
    }

    void OnTriggerStay(Collider col) {
        if (col.transform.tag == "Player")
        {
            if (Time.time > (timeSinceLast + LightAttack.Cooldown))
            {
                Attack(col.transform, LightAttack);
            }
        }
    }

    void Attack(Transform target, Attacks type)
    {
        StartCoroutine(_attack(target, type));
    }

    IEnumerator _attack(Transform target, Attacks type)
    {
        BaseScript.Attacking = true;
        transform.root.GetComponent<Animation>().clip = type.AttackAnim;
        transform.root.GetComponent<Animation>().Play();
        transform.root.GetComponent<AudioSource>().PlayOneShot(type.Sound);
        target.GetComponent<player>().TakeHealth(type.Damage, 0);
        timeSinceLast = Time.time;
        yield return new WaitForSeconds(type.AttackAnim.length);
        BaseScript.Attacking = false;
    }
	
    [System.Serializable]
    public class Attacks
    {
        public int Damage;
        public AnimationClip AttackAnim;
        public float Cooldown;
        public AudioClip Sound;
    }
}