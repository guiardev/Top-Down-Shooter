using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour{

    private enemyHP _enemyHP;
    private NavMeshAgent nav;
    private Transform playerTransform;

    public float distanceAttack;
    
    // Start is called before the first frame update
    void Start(){

        _enemyHP = GetComponent<enemyHP>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();

        //nav.destination = playerTransform.position;
    }

    // Update is called once per frame
    void Update(){

        if (playerTransform != null){
            nav.destination = playerTransform.position; // comando que vai fazer inimigo move para um determinado ponto onde estiver player
        }

        if(_enemyHP.isDead == true){
            Debug.Log("_enemyHP.isDead ");
            nav.enabled = false;
        }

        float distance = Vector3.Distance(transform.position, playerTransform.position); // calculando distancia player

        if (distance <= distanceAttack && _enemyHP.isAttack == false){ // verificando distancia player que enemy vai attack
            _enemyHP.setAttack();
        }

    }
}