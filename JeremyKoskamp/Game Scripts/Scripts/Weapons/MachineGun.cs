using UnityEngine;

public class MachineGun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public float bulletSpeed = 6f;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;


    public float cooldown;
    private float schootCooldown;

    public bool canShoot;

    public delegate void ScreenShake(float shakeTime);
    public static event ScreenShake sendTime;

    private void Start() {
        schootCooldown = cooldown;
    }

    // Update is called once per frame
    void Update () {

        if ( Input.GetMouseButtonDown(0) ) {
            canShoot = true;
        }

        if ( Input.GetMouseButtonUp(0) ) {
            canShoot = false;
            schootCooldown = 0f;
        }


        if ( canShoot ) {
            Shoot();
            if ( schootCooldown > 0 ) {
                schootCooldown -= Time.deltaTime;
            }
            else {
                schootCooldown = cooldown;
            }
        }
	}


    void Shoot() {
        if ( schootCooldown <= 0f ) {
            var bullet = (GameObject) Instantiate( bulletPrefab, bulletSpawn.position, bulletSpawn.rotation );
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

            if ( sendTime != null ) {
                sendTime(0.2f);
            }
            Destroy(bullet, 2f);
        }
    }
}
