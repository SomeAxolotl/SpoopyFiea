using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject _monster;
    private static bool[] questItems;

    [SerializeField]private Transform _collectableList;

    public static Action onMonsterActivate;

    private static int _requiredNumCollectable;


    public static void CollectableObtained(int index)
    {
        if(index >= questItems.Length || index < 0)
        {
            Debug.LogError("Attempting to access out of bounds Collectable");
            return;
        }

        if (!questItems[index])
            _requiredNumCollectable--;

        questItems[index] = true;
        Debug.Log("Obtained item " + index);

        if(_requiredNumCollectable == 0)
        {
            Debug.Log("Collected everything");
        }
    }

    
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Calling Awake in game manager");
        onMonsterActivate += SpawnMonster;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _monster = GameObject.FindWithTag("AI");
        if (_monster == null)
            Debug.Log("Monster is null");
        _monster.SetActive(false);
        Debug.Log("Scene has loaded");

       GameObject[] interactArray = GameObject.FindGameObjectsWithTag("Interactable");

        foreach(GameObject g in interactArray)
        {
            if(g.name.CompareTo("Collectables") == 0)
            {
                _collectableList = g.transform;
                questItems = new bool[_collectableList.childCount];
                _requiredNumCollectable = _collectableList.childCount;
                break;
            }
        }
    }

    void SpawnMonster()
    {
        _monster.SetActive(true);
    }
}
