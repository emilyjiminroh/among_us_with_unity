using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject missionView;
    
    //게임 종료 버튼 누르면 호출
    public void ClickQuit()
    {
        // 유니티 에디터
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        //안드로이드
#else
Application.Quit();
#endif
    }
    //미션 버튼 누르면 호출
    public void ClickMission()
    {
        gameObject.SetActive(false);
        missionView.SetActive(true);

        GameObject player = Instantiate(Resources.Load("Character"),new Vector3(9,-1,0), Quaternion.identity) as GameObject;
        player.GetComponent<PlayerCtrl>().mainView = gameObject;
        player.GetComponent<PlayerCtrl>().missionView = missionView;
    }
}
