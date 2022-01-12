using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public bool isJoyStick;
    public Image touchBtn, joyStickBtn;
    public Color blue;
    public PlayerCtrl playerCtrl_script;

    GameObject mainView, missionView;

    private void Start()
    {
        mainView = playerCtrl_script.mainView;
        missionView = playerCtrl_script.missionView;   
    }

    // 설정 버튼을 누르면 호출
    public void ClickSetting()
    {
        gameObject.SetActive(true);
        playerCtrl_script.isCantMove=true;
    }

    //게임으로 돌아가기 버튼 누르면 호출
    public void ClickBack()
    {
        gameObject.SetActive(false);
        playerCtrl_script.isCantMove = false;

    }

    // 터치이동을 누르면 호출
    public void ClickTouch()
    {
        //print("click touch");
        isJoyStick = false;
        touchBtn.color = blue;
        joyStickBtn.color = Color.white;
    }

    // 조이스틱을 누르면 호출
    public void ClickJoyStick()
    {
        //print("click joystick");
        isJoyStick = true;
        touchBtn.color = Color.white;
        joyStickBtn.color = blue;
    }

    // 게임 나가기 버튼 누르면 호출
    public void ClickQuit()
    {
        mainView.SetActive(true);
        missionView.SetActive(false);

        //캐릭터 삭제
        playerCtrl_script.DestroyPlayer();
    }
}
