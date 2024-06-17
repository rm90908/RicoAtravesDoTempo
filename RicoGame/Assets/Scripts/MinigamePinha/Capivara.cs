using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capivara : MonoBehaviour
{
    public Vector3 PosicaoCentro = new Vector3 (0f, -1.33f, 0f);
    public Vector3 PosicaoEsquerda = new Vector3 (-1.5f, -1.33f, 0f);
    public Vector3 PosicaoDireita = new Vector3 (1.5f, -1.33f, 0f);

    public Universo universo; 
    public bool PodeMover;
    
    [SerializeField]
    float impulso;
    SpriteRenderer capivara;

    Rigidbody2D rig;
    private SpriteRenderer spriteRenderer;
    public Sprite RicoVivo;
    public Sprite RicoMorto;

    public Color corDano = Color.red;
    public Color corNormal = Color.white;
    public Color corCura = Color.green;
    // Start is called before the first frame update
    void Start()
    {
        capivara = GetComponent<SpriteRenderer>();
        PodeMover = true;
        rig=GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = RicoVivo;
    }
    // Update is called once per frame
    void Update()
    {
    
    }
    public void Parar(){
        PodeMover = false;
    }
    public void Voltar(){
        PodeMover = true;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pinha"))
        {
                universo.Addpontos();
            
        }
        if (collision.gameObject.CompareTag("Bigorna"))
        {
            GameObject objetoUniverso = GameObject.Find("Universo");
            if (objetoUniverso != null)
            {
                StartCoroutine(Dano());
                Universo universo = objetoUniverso.GetComponent<Universo>();
                universo.Dano();
            }
        }
        if (collision.gameObject.CompareTag("Coracao")){
            GameObject objetoUniverso = GameObject.Find("Universo");
            if (objetoUniverso != null)
            {
                StartCoroutine(Cura());
                Universo universo = objetoUniverso.GetComponent<Universo>();
                universo.Cura();
                Debug.Log("colidiu com coracao");
            }
        }
    }
    IEnumerator Dano()
    {
        spriteRenderer.color = corDano;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = corNormal;
    }
    IEnumerator Cura()
    {
        spriteRenderer.color = corCura;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = corNormal;
    }
    public void Morreu(){
        spriteRenderer.sprite = RicoMorto;
        PodeMover = false;
        capivara.flipY=false;

        rig.bodyType=RigidbodyType2D.Dynamic;
        rig.AddForce(Vector2.up * impulso,ForceMode2D.Impulse);
    }
    public void Reviveu(){
        spriteRenderer.sprite = RicoVivo;
        PodeMover = true;
        capivara.flipY=false;
        transform.position = PosicaoCentro;
        capivara.flipX=false;
        rig.bodyType=RigidbodyType2D.Kinematic;
        rig.velocity = Vector2.zero;
        rig.angularVelocity = 0;
    }
    public void Centro(){
        if ( PodeMover == true){
            if (transform.position.x == 1.5f){
                capivara.flipX=false;
                transform.position = PosicaoCentro;
            }
            if (transform.position.x == -1.5f){
                capivara.flipX=true;
                transform.position = PosicaoCentro;
            }
        }
        else{
            Debug.Log("não pode se mover");
        }
    }
    public void Esquerda(){
        if ( PodeMover == true){
            capivara.flipX=false;
            transform.position = PosicaoEsquerda;
        }
        else{
            Debug.Log("não pode se mover");
        }
    }
    public void Direita(){
        if (PodeMover == true){
            capivara.flipX=true;
            transform.position = PosicaoDireita;
        }
        else{
            Debug.Log("não pode se mover");
        }
    }
}