using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //variables 
    public float movementSpeed;
    
    public GameObject playerMovePoint;
    private Transform pmr;
    private bool pmrSpawned;
    private GameObject triggeringPMR;

    private bool moving;


    //Fungsi
    void Update()
    {


       //Player movement
       Plane playerPlane = new Plane(Vector3.up, transform.position);
       Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
       float hitDistance = 0.0f;

       if(playerPlane.Raycast(ray, out hitDistance))
       {
           Vector3 mousePosition = ray.GetPoint(hitDistance);
           if(Input.GetMouseButtonDown(0))
           {
               moving = true;

              if(pmrSpawned)
              {
                  pmr = null;
                  pmr = Instantiate(playerMovePoint.transform, mousePosition, Quaternion.identity);
              } else {
                  pmr = Instantiate(playerMovePoint.transform, mousePosition, Quaternion.identity);
              }
           }
       }
         if(pmr)
          pmrSpawned = true;
        else
          pmrSpawned = false;

        if(moving)
        Move();
    }
    
    
    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, pmr.transform.position, movementSpeed);
        this.transform.LookAt(pmr.transform);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PMR")
        {
            triggeringPMR = other.gameObject;
            triggeringPMR.GetComponent<PMR>().DestroyPMR();
        }
    }
}
