using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    private Vector3 playerPos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canJump;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;
    public int amountOfBullet;
    public float spread, bulletSpeed;



    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }
    

    void Update()
    {

        //Check if character just landed on the ground
       /* if (GameObject.Find("PrototypeHero").GetComponent<PrototypeHeroDemo>().m_grounded == false)
        {
            canJump = false;

        }*/
     


        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
     

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;

            }
        }

        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;

            Shoot();
        }


        if (Input.GetButtonDown("Fire1"))
        {
           
            if (rotZ > -120 && rotZ < -65)
            {
                
                canJump = true;
                
            

            }
            
        }


        if (Input.GetButtonDown("Fire1") == false )
        {
                canJump = false;
        }



    }

 

    
    void Shoot()
    {
        for (int i = 0; i < amountOfBullet; i++)
        {
            GameObject b = Instantiate(bullet, bulletTransform.position, bulletTransform.rotation);
            Rigidbody2D brb = b.GetComponent<Rigidbody2D>();
            Vector2 dir = transform.rotation * Vector2.right;
            Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-spread, spread);
            brb.velocity = (dir + pdir) * bulletSpeed;

        }
    }
}
