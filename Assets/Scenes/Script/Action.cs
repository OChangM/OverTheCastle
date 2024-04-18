using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public Animator Mov;
    //ddddddddddddddddddddddddddddddd
    private float yRange = 3;
    private bool ground = false;
    const float STOP_JUMP = 0.0F;
    const float SPEED_JUMP = 5.0F;
    float SPEED_MOVE = 3.0F;
    public float delyTime = 10f;
    public bool isDelay;
    public float accumTime;
    public LayerMask layer;
    public float coolTime = 1.5f;
    Rigidbody2D rb;
    bool leftPressed = false;
    bool rightPressed = false;
    bool CanSwingSword = false;
    bool CanMoving =false;
    bool isJumping; // 점프 유무 변수 선언
    bool Death; //사망처리(조작금지를 위해)
    public string SceneToLoad;


//dddddddddddddddddddddddddddddddd
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Mov = GetComponent<Animator>();
        Mov.SetBool("Idle",false);
        isJumping = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Death == true)//사망처리(조작금지를 위해)
        {
            return;
        }
        
        //
        float dist = SPEED_MOVE *Time.deltaTime;
        Vector2 pos = transform.position;

        //전진 애니메이션
        if(Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(3, 3, 1);
            Mov.ResetTrigger("Attack1");
            Mov.SetTrigger("Run");
            pos.x += dist;
            
        }
        //후진 애니메이션
        if(Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-3, 3, 1);
            Mov.ResetTrigger("Attack1");
            Mov.SetTrigger("Run");
            pos.x -= dist;
        }
        transform.position = pos;

            //정지
        if(Input.anyKey==false)
        {
            Mov.SetTrigger("Idle");
            CanSwingSword = true;

            //Mov.SetBool("Stop",false);
        }
        //공격

        
        if(Input.GetMouseButtonDown(0))
        {
            CanSwingSword = true;
            leftPressed = false;
            rightPressed = false;
            Mov.SetTrigger("Attack1");
            SPEED_MOVE = 0.0F; //공격시 이동 중지
         }
         SPEED_MOVE = 3.0F;
        
        //점프 예정
        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            isJumping = true;
            Mov.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, SPEED_JUMP);
                    //전진 애니메이션
            if(Input.GetKey(KeyCode.D))
                {
                    transform.localScale = new Vector3(3, 3, 1);
                    Mov.ResetTrigger("Attack1");
                    
                    pos.x += dist;
            
                }
            //후진 애니메이션
            if(Input.GetKey(KeyCode.A))
                {
                    transform.localScale = new Vector3(-3, 3, 1);
                    Mov.ResetTrigger("Attack1");
                    
                    pos.x -= dist;
                }
        }
        
    }

    IEnumerator OnCollisionEnter2D(Collision2D collision2D)
        {
            if (collision2D.gameObject.CompareTag("Ground"))
            {
                isJumping = false;
            }
            if(collision2D.collider.gameObject.CompareTag("DeadTile"))
            {
                Debug.Log("Killed");
                Mov.SetTrigger("Death");
                Death = true;
                yield return new WaitForSeconds(2);
                SceneManager.LoadScene("Playing"); // Scene 재시작
            }
        }

}
