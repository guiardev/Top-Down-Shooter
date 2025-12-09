using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour{

    private gameController _gameController;
    private playerShooter _playerShooter;
    private Rigidbody _playerRb;
    private Animator _playerAnimator;
    private Vector3 _movement;
    private float _camRayLength = 100f, _percHp;
    private int _groundMask, _currentHp;
    private bool _isWalk, _isStep;

    public float speed;
    public int maxHp;

    // Start is called before the first frame update
    void Start(){

        _gameController = FindObjectOfType(typeof(gameController)) as gameController;
        _playerShooter = GetComponent<playerShooter>();

        _playerRb = GetComponent<Rigidbody>();
        _playerAnimator = GetComponentInChildren<Animator>();

        //Cursor.visible = false;

        _currentHp = maxHp;

        _groundMask = LayerMask.GetMask("Ground"); // verifica onde esta chão

    }

    // Update is called once per frame
    void FixedUpdate(){

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Move(horizontal, vertical);

        Turning();
    }

    void Move(float h, float v){

        // verificando se ele nao esta atirar, pq ele vai parar quando tiver atirando
        if(_playerShooter.isShot == false && _playerShooter.isKnifeAttack == false){

            _movement.Set(h, 0, v); // vai determinar movement

            if(h == 0 && v == 0){
                _isWalk = false;
            }else{
                _isWalk = true;
            }

        }else{
            _movement.Set(0, 0, 0);
            _isWalk = false;
        }

        if(_isWalk == true && _isStep == false){
            StartCoroutine("stepFx");
        }

        // When normalized, a vector keeps the same direction but its length is 1
        _movement = _movement.normalized * speed * Time.deltaTime;

        _playerRb.MovePosition(transform.position + _movement);
        _playerAnimator.SetBool("isWalk", _isWalk);
    }

    void Turning(){

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);  // vai criar um linha na direção mouse
        RaycastHit groundHit;

        // verifica se tem vai colidir com chão e ne outro objeto que esteja layer ground
        if(Physics.Raycast(camRay, out groundHit, _camRayLength, _groundMask)){
            
            Vector3 playerToMouse = groundHit.point - transform.position;
            playerToMouse.y = 0; // colocando y no valor 0 para ele nao rotacionar objeto

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse); // deslindo para onde vai olhar

            if(_playerShooter.isKnifeAttack == false){
                _playerRb.MoveRotation(newRotation);
            }
        }
    }

    public void TriggerShot(){
        //_playerAnimator.SetTrigger("attack");
        _playerAnimator.SetBool("attack", true);
        Stop();
    }

    public void Stop(){
        StartCoroutine(StopAnimation(0.3f));
    }
    
    IEnumerator StopAnimation(float secondsStop){
        yield return new WaitForSeconds(secondsStop);
        _playerAnimator.SetBool("attack", false);
        //_playerAnimator.SetBool("isWalk", false);
        Debug.Log("StopAnimation");
    }
    
    public void changeAnimationLayer(int idLayer){

        for (int i = 0; i < _playerAnimator.layerCount; i++){
            _playerAnimator.SetLayerWeight(i, 0);
        }

        _playerAnimator.SetLayerWeight(idLayer, 1);
    }



    public void makeHit(int dmg){

        _currentHp -= dmg;
        _percHp = (float)_currentHp / (float)maxHp; // convertendo valor int maxHp para float

        if(_percHp < 0){
            _percHp = 0;
        }

        _gameController.updateHUD(_percHp);
    }

    IEnumerator stepFx(){

        _isStep = true;

        int idStep = Random.Range(0, _gameController.fxStep.Length);
        _gameController.playFX(_gameController.fxStep[idStep]);
        yield return new WaitForSeconds(_gameController.fxStep[idStep].length);

        _isStep = false;
    }

}