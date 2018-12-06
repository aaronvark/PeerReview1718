using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp : BaseEnemy {

    public float distance;

    public StateMachine<Imp> stateMachine { get; set; }

    private void Start() {
        stateMachine = new StateMachine<Imp>(this);
        stateMachine.ChangeState(RunToPlayer.Instance);
    }

    private void Update() {
            stateMachine.Update();        
    }
}
