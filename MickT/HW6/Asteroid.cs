using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public int points;
    public GameObject player;
    public GameManager gm;

    [SerializeField]
    private float maxThrust;
    [SerializeField]
    private float maxTorque;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float screenTop;
    [SerializeField]
    private float screenBottom;
    [SerializeField]
    private float screenLeft;
    [SerializeField]
    private float screenRight;
    [SerializeField]
    private int asteroidSize; // 1 to 3 sizes 3 max!
    [SerializeField]
    private GameObject mediumAsteroid;
    [SerializeField]
    private GameObject smallAsteroid;


    // Use this for initialization
    void Start () {

        //adds random thurst and rotaion to the astroid
        Vector2 thrust = new Vector2(Random.Range(-maxThrust, maxThrust), Random.Range(-maxThrust, maxThrust));
        float torque = Random.Range(-maxTorque, maxTorque);

        rb.AddForce(thrust);
        rb.AddTorque(torque);

        //find player
        player = GameObject.FindWithTag("Player");
        //find game manager
        gm = GameObject.FindObjectOfType<GameManager>();
	}

	
	// Update is called once per frame
	void Update () {

        // wraping, chanes the position of the astroid when it leaves the screen
        Vector2 newPos = transform.position;
        if (transform.position.y > screenTop) {
            newPos.y = screenBottom;
        }

        if (transform.position.y < screenBottom) {
            newPos.y = screenTop;
        }

        if (transform.position.x > screenRight) {
            newPos.x = screenLeft;
        }

        if (transform.position.x < screenLeft) {
            newPos.x = screenRight;
        }

        transform.position = newPos;

    }


    void OnTriggerEnter2D(Collider2D other) {

        //checks tag Bullet 
        if (other.CompareTag("Bullet")) {
            Destroy(other.gameObject);

            // checks different sizes of the asteroids
            if (asteroidSize == 3) {
                Instantiate(mediumAsteroid, transform.position.normalized, transform.rotation);
                Instantiate(mediumAsteroid, transform.position.normalized, transform.rotation);

                gm.UpdateAmountAsteroids (1);
            }
            else if (asteroidSize == 2) {
                Instantiate(smallAsteroid, transform.position.normalized, transform.rotation);
                Instantiate(smallAsteroid, transform.position.normalized, transform.rotation);

                gm.UpdateAmountAsteroids(1);
            }
            else if (asteroidSize == 1) {

                gm.UpdateAmountAsteroids(-1);
            }

            //Score points
            player.SendMessage("ScorePoints",points);

            //remove asteroid
            Destroy(gameObject);
        }
    }
}
