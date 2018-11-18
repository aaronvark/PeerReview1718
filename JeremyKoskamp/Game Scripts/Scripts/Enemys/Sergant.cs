using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sergant : BaseEnemy {

    public float lookRadius = 10000f;
    public int health;
    private int damage;

    public float distance;

    public static Transform target;
    public GameObject deathParticle;
    NavMeshAgent agent;

    public delegate void EnemyDied(int Death);
    public static event EnemyDied SendDeath;


    private void Awake() {
        OnHitDamage.SendHit += TakeDamage;
    }


    // Use this for initialization
    void Start () {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        distance = Vector3.Distance(target.position, transform.position);
    }
	

	// Update is called once per frame
	void Update () {
        

        if (distance <= lookRadius) {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance) {
                // face target
                FaceTarget();

                // Attack target 
                Attack();
            }
        }

        if(health < 0 ) {            
            Death();            
        }
	}


    private void TakeDamage(GameObject enemy, int hitDamage) {
        if (enemy == gameObject) {            
            health -= hitDamage;
        }
    }

    private void Death() {
        OnHitDamage.SendHit -= TakeDamage;
        Instantiate(deathParticle, gameObject.transform.position, Quaternion.identity, deathParticle.transform.parent = null);
        agent.speed = 0;

        if ( SendDeath != null ) {
            SendDeath(1);
        }

        Destroy(this.gameObject);


    }


    void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    void Attack() {
        Debug.Log("Biem");
    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
