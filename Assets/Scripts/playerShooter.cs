using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerShooter : MonoBehaviour{

    private playerController _playerController;
    private gameController _gameController;
    private LineRenderer _shotLine;
    private RaycastHit _shotHit;
    private Ray _shotRay; // alcance da arma
    private float _timer;
    private int _shotMask;
    private bool _isReload;

    public GameObject hitBoxKnifePrefab;
    public Transform hitBoxKnifePosition, aim;
    public Transform[] shotPosition;
    public Image imgArma;
    public Sprite[] icoArma;
    public List<GameObject> armas;
    public TMP_Text qtdPenteTxt, qtdAmmunitionTxt;
    public float[] timeBetweenBullets, rangeAim;
    public float range = 100f, displayEffect = 0.02f, delayKnifeAttack = 0.6f;
    
    // 0: qtdPentes, 1: qtdAmmunition, 2: qtdAmmunitionPorPentes
    public int[,] penteAmmunition = new int[3,3]; // array multidimensionais 3 linha e 3 colunas
    public int idArma;
    public bool isShot, isKnifeAttack;

    // Start is called before the first frame update
    void Start(){

        _playerController = GetComponent<playerController>();
        _gameController = FindObjectOfType(typeof(gameController)) as gameController;

        _shotLine = GetComponent<LineRenderer>();
        _shotLine.enabled = false;

        _shotMask = LayerMask.GetMask("Enemy"); // buscando layer enemy

        changeWeapon();

        // definindo valores nas suas posicoes
        //knife
        penteAmmunition[0,0] = 0;
        penteAmmunition[0,1] = 0;
        penteAmmunition[0,2] = 0;

        //riffle
        penteAmmunition[1,0] = 1; // quantidade pentes
        penteAmmunition[1,1] = 60;
        penteAmmunition[1,2] = 60;

        //gun
        penteAmmunition[2,0] = 1;
        penteAmmunition[2,1] = 12;
        penteAmmunition[2,2] = 12;

    }

    // Update is called once per frame
    void Update(){

        // fazendo com mira tenho alcance debatendo id arma, o transform.forward e para dar direção
        aim.position = shotPosition[idArma].position + transform.forward * rangeAim[idArma];

        _timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && idArma == 0 && isKnifeAttack == false){

            isKnifeAttack = true;
            _playerController.TriggerShot();

            //Instantiate(hitBoxKnifePrefab, hitBoxKnifePosition.position, transform.localRotation);
            //Debug.LogError("Pause"); // pausa no game

            StartCoroutine("endKnifeAttack");
        }

        // verificando a munição e tempo extra elas
        if(Input.GetButton("Fire1") && _timer >= timeBetweenBullets[idArma] && idArma != 0 && _isReload == false){
            Shot();
        }

        if(Input.GetButtonUp("Fire1")){
            isShot = false;
        }

        // verifica tempo que foi criando linha para método fazer desaparecer linha
        if(_timer >= timeBetweenBullets[idArma] * displayEffect){
            DisableEffect();
        }

        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0 && isKnifeAttack == false && _isReload == false){

            idArma += 1;

            if(idArma > armas.Count - 1){ // nao deixar passar numero armas que tem no game
                idArma = 0;
            }

            changeWeapon();
            _playerController.changeAnimationLayer(idArma);
        }

        if(Input.GetAxisRaw("Mouse ScrollWheel") < 0 && isKnifeAttack == false && _isReload == false){

            idArma -= 1;

            if(idArma < 0){ // nao deixar passar numero armas que tem no game
                idArma = armas.Count - 1;
            }

            changeWeapon();
            _playerController.changeAnimationLayer(idArma);
        }
    }

    void Shot(){

        _timer = 0;
        isShot = true;

        if (checkAmmunition() == true){

            _playerController.TriggerShot();

            _gameController.playFX(_gameController.fxFire[idArma]);

            // agilitado line do LineRenderer e sua posição
            _shotLine.enabled = true;
            _shotLine.SetPosition(0, shotPosition[idArma].position);

            _shotRay.origin = shotPosition[idArma].position;
            _shotRay.direction = transform.forward;

            // verificar quando ele acertou inimigo
            if (Physics.Raycast(_shotRay, out _shotHit, range, _shotMask)){

                //comando quando acertar um inimigo
                Instantiate(_gameController.bloodAnimation, _shotHit.point, _gameController.bloodAnimation.transform.localRotation);
                _shotLine.SetPosition(1, _shotHit.point); // essa onde vai sair tiro
                _shotHit.transform.gameObject.GetComponent<enemyHP>().makeHit(1); // tirando hp enemy
            }else{
                _shotLine.SetPosition(1, _shotRay.origin + _shotRay.direction * range);
            }

            // tirando munição e os pentes
            penteAmmunition[idArma, 1] -= 1;

            UpDateHud(idArma); // update hud quantidade bala

            //verificando se tem um pente da arma que player estiver usando
            if (penteAmmunition[idArma, 1] == 0 && penteAmmunition[idArma, 0] > 0){

                // penteAmmunition[idArma, 1] = penteAmmunition[idArma, 2];
                // penteAmmunition[idArma, 0] -= 1;
                StartCoroutine("loading");
            }
            
            _playerController.Stop();
        }else{
            _gameController.playFX(_gameController.noAmmo);
        }

    }

    void DisableEffect(){
        _shotLine.enabled = false;
    }

    IEnumerator endKnifeAttack(){

        yield return new WaitForSeconds(0.2f);
        Instantiate(hitBoxKnifePrefab, hitBoxKnifePosition.position, transform.localRotation);

        yield return new WaitForSeconds(delayKnifeAttack);
        isKnifeAttack = false;
    }


    void changeWeapon(){

        if(idArma == 0){
            qtdAmmunitionTxt.enabled = false;
            qtdPenteTxt.enabled = false;
        }else{
            qtdAmmunitionTxt.enabled = true;
            qtdPenteTxt.enabled = true;
        }

        imgArma.sprite = icoArma[idArma];

        UpDateHud(idArma);
    }

    void UpDateHud(int idA){
        qtdPenteTxt.text = penteAmmunition[idA, 0].ToString();
        qtdAmmunitionTxt.text = penteAmmunition[idA, 1].ToString();
    }

    bool checkAmmunition(){
        bool haveAmmunition = penteAmmunition[idArma, 1] > 0;
        return haveAmmunition;
    }

    IEnumerator loading(){

        _isReload = true;
        _gameController.playFX(_gameController.fxReload[idArma]);

        yield return new WaitForSeconds(_gameController.fxReload[idArma].length);
        penteAmmunition[idArma, 1] = penteAmmunition[idArma, 2];
        penteAmmunition[idArma, 0] -= 1;

        _isReload = false;

        UpDateHud(idArma);
    }

}