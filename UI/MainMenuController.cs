using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    private MainMenuModel model;
    private MainMenuView view;

    void Start()
    {
        model = new MainMenuModel();
        view = FindObjectOfType<MainMenuView>();

        // Model�� View ����
        model.OnUserNameChanged += view.SetUserName;

        // �ʱ� �� ����
        model.UserName = "Codex of Ruin";
        UIManager.Instance.Gold = 1000;
        UIManager.Instance.Cash = 500;

        // ��ư �̺�Ʈ ����
        view.playButton.onClick.AddListener(OnPlayClicked);
        view.friendButton.onClick.AddListener(OnFriendClicked);
        view.ShopButton.onClick.AddListener(OnShopClicked);

        view.settingsButton.onClick.AddListener(OnSettingsClicked);
        view.GuildButton.onClick.AddListener(OnGuildClicked);
    }

    void OnPlayClicked()
    {
        Debug.Log("���� ����!");
        SceneManager.LoadScene("Prolog01");
    }

    void OnSettingsClicked()
    {
        Debug.Log("���� ȭ�� ����");
    }

    void OnFriendClicked()
    {
        Debug.Log("����â ����");
    }
    void OnShopClicked()
    {
        Debug.Log("���� ����");
    }
    void OnGuildClicked()
    {
        Debug.Log("��� ����");
    }

}
