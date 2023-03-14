using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class ARPLaserStart : MonoBehaviour
{
    public Material mat1, mat2;
    ARPLaser beam;
    public bool laserActive;
    public Vector3 direction;

    void Start()
    {
        UnityEngine.Debug.Log(gameObject.transform.position);
    }


    void Update()
    {
        if (laserActive)
        {
            Destroy(GameObject.Find("Laser Beam-" + gameObject.name));
            beam = new ARPLaser(gameObject.transform.position, direction, mat1, mat2, gameObject.name); // pass in direction variable
        }
        else
        {
            Destroy(GameObject.Find("Laser Beam-" + gameObject.name));
        }
    }

    public void SetLaserActive(bool input)
    {
        laserActive = input;
    }

    public void ActivateSecondPortal()
    {
        GameObject.Find("Portal2").GetComponent<ARPLaserStart>().SetLaserActive(true);
        //UnityEngine.Debug.Log("Activated Laser for: " + GameObject.Find("Portal2").name);
    }
}