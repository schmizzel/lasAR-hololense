using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class ARPLaserStart : MonoBehaviour
{
    public Material mat1, mat2;
    public bool laserActive;
    public Vector3 direction;
    public AudioSource laserSound;
    LaserNew beam;

    void Start() {}

    void Update(){
        if (laserActive) {
            Destroy(GameObject.Find("Laser Beam"));
            beam = new LaserNew(gameObject.transform.position, direction, mat1, mat2); 
        } else {
            Destroy(GameObject.Find("Laser Beam"));
        }
    }

    public void SetLaserActive(bool input) {
        laserActive = input;
    }
}