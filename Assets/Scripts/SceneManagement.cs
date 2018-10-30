using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

    //Buttons used in menu
    public Button StartButton1;
    public Button StartButton2;
    public Button ExitButton;
    public Button MenuButton;
    public Button CreditsButton;
    //public Button RestartButton;
    [SerializeField]
    AudioClip pressed;
    static AudioClip _pressed;
    [SerializeField]
    AudioSource source;
    static AudioSource _source;

    //the number of hiders the seeker has found
    public static int hidersFound = 0;

    // Use this for initialization
    void Start()
    {

        Screen.SetResolution(1920, 1080, true);

        AudioClip _pressed=pressed;
        _source = source;
        //dont destroy this game object
        DontDestroyOnLoad(this);

        //create code for buttons, buttons only work if there is an object attached to it
        if (StartButton1 != null)
        {
            Button btn = StartButton1.GetComponent<Button>();
            btn.onClick.AddListener(TaskOnClick);
        }
        if (StartButton2 != null)
        {
            Button btn = StartButton2.GetComponent<Button>();
            btn.onClick.AddListener(TaskOnClick5);
        }
        if (ExitButton != null)
        {
            Button btn1 = ExitButton.GetComponent<Button>();
            btn1.onClick.AddListener(TaskOnClick1);
        }
        if (MenuButton != null)
        {
            Button btn2 = MenuButton.GetComponent<Button>();
            btn2.onClick.AddListener(TaskOnClick2);
        }
        if (CreditsButton != null)
        {
            Button btn3 = CreditsButton.GetComponent<Button>();
            btn3.onClick.AddListener(TaskOnClick3);
        }
        //if (RestartButton != null)
        //{
        //    Button btn4 = RestartButton.GetComponent<Button>();
        //    btn4.onClick.AddListener(TaskOnClick4);
        //}
    }

    // Update is called once per frame
    void Update()
    {
    }

    //loads instructions
    void TaskOnClick()
    {
        source.PlayOneShot(pressed, .25f);
        SceneManager.LoadScene("Instructions");
    }

    //exits game
    void TaskOnClick1()
    {
        source.PlayOneShot(pressed, .25f);
        Application.Quit();
    }

    //returns to start menu
    void TaskOnClick2()
    {
        source.PlayOneShot(pressed, .25f);
        SceneManager.LoadScene("StartMenu");
    }

    //loads credits
    void TaskOnClick3()
    {
        source.PlayOneShot(pressed, .25f);
        SceneManager.LoadScene("Credits");
    }

    ////loads checkpoint
    //void TaskOnClick4()
    //{
    //    SceneManager.LoadScene("Level" + StatManager.level);
    //    StatManager.hasKey = false;
    //}

    //loads game
    void TaskOnClick5()
    {
        source.PlayOneShot(pressed, .25f);
        SceneManager.LoadScene("Level1");
    }

    //loads win scene
    public static void Win()
    {
        _source.PlayOneShot(_pressed, .25f);
        SceneManager.LoadScene("Win");
    }
    //loads gameover scene
    public static void GameOver()
    {
        _source.PlayOneShot(_pressed, .25f);
        SceneManager.LoadScene("GameOver");
    }

}
