using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _collectablesText;
    [SerializeField]
    private PlayerCollection _playerCollection;
    [SerializeField]
    private GameObject _gameOverScreen;
    [SerializeField]
    private Image _staminaBar;

    [SerializeField] private Color _staminaColor, _exhaustedColor;

    public static Action gameOver;
    public static Action<float,bool> sprintStatus;

    //private static UIManager _instance = null;

    //public static UIManager Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = FindObjectOfType<UIManager>();
    //            DontDestroyOnLoad(_instance);
    //        }
    //        else
    //        {
    //            Destroy(_instance);
    //            _instance = Instantiate(_UIManagerPrefabStatic).GetComponent<UIManager>();
    //            DontDestroyOnLoad(_instance);
    //        }

    //        return _instance;
    //    }
    //}

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _staminaBar = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        _gameOverScreen = transform.GetChild(1).gameObject;
        _gameOverScreen.SetActive(false);
        Debug.Log("Setting gameover screen to false");
    }

        private void AddCollectable()
    {
        _collectablesText.text = "Collectables : " + _playerCollection.CollectableCount;
    }

    public void UpdateStaminaGauge(float staminaRatio, bool exhausted)
    {
        Debug.Log("Updating stamina with "+staminaRatio);
        _staminaBar.fillAmount = staminaRatio;

        if(exhausted)
        {
            _staminaBar.color = _exhaustedColor;
        }
        else
            _staminaBar.color= _staminaColor;
    }

    private void OnEnable()
    {
        

        /*_gameOverScreen = GameObject.Find("GameOver");*/
    }
    // Start is called before the first frame update
    void Start()
    {
        /*Collectable.OnCollected += AddCollectable;*/
        gameOver += GameOverScreen;
        sprintStatus += UpdateStaminaGauge;
        SceneManager.sceneLoaded += OnSceneLoaded;
        _staminaBar = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        /*_gameOverScreen.SetActive(false);*/
    }

    void GameOverScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        transform.GetChild(1).gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        /*Collectable.OnCollected-=AddCollectable;*/
        gameOver -= GameOverScreen;
        sprintStatus -= UpdateStaminaGauge;
    }

    //MOVE THIS ELSEWHERE
    public void RestartLevel()
    {
        _gameOverScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
