using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionCtrl : MonoBehaviour
{
    public Slider guage;
    public CircleCollider2D[] colls;
    public GameObject text_anim, mainView;

    int missionCount;

    // 미션 초기화
    public void MissionReset()
    {
        guage.value = 0;
        missionCount = 0;

        for(int i = 0; i < colls.Length; i++)
        {
            colls[i].enabled = true;
        }
        text_anim.SetActive(false);

    }

    //미션 성공하면 호출
    public void MissionSuccess(CircleCollider2D coll)
    {
        missionCount++;

        guage.value = missionCount / 7f;

        //성공한 미션은 다시 플레이 X
        coll.enabled = false;

        // 성공여부 체크 + text_success 실행
        if(guage.value == 1)
        {
            text_anim.SetActive(true);

            Invoke("Change", 1f);
        }
    }



    // 화면 전환
    public void Change()
    {
        mainView.SetActive(true);
        gameObject.SetActive(false);

        //캐릭터 삭제
        FindObjectOfType<PlayerCtrl>().DestroyPlayer();
    }

}
