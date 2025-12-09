using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour{

    private Camera mainCamera;

    public Image barraHP;

    public AudioSource fxSource, MusicSource;
    public AudioClip[] fxStep, fxFire, fxReload;
    public AudioClip noAmmo;

    public Transform player, limiteCamL, limiteCamR, limiteCamT, limiteCamB;
    public GameObject bloodAnimation, bloodSprite;
    public float x, z, speedCam;

    // Start is called before the first frame update
    void Start(){
        
        mainCamera = Camera.main;

        x = player.position.x;
        z = player.position.z;
    }

    // Update is called once per frame
    void Update(){
        
    }

    void LateUpdate(){

        Vector3 posCurrentCam = mainCamera.transform.position;
        Vector3 posDestCam = posCurrentCam;

        //limitando camera lados

        if((mainCamera.transform.position.x > limiteCamL.position.x || mainCamera.transform.position.x < player.position.x) 
            && (mainCamera.transform.position.x < limiteCamR.position.x || mainCamera.transform.position.x > player.position.x)){
            x = player.position.x;
        }

        if(mainCamera.transform.position.x < limiteCamL.position.x && player.position.x < limiteCamL.position.x){
            x = limiteCamL.position.x;
        }

        if(mainCamera.transform.position.x > limiteCamR.position.x && player.position.x > limiteCamR.position.x){
            z = limiteCamR.position.x;
        }

        if(mainCamera.transform.position.z > limiteCamB.position.z || mainCamera.transform.position.z < player.position.z){
            z = player.position.z;
        }

        if(mainCamera.transform.position.z < limiteCamB.position.z && player.position.z < limiteCamB.position.z){
            z = limiteCamB.position.z;
        }else if(mainCamera.transform.position.z > limiteCamT.position.z){
            z = limiteCamT.position.z;
        }


        // fazendo o movimento camera
        posDestCam = new Vector3(x, mainCamera.transform.position.y, z);
        posCurrentCam = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        mainCamera.transform.position = Vector3.Lerp(posCurrentCam, posDestCam, speedCam * Time.deltaTime);

    }

    public void updateHUD(float percHp){
        barraHP.fillAmount = percHp;
    }

    public void playFX(AudioClip clip){
        fxSource.PlayOneShot(clip);
    }
}