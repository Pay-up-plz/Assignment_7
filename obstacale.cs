using UnityEngine;
using System.Collections;

public class obstacle : MonoBehaviour {

    public GameObject obstacle2Destroy;

    void OnTriggerEnter2D(Collider2D col){
        if (col.CompareTag("Animal")){
            Destroy(obstacle2Destroy);

        }

    }
 }
