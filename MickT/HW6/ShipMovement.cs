using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipMovement : MonoBehaviour {

    public Ufo ufo;
    public Text scoreMenuText;
    public Text scoreText;
    public Text livesText;
    public GameObject gameOverScreen;

    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float thrust;
    [SerializeField]
    private float turnThrust;
    private float thrustInput;
    private float turnInput;
    [SerializeField]
    private float boost;
    [SerializeField]
    private float boostBack;
    [SerializeField]
    private float screenTop, screenBottom, screenLeft, screenRight; // dient voor de grensen van het scherm
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float bulletForce;
    [SerializeField]
    private float forcedeath;
    [SerializeField]
    private Color immortalColor;
    [SerializeField]
    private Color normalColor;
    [SerializeField]
    private AudioSource audio;
    [SerializeField]
    private GameObject explosion;
    private int lives = 3;
    private int score;


    // Use this for initialization
    void Start () {
        score = 0;

        scoreText.text = "Score " + score;
        scoreMenuText.text = "Score " + score;
        livesText.text = "Lives " + lives;

    }
	

	// Update is called once per frame
	void Update () {
        // inut van keyboard
        thrustInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        //input fires bullet
        if (Input.GetButtonDown("Fire1")){
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * bulletForce);
            Destroy(newBullet, 3.5f);
        }

        //Boost forward activate
        if (Input.GetButtonDown("Boost")) {
            rb.AddRelativeForce(Vector2.up * boost);
        }

        //Boost activate
        if (Input.GetButtonDown("BoostBack")) {
            rb.AddRelativeForce(Vector2.down * boostBack);
        }

        //rotate ship
        transform.Rotate(Vector3.forward * turnInput * Time.deltaTime * -turnThrust);


        // wraping, changes the position of the ship when it leaves the screen
        Vector2 newPos = transform.position;
        if (transform.position.y > screenTop){
            newPos.y = screenBottom;
        }

        if (transform.position.y < screenBottom){
            newPos.y = screenTop;
        }

        if (transform.position.x > screenRight){
            newPos.x = screenLeft;
        }

        if (transform.position.x < screenLeft){
            newPos.x = screenRight;
        }

        transform.position = newPos;

    }


    void FixedUpdate()
    {
        
        rb.AddRelativeForce(Vector2.up * thrustInput);
        //rb.AddTorque(-turnInput);

    }


    void ScorePoints(int addPoints) {
        score += addPoints;
        scoreText.text = "Score " + score;
        scoreMenuText.text = "Score " + score;
    }


    // If player dies it will be immortal for 3 seconds when it respawns
    void Respawn() {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.enabled = true;
        sr.color = immortalColor;
        Invoke ("Immortal", 3f);
    }


    void Immortal() {
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = normalColor;
    }


    void LoseLife() {
        lives--;
        // explosion particles ship
        GameObject newEplosion = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(newEplosion, 3f);
        //play explosion sound
        audio.Play();

        livesText.text = "Live " + lives;
        //Respawn
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Invoke("Respawn", 3f);

        if (lives <= 0) {
            //death
            GameOver();
        }
    }


    //Lose life
    private void OnCollisionEnter2D(Collision2D col) {
        if (col.relativeVelocity.magnitude > forcedeath) {
            LoseLife();
        }
    }


    //Lose life when shot by laser
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Laser")) {
            LoseLife();
            ufo.Disable();
        }
    }


    // Shows game over screen
    void GameOver() {
        CancelInvoke();
        gameOverScreen.SetActive(true);
    }


    public void TryAgain() {
        SceneManager.LoadScene("Main");
    }


    public void GoToMenu() {
        SceneManager.LoadScene("Menu");
    }

}
