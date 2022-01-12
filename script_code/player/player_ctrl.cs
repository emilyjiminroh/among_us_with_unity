using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerCtrl : MonoBehaviour
{
    public GameObject joyStick, mainView, missionView;
    public Settings settings_script;
    public Button Btn;

    Animator anim;
    GameObject coll;
    

    //speed 변수 생성
    public float speed;

    public bool isCantMove;


    //시작할 때의 실행
    private void Start()
    {
        anim = GetComponent<Animator>();

        // main camera를 Character의 자식으로 지정
        Camera.main.transform.parent = transform;
        // 처음 위치 지정 => 유니티에서의 main camera의 원래 위치를 상쇄해주기 위해
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);
    }
    private void Update()
    {
        if (isCantMove)
        {
            joyStick.SetActive(false);
        }
        else
        {
            //move함수 호출
            Move();
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
        if(collision.tag == "Mission")
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
    }
    // 버튼 누르면 호출
    public void ClickButton()
    {
        //MissionStart 호출
        coll.SendMessage("MissionStart");
        isCantMove = true;
        Btn.interactable = false;

    }

    // 미션 종료하면 호출
    public void MissionEnd()
    {
        isCantMove = false;
    }
}



