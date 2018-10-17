using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMove : MonoBehaviour {


    [SerializeField]
    Transform _destination;
    private NavMeshAgent agent;
    private int lifetime = 2;

    private float accuracyWP = 0.6f; // accuracy when following way points

    public enum State { Dead, Aggro, Patrol };
    public State enumState;



    private float speed = 15;

    public GameObject[] waypoints;
    public int currentWP = 0;


    public Transform player;
    Vector3 direction;




    // Use this for initialization
    void Start() {

        agent = GetComponent<NavMeshAgent>();

        GetComponent<NavMeshAgent>().speed = speed;


        //Vector3 direction = player.position - this.transform.position;
        direction.y = 0;

        currentWP = Random.Range(0, waypoints.Length);

        enumState = State.Patrol;

    }

    // Update is called once per frame
    void Update() {

        print(GetComponent<NavMeshAgent>().speed);

        switch (enumState) {

            case State.Patrol:
                Patrol();
                // print("Patrol");
                break;
            case State.Aggro:
                Aggro();
                // print("Aggro");
                break;
            case State.Dead:
                Dead();
                break;

            default:
                Patrol();
                break;
        }


    }//End Update


    void Dead() {

        Destroy(gameObject, lifetime);
        print("Am dead");
    }

    void Patrol() {

        if (waypoints.Length > 1.1)
        {
            if (Vector3.Distance(waypoints[currentWP].transform.position, transform.position ) < accuracyWP)
            {
                currentWP = Random.Range(0, waypoints.Length);
            }
            //Go to WP direction
            agent.SetDestination(waypoints[currentWP].transform.position);
        }
    }


    void Aggro() {

        agent.SetDestination(player.position);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player"){

            enumState = State.Patrol;
        }
    }

    private void SetDestination()
    {
        if (_destination != null)
        {
            agent.SetDestination(waypoints[currentWP].transform.position);

        }
    }



}
