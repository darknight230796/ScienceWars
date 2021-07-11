using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThomasEdisonArmHit : MonoBehaviour {

    private float radius = 1f;
    public LayerMask layer;
	
	// Update is called once per frame
	void Update () {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layer); 
	}
}
