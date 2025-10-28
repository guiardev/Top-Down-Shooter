using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHP : MonoBehaviour{

    private gameController _gameController;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public BoxCollider col;
    private Ray shotRay;
    private RaycastHit shotHit;
    private int currentHP, shotMask;

    public float attackDuration = 0.4f, range = 2f;
    public int maxHP, dmg = 1, orderLife, orderDead;
    public bool isDead, isAttack;

    // Start is called before the first frame update
    void Start(){

        _gameController = FindObjectOfType(typeof(gameController)) as gameController;
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        col = GetComponent<BoxCollider>();

        currentHP = maxHP;

        shotMask = LayerMask.GetMask("Player");

        //colocando valor order layer na variável orderLife
        spriteRenderer.sortingOrder = orderLife;
    }

    // Update is called once per frame
    void Update(){

    }

    public void makeHit(int dmg){

        currentHP -= dmg;

        if (currentHP <= 0){

            col.enabled = false;
            isDead = true;
            animator.SetTrigger("dead");

            StartCoroutine("disappear");
        }
    }

    // public void Stop(){
    //     StartCoroutine(StopAnimation(0.7f));
    // }
    
    // IEnumerator StopAnimation(float secondsStop){
    //     yield return new WaitForSeconds(secondsStop);
    //     animator.enabled = false;
    // }


    IEnumerator disappear(){ // fazendo com enemy suma

        //mudando valor order layer variável orderDead
        spriteRenderer.sortingOrder = orderDead;
        Instantiate(_gameController.bloodSprite, transform.position, _gameController.bloodSprite.transform.localRotation);

        yield return new WaitForSeconds(3);

        for (int i = 0; i < 15; i++){
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.02f);
        }

        Destroy(this.gameObject);
    }


    public void setAttack(){
        isAttack = true;
        animator.SetTrigger("attack");
        StartCoroutine("endAttack");
    }

    IEnumerator endAttack(){

        yield return new WaitForSeconds(0.3f);

        shotRay.origin = transform.position;
        shotRay.direction = transform.forward;

        if (Physics.Raycast(shotRay, out shotHit, range, shotMask)){
            shotHit.transform.gameObject.GetComponent<playerController>().makeHit(dmg); // comando quando acertar um enemy
        }

        yield return new WaitForSeconds(0.3f);

        shotRay.origin = transform.position;
        shotRay.direction = transform.forward;

        if (Physics.Raycast(shotRay, out shotHit, range, shotMask)){
            shotHit.transform.gameObject.GetComponent<playerController>().makeHit(dmg); // comando quando acertar um enemy
        }

        yield return new WaitForSeconds(attackDuration);
        isAttack = false;
    }
}