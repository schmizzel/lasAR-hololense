using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARPLaserStart : MonoBehaviour
{
    // Start is called before the first frame update
    public Material mat1, mat2;
    ARPLaser beam;
    public bool laserActive;

    // Update is called once per frame
    void Update()
    {
        if (laserActive)
        {
            Destroy(GameObject.Find("Laser Beam-" + gameObject.name));
            beam = new ARPLaser(gameObject.transform.position, gameObject.transform.right, mat1, mat2, gameObject.name);
        } else
        {
            Destroy(GameObject.Find("Laser Beam-" + gameObject.name));
        }
    }

    public void SetLaserActive(bool input)
    {
        laserActive = input;
    }

}
