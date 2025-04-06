using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuView : MonoBehaviour
{
    public TextMeshProUGUI userNameText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI cashText;

    //���� UI
    public Button playButton; //���丮 ���� ��ư
    public Button friendButton; //���� ��ư
    public Button InventButton;//���� ��ư
    public Button MissionButton; //�̼� ��ư
    public Button AchiveButton; //���� ��ư
    public Button ShopButton; //���� ��ư
    public Button PackageButton; //��Ű�� ��ư
    //������ ��� UI
    public Button settingsButton; //���� ��ư
    public Button mailButton; //���� ��ư
    public Button newsButton; //�˸� ��ư
    //���� ��� UI
    public Button myRoomButton; //���̷� ��ư
    public Button GuildButton; //��� ��ư
    public void SetUserName(string userName)
    {
        userNameText.text = userName;
    }
  
}
