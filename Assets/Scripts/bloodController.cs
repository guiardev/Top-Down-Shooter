using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodController : MonoBehaviour{

    private SpriteRenderer spriteRenderer;

    public Sprite[] bloodType;
    public Color[] bloodColor;

    public float disappearSpeed, waitTime;

    // Start is called before the first frame update
    void Start(){

        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = bloodType[Random.Range(0, bloodType.Length)];

        spriteRenderer.color = bloodColor[0];

        StartCoroutine("disappearColor");
    }

    IEnumerator disappearColor(){

        // fazendo com color do sague desapareca em um certo tempo
        while(spriteRenderer.color != bloodColor[1]){
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, bloodColor[1], disappearSpeed);
            yield return new WaitForSeconds(waitTime);
        }

        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update(){
        
    }

}