using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    // 게임 신 넘기기, 게임 실행,게임 세팅 설정을 담당하는 스크립트 자세한 내용은 우준혁 내용 넣기바람

    public Slider soundSlider;  //사운드 조절
    public Slider mouseSlider;  //감도 조절
    public GameObject optionPanel;  
    public float maxTime;   //게임플레이 시간
    public Text timeText;   //0시부터 6시까지
    public AudioMixer audioMixer;
    public AudioSource audioSource;
    public AudioClip soundClip;
    public AudioClip breath;
    public GameObject diePanel;
    public TextManager textManager;
    public GameObject clearPanel;
    public GameObject helpPanel;


    private bool m_cursorIsLocked = false;
    public float timer = 1f;
    private float interval = 1f;

    public void Volume()
    {
        audioMixer.SetFloat("Sound", Mathf.Log10(soundSlider.value) * 20);
    }
    private void LockCursor()
    {

        if (m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!m_cursorIsLocked)
        {
             Cursor.lockState = CursorLockMode.None;
             Cursor.visible = true;
        }

    }
    public void Awake()
    {
        soundSlider.value = PlayerPrefs.GetFloat("Sound");
        mouseSlider.value = PlayerPrefs.GetFloat("MouseSensitivity");   //마우스 감도 가져오기
    }
    private IEnumerator Start()
    {
        clearPanel.SetActive(false);
        diePanel.SetActive(false);

        if (SceneManager.GetActiveScene().name == "MainScene")  //처음 시작할 때 옵션패널 끄기
        {
            optionPanel.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name != "MainScene")
        {
            OnMouseLock();
        }

        while (true)
        {
            yield return new WaitForSeconds(interval);  // 1초를 기다리는 WaitForSeconds 객체 생성

            if (timer == 0) //0시
            {
                timeText.text = "0:00";
            }
            else if (timer == maxTime / 6) //1시
            {
                Debug.Log("1시");
                timeText.text = "1:00";
                audioSource.PlayOneShot(soundClip);
            }
            else if (timer == maxTime / 6*2)  //2시
            {
                timeText.text = "2:00";
                audioSource.PlayOneShot(soundClip);
            }
            else if (timer == maxTime / 6*3)  //3시
            {
                timeText.text = "3:00";
                audioSource.PlayOneShot(soundClip);
            }
            else if (timer == maxTime / 6*4)  //4시
            {
                timeText.text = "4:00";
                audioSource.PlayOneShot(soundClip);
            }
            else if (timer == maxTime / 6*5)  //5시
            {
                timeText.text = "5:00";
                audioSource.PlayOneShot(soundClip);
            }
            else if (timer == maxTime)  //6시(클리어)
            {
                timeText.text = "6:00";
                StartCoroutine(Clear());
            }

            timer += 1f;
        }
    }

    void Update()
    {
        Volume();
        LockCursor();
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            OptionButton();
        }
        if (optionPanel)
        {
            PlayerPrefs.SetFloat("MouseSensitivity", mouseSlider.value); //감도 가져와서 슬라이더에 표시
            PlayerPrefs.SetFloat("Sound", soundSlider.value);
        }
    }

    public void StartButton()   //시작버튼(누르면 병원씬으로 이동)
    {
        SceneManager.LoadScene("HospitalScene");
        OnMouseLock();
    }
    public void OptionButton()  //옵션패널 활성화
    {
        optionPanel.SetActive(true);
        OffMouseLock();
        Time.timeScale = 0f;
    }
    public void ReStartButton()   //시작버튼(누르면 병원씬으로 이동)
    {
        SceneManager.LoadScene("MainScene");
    }
    public void Die()   //죽었을 때
    {
        diePanel.SetActive(true);
        OffMouseLock();
    }
    public IEnumerator Clear() //엔딩
    {
        clearPanel.SetActive(true);
        audioSource.PlayOneShot(breath);
        yield return new WaitForSeconds(6);
        textManager.Text("엔딩", "나는 병원에서 도망쳐 나왔다.");
        yield return new WaitForSeconds(2);
        textManager.Text("엔딩", "그날 있었던 일들은 그냥 모르는 척 하기로 했다.");
        yield return new WaitForSeconds(2);
        textManager.Text("엔딩", "나는 더 이상 그것들과는 얽히고 싶지않아...");
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("MainScene");
    }
    public void PanelOff()    //옵션패널 끄기
    {
        optionPanel.SetActive(false);
        if (SceneManager.GetActiveScene().name != "MainScene")
        {
            OnMouseLock();
        }
        Time.timeScale = 1f;
    }

    //@민우
    //마우스 잠금 해제
    public void OffMouseLock()
    {
        m_cursorIsLocked = false;
    }

    public void HelpPanelOn()
    {
        helpPanel.SetActive(true);
    }
    public void HelpPanelOff()
    {
        helpPanel.SetActive(false);
    }
    //마우스 잠금
    public void OnMouseLock()
    {
        m_cursorIsLocked = true;
    }

    public void ExitButton()    //게임끄기
    {
        Application.Quit();
    }
}
