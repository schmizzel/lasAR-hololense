using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;


public class ARPLaser : MonoBehaviour 
{
    private LineRenderer lr;
    [SerializeField]
    private Transform startPoint;
    public Material mat1, mat2;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, startPoint.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.right, out hit))
        {
            /**
            if (hit.collider.gameObject.tag == "ColliderTest")
            {
                lr.SetPosition(1, hit.point);
            }

            */

            switch (hit.collider.gameObject.tag)
            {
                case "ColliderTest":
                    lr.SetPosition(1, hit.point);
                    break;
                case "AmpTest":
                    lr.material = mat2;
                    break;
            }
        }
        else lr.SetPosition(1, transform.right * 5000);
    } 

}
