using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerController.playerDied += StartCotourine;
    }

    void StartCotourine() {
        print("ded");
        StartCoroutine(WaitForNextRound());
    }

    IEnumerator WaitForNextRound() {
        yield return new WaitForSeconds(5);
        ReloadScene();
    }

    void ReloadScene() {
        PlayerController.playerDied -= StartCotourine;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
