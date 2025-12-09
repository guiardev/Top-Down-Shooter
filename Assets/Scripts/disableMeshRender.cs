using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableMeshRender : MonoBehaviour{

    // Start is called before the first frame update
    void Start(){
        GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update(){
        
    }
}
