using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mission4 : MonoBehaviour
{
    public Transform numbers;
    public Color blue;

    MissionCtrl missionCtrl_script;
    Animator anim;
    PlayerCtrl playerCtrl_script;

    int count;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        missionCtrl_script = FindObjectOfType<MissionCtrl>();

    }

    //미션 시작
    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerCtrl_script = FindObjectOfType<PlayerCtrl>();

        // 초기화
        for(int i = 0; i < numbers.childCount; i++)
        {
            numbers.GetChild(i).GetComponent<Image>().color = Color.white;
            numbers.GetChild(i).GetComponent<Button>().enabled = true;
        }

        // 숫자 랜덤 배치
        for (int i = 0; i < 10; i++)
        {
            Sprite temp = numbers.GetChild(i).GetComponent<Image>().sprite;

            int rand = Random.Range(0, 10);
            numbers.GetChild(i).GetComponent<Image>().sprite = numbers.GetChild(rand).GetComponent<Image>().sprite;

            numbers.GetChild(rand).GetComponent<Image>().sprite = temp;
        }
        count = 1;
    }

    //엑스 버튼 누르면 호출
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerCtrl_script.MissionEnd();
    }

    // 숫자 버튼 누르면 호출
    public void ClickNumber()
    {
        if(count.ToString() == EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name)
        {
            //색 변경
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = blue;

            //버튼 비활성화
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().enabled = false;


            count++;

            // 성공여부 체크
            if(count == 11)
            {
                Invoke("MissionSuccess", 0.2f);
            }
        }
    }
    
    //미션 성공하면 호출
    public void MissionSuccess()
    {
        ClickCancle();
        missionCtrl_script.MissionSuccess(GetComponent<CircleCollider2D>());

    }

}