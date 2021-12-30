using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CountDownController : MonoBehaviour
{
    #region Singleton
    private static CountDownController instance;
    public static CountDownController Instance 
    {
        get
        {
            if (instance == null)
            {
                instance = new CountDownController();
            }
            return instance;
        }
    }

    public Dictionary<string, GameObject> DicCount;
    #endregion

    [Header("CountDown IMG")]
    [SerializeField] GameObject count_01;
    [SerializeField] GameObject count_02;
    [SerializeField] GameObject count_03;
    [SerializeField] GameObject count_Start;

    Dictionary<int, GameObject> dicCount;

    #region public variable
    public int timer = 0;
    public bool isCountEnd = false;
    #endregion

    #region LifeCycle
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        /*
        dicCount = new Dictionary<int, GameObject>()
        {
            {1, count_01},
            {2, count_02},
            {3, count_03},
            {0, count_Start}
        };
        */
    }
    #endregion

    #region public Method
    /// <summary>
    /// 카운트다운 초기화 처리
    /// </summary>
    public void Counting_Init()
    {
        timer = 0;

        count_01.SetActive(false);
        count_02.SetActive(false);
        count_03.SetActive(false);
        count_Start.SetActive(false);
 
    }

    public void StartCountDown()
    {

        Debug.Log(timer);
        if (timer == 0)
        {
            Time.timeScale = 0.0f;
            // 게임 시작 시 타이머 정지

        }

        if (timer <= 170)
        {
            timer++;
            if (timer  > 50)
            {
                //ImageOn(3);
                count_03.SetActive(true);
            }
            if (timer > 90)
            {
                //ImageOff(3);
                //ImageOn(2);
                count_03.SetActive(false);
                count_02.SetActive(true);
            }
            if (timer > 130)
            {
                //ImageOff(2);
                //ImageOn(1);
                count_02.SetActive(false);
                count_01.SetActive(true);
            }
            if (timer >= 170)
            {
                //ImageOff(1);
                //ImageOn(0);
                count_01.SetActive(false);
                count_Start.SetActive(true);
                StartCoroutine(HideCountDown());
                Time.timeScale = 1.0f;
                isCountEnd = true;
            }
        }
    }

    public void ImageOn(int _num)
    {
        if (dicCount.TryGetValue(_num, out GameObject value))
        {
            value.SetActive(true);
        }
    }

    public void ImageOff(int _num)
    {
        if (dicCount.TryGetValue(_num, out GameObject value))
        {
            value.SetActive(false);
        }
    }

    public void HideCountText()
    {
        count_Start.SetActive(false);
    }
    #endregion

    #region Coroutine Method
    IEnumerator HideCountDown()
    {
        yield return new WaitForSeconds(1.0f);
        count_Start.SetActive(false);
    }
    #endregion
}
