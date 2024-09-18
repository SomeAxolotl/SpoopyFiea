using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{

    public enum TriggerType { None, MonsterTrigger, OpenDoor};

    [SerializeField]private TriggerType type;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            switch(type)
            {
                case TriggerType.MonsterTrigger:
                    GameManager.onMonsterActivate?.Invoke();
                    break;
                default:
                    break;
            }

            gameObject.SetActive(false);
        }
    }
}
