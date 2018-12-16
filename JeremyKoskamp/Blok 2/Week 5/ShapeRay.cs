using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeRay : MonoBehaviour {

    public GameObject enemyPrefab;
    public GameObject shape;

    public int enemyAmount;

    public float radius;
    private float angle;

    private Vector3 position;

    [SerializeField] private List<Vector3> hitPoints;

    private Vector3 temp = new Vector3();

    // Use this for initialization
    void Start ()
    {
        position = this.transform.position;
        CreateShape();
    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    void CreateShape()
    {
        GameObject shapeObject = Instantiate(shape, position, shape.transform.rotation);
        shapeObject.transform.localScale *= radius;
        Destroy(shapeObject, 0.1f);
        SetPositions();
             
    }


    void SetPositions()
    {
        Quaternion quaternion = Quaternion.AngleAxis(360f / (float)(enemyAmount), transform.up);
        Vector3 vec3 = transform.forward * radius;

        RaycastHit hit;
        Debug.Log("setPos");

        for(int i = 0; i < enemyAmount; i++)
        {
            if(Physics.Raycast(transform.position, vec3, out hit, Mathf.Infinity))
            {
                if(hit.collider.tag == "shape")
                {
                    temp = hit.point;

                    hitPoints.Add(temp);
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    //Vector3 hitPos = hit.point;

                    vec3 = quaternion * vec3;
                    print("hit");
                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
        }
        InstantiateEnemys();        
    }

    void InstantiateEnemys()
    {
        for (int i = 0; i < hitPoints.Capacity; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, hitPoints[i], enemyPrefab.transform.rotation) as GameObject;
            enemy.transform.parent = this.transform;
        }
    }
}
