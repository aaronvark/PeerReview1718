using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoManager : MonoBehaviour {


    public Ufo ufo;


    #region Singleton
    public static UfoManager instance;


    private void Awake() {
        if (instance != null) {
            return;
        }

        instance = this;

    }
    #endregion
}
