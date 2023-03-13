using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;


public class ARPLaser : MonoBehaviour
    private LineRenderer lr;
[Serializefield]
privare startPoint;
{
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer> 
    }

    // Update is called once per frame
    void Update()
    {
        lr.setPosition(0, startPoint.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.right, out hit))
        {
            if (hit.collider)
            {
                lr.setPosition(1, hit.point);
            }
        } else lr.setPosition(1, -transform.right * 5000)
    } 

}
