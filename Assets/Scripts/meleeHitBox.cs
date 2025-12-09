using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeHitBox : MonoBehaviour{

    void Start(){
        Destroy(this.gameObject, 0.02f);
    }

    void OnTriggerEnter(Collider col){
        col.gameObject.GetComponent<enemyHP>().makeHit(1);
        //Debug.Log("ok");
    }
}