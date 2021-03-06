using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit; //Útil para chamar a caixa de colisão quando não quisermos que um personagem atravesse um objeto

    private void Start(){
        //Assim que o game inicia, uma caixa de colisão é criada.
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate(){
        //A função retorna -1, 0 ou 1, dependendo se o personagem se move, respectivamente, para a esquerda, para lugar nenhum ou para a direita.
        float x = Input.GetAxisRaw("Horizontal");

        //A função retorna -1, 0 ou 1, dependendo se o personagem se move, respectivamente, para baixo, para lugar nenhum ou para cima.
        float y = Input.GetAxisRaw("Vertical");

        //Mostra no console o valor de x a cada loop, ou seja, a cada passagem de frame.
        Debug.Log(x);

        //Mostra no console o valor de y a cada loop, ou seja, a cada passagem de frame.
        Debug.Log(y);

        /*
        moveDelta = Vector3.zero; //Vector3.zero cria um vetor de 3 posições totalmente zerado. Não se faz necessário aqui porque a linha abaixo faz o mesmo serviço.
        */

        //Reset moveDelta
        moveDelta = new Vector3(x,y,0);
        
        //Muda a direção do sprite dependendo se está indo para a esquerda ou para a direita.
        if (moveDelta.x > 0){
            transform.localScale = Vector3.one; //Isso é o mesmo que: "transform.localScale = new Vector3(1,1,1);". Cuidado para não colocar como Vector3(1,0,0), pois assim irá encolher o sprite horizontalmente, fazendo-o desaparecer do mapa.
        }else if (moveDelta.x < 0){
            transform.localScale = new Vector3(-1,1,1);
        }

        //Invoca a caixa de colisão do player a cada frame. Indicará se houve ou não colisão no eixo y. Se a caixa retornar null, o personagem pode se mover na direção apontada. Caso contrário, não.
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0,moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null){
            //Faz o sprite se mover (apenas no eixo y). Time.deltaTime serve para atualizar a posição no eixo com base no tempo que se passou desde o último frame até o atual. Lembrar que este tempo varia.
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        //Invoca a caixa de colisão do player a cada frame. Indicará se houve ou não colisão no eixo x. Se a caixa retornar null, o personagem pode se mover na direção apontada. Caso contrário, não.
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null){
            //Faz o sprite se mover (apenas no eixo x). Time.deltaTime serve para atualizar a posição no eixo com base no tempo que se passou desde o último frame até o atual. Lembrar que este tempo varia.
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
