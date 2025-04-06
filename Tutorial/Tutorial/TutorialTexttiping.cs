using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class TutorialTexttiping : MonoBehaviour
{
    public TMP_Text text;
    public string[] prologDialo;

    private string[] dialoges;
    private int talkNum;
    private Coroutine typingCoroutine;

    private void OnEnable()
    {
        StartTalk(prologDialo);
    }

    private void OnDisable()
    {
        StopAllCoroutines(); // ��Ȱ��ȭ�� �� �ڷ�ƾ ����
    }

    public void StartTalk(string[] talks)
    {
        dialoges = talks;
        talkNum = 0;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine); // ���� �ڷ�ƾ ����

        typingCoroutine = StartCoroutine(LoopTalk());
    }

    private IEnumerator LoopTalk()
    {
        while (gameObject.activeSelf) // UI�� Ȱ��ȭ ������ ���� �ݺ�
        {
            yield return StartCoroutine(Typing(dialoges[talkNum]));

            talkNum++;
            if (talkNum >= dialoges.Length)
                talkNum = 0; // �ٽ� ó������ ���ư�

            yield return new WaitForSeconds(1f); // ���� ��� �� ���ð�
        }
    }

    private IEnumerator Typing(string talk)
    {
        text.text = "";
        if (talk.Contains("  ")) talk = talk.Replace("  ", "\n");

        foreach (char letter in talk)
        {
            text.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1.5f); // ��� ���� �� ���
    }
}
