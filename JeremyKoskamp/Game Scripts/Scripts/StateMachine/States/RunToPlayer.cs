using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunToPlayer : State<Imp> {

    private static RunToPlayer _instance;

    public float lookRadius = 10000f;
    public int health;
    private int damage;

    public float distance;

    public static Transform target;
    public GameObject deathParticle;
    NavMeshAgent agent;

    private RunToPlayer() {
        if ( _instance != null ) {
            return;
        }
        _instance = this;
    }

    public static RunToPlayer Instance {
        get {
            if ( _instance == null ) {
                new RunToPlayer();
            }
            return _instance;
        }
    }

    public override void EnterState(Imp _owner) {
        Debug.Log("Entering First State");

    }

    public override void ExitState(Imp _owner) {
        Debug.Log("Exiting First State");
    }

    public override void UpdateState(Imp _owner) {
        if ( _owner ) {
            _owner.stateMachine.ChangeState(Attack.Instance);
        }
    }
}
