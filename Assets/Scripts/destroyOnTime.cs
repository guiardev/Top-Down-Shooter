using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnTime : MonoBehaviour{

    public float timeDestroy;

    // Start is called before the first frame update
    void Start(){
        Destroy(this.gameObject, timeDestroy);
    }

    // Update is called once per frame
    void Update(){
        
    }
}
