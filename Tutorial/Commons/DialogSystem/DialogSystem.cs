﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Speaker { Kim = 0, KO =1,Nar }

public class DialogSystem : MonoBehaviour
{
	[SerializeField]
	private	Dialog[]			dialogs;						// 현재 분기의 대사 목록
	[SerializeField]
	private	Image[]				imageDialogs;					// 대화창 Image UI
	[SerializeField]
	private	TextMeshProUGUI[]	textNames;						// 현재 대사중인 캐릭터 이름 출력 Text UI
	[SerializeField]
	private	TextMeshProUGUI[]	textDialogues;					// 현재 대사 출력 Text UI
	[SerializeField]
	private	GameObject[]		objectArrows;					// 대사가 완료되었을 때 출력되는 커서 오브젝트
	[SerializeField]
	private	float				typingSpeed;					// 텍스트 타이핑 효과의 재생 속도
	[SerializeField]
	private	KeyCode				keyCodeSkip = KeyCode.Space;    // 타이핑 효과를 스킵하는 키
	[SerializeField]
	private AudioSource audioSource;

    private	int					currentIndex = -1;
	private	bool				isTypingEffect = false;			// 텍스트 타이핑 효과를 재생중인지
	private	Speaker				currentSpeaker = Speaker.Kim;

	[SerializeField] private AudioClip clickSound;
	[SerializeField] private AudioSource effectAudiosource;
    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    public void Setup()
	{
		for ( int i = 0; i < 2; ++ i )
		{
			// 모든 대화 관련 게임오브젝트 비활성화
			InActiveObjects(i);
		}

		SetNextDialog();
	}

	public bool UpdateDialog()
	{
		if ( Input.GetKeyDown(keyCodeSkip) || Input.GetMouseButtonDown(0) )
		{
			effectAudiosource.Play();
			// 텍스트 타이핑 효과를 재생중일때 마우스 왼쪽 클릭하면 타이핑 효과 종료
			if ( isTypingEffect == true )
			{
				
				// 타이핑 효과를 중지하고, 현재 대사 전체를 출력한다
				StopCoroutine("TypingText");
				isTypingEffect = false;
				textDialogues[(int)currentSpeaker].text = dialogs[currentIndex].dialogue;
				// 대사가 완료되었을 때 출력되는 커서 활성화
				objectArrows[(int)currentSpeaker].SetActive(true);
				
                return false;
			}

			// 다음 대사 진행
			if ( dialogs.Length > currentIndex + 1 )
			{
				effectAudiosource.Stop();
				SetNextDialog();
			}
			// 대사가 더 이상 없을 경우 true 반환
			else
			{
                audioSource.Stop();
                // 모든 캐릭터 이미지를 어둡게 설정
                for ( int i = 0; i < 2; ++ i )
				{
					// 모든 대화 관련 게임오브젝트 비활성화
					InActiveObjects(i);
				}

				return true;
			}
		}

		return false;
	}

	private void SetNextDialog()
	{
		audioSource.Stop();
        effectAudiosource.PlayOneShot(clickSound);
        // 이전 화자의 대화 관련 오브젝트 비활성화
        InActiveObjects((int)currentSpeaker);

		currentIndex ++;

		// 현재 화자 설정
		currentSpeaker = dialogs[currentIndex].speaker;

		// 대화창 활성화
		imageDialogs[(int)currentSpeaker].gameObject.SetActive(true);

		// 현재 화자 이름 텍스트 활성화 및 설정
		textNames[(int)currentSpeaker].gameObject.SetActive(true);
		textNames[(int)currentSpeaker].text = dialogs[currentIndex].speaker.ToString();

		// 화자의 대사 텍스트 활성화 및 설정 (Typing Effect)
		textDialogues[(int)currentSpeaker].gameObject.SetActive(true);
        PlayVoice(dialogs[currentIndex].voiceClip);
        StartCoroutine(nameof(TypingText));
	}

	private void InActiveObjects(int index)
	{
		imageDialogs[index].gameObject.SetActive(false);
		textNames[index].gameObject.SetActive(false);
		textDialogues[index].gameObject.SetActive(false);
		objectArrows[index].SetActive(false);
	}

	private IEnumerator TypingText()
	{
		int index = 0;
		
		isTypingEffect = true;
        while ( index < dialogs[currentIndex].dialogue.Length )
		{
			textDialogues[(int)currentSpeaker].text = dialogs[currentIndex].dialogue.Substring(0, index);

			index ++;

			yield return new WaitForSeconds(typingSpeed);
		}

		isTypingEffect = false;

		objectArrows[(int)currentSpeaker].SetActive(true);
	}
    private void PlayVoice(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }

        if (clip != null) 
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}

[System.Serializable]
public struct Dialog
{
	public	Speaker		speaker;	// 화자
	[TextArea(3, 5)]
	public	string		dialogue;   // 대사
    public AudioClip voiceClip; //오디오
}

