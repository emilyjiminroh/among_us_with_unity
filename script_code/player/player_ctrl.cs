using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerCtrl : MonoBehaviour
{
    public GameObject joyStick, mainView, playView;
    public Settings settings_script;
    public Button Btn;
    public Sprite use, kill;
    public Text text_cool;

    Animator anim;
    GameObject coll;
    KillCtrl killctrl_script;
    

    //speed 변수 생성
    public float speed;

    public bool isCantMove,isMission;

    float timer;
    bool isCool,isAnim;


    //시작할 때의 실행
    private void Start()
    {
        anim = GetComponent<Animator>();

        // main camera를 Character의 자식으로 지정
        Camera.main.transform.parent = transform;
        // 처음 위치 지정 => 유니티에서의 main camera의 원래 위치를 상쇄해주기 위해
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);

        // 미션이라면
        if (isMission)
        {
            Btn.GetComponent<Image>().sprite = use;

            text_cool.text = "";

        }
        // 킬 퀘스트라면,
        else
        {
            killctrl_script = FindObjectOfType<KillCtrl>();

            Btn.GetComponent<Image>().sprite = kill;

            timer = 5;
            isCool = true;
        }
    }
    private void Update()
    {
        // 쿨타임
        if (isCool)
        {
            timer -= Time.deltaTime;
            text_cool.text = Mathf.Ceil(timer).ToString();

            if(text_cool.text == "0")
            {
                text_cool.text = "";
                isCool = false;
            }
        }

        if (isCantMove)
        {
            joyStick.SetActive(false);
        }
        else
        {
            //move함수 호출
            Move();
        }
        
        //애니메이션이 끝났다면
        if(isAnim && killctrl_script.kill_anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            killctrl_script.kill_anim.SetActive(false);
            killctrl_script.Kill();
            isCantMove = false;
            isAnim = false;
        }
    }

    // 캐릭터 움직임 관리 함수
   void Move()
   {
        if (settings_script.isJoyStick)
        {
            joyStick.SetActive(true);
        }
        else
        {
            joyStick.SetActive(false);
            //JoyStick 말고 화면을 클릭했는지 판단
            if (Input.GetMouseButton(0)) //좌측을 클릭 혹은 터치 했을 때
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    //dir 변수에 클릭한 위치 - 중간 위치를 빼주고 normalized 해준다
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f)).normalized;
                    //위치를 speed 값과 시간을 고려해 업데이트 해준다
                    transform.position += dir * speed * Time.deltaTime;

                    //animation 의 isWalk를 true로
                    anim.SetBool("isWalk", true);

                    //왼쪽으로 이동
                    if (dir.x < 0)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    //오른쪽으로 이동 (좌우반전)
                    else
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                }
               
            }
            //클릭하지 않는다면
            else
            {
                anim.SetBool("isWalk", false);
            }
        }

       
   }

    //캐릭터 삭제
    public void DestroyPlayer()
    {
        Camera.main.transform.parent = null;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Mission" && isMission)
        {
            coll = collision.gameObject;
            Btn.interactable = true;
        }

        if (collision.tag == "NPC" && !isMission)
        {
            coll = collision.gameObject;
            Btn.interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Mission")
        {
            coll = null;

            Btn.interactable = false;
        }

        if (collision.tag == "NPC" && !isMission && !isCool)
        {
            coll = null;
            Btn.interactable = false;
        }

    }

    // 버튼 누르면 호출
    public void ClickButton()
    {
        // 미션일 때
        if (isMission)
        {
            //MissionStart 호출
            coll.SendMessage("MissionStart");
        }

        //킬 퀘스트일 때
        else
        {
            Kill();
        }

        isCantMove = true;
        Btn.interactable = false;

    }

    void Kill()
    {
        //죽이는 애니메이션
        killctrl_script.kill_anim.SetActive(true);
        isAnim = true;

        //죽은 이미지 변경
        coll.SendMessage("Dead");

        // 죽은 NPC는 다시 죽일 수 없게
        coll.GetComponent<CircleCollider2D>().enabled = false;
    }

    // 미션 종료하면 호출
    public void MissionEnd()
    {
        isCantMove = false;
    }
}



