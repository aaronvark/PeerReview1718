using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State<Imp> {

    private static Attack _instance;

    private Attack() {
        if ( _instance != null ) {
            return;
        }
        _instance = this;
    }

    public static Attack Instance {
        get {
            if ( _instance == null ) {
                new Attack();
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
