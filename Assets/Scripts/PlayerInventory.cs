using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] GameObject _activeDistraction;
    bool _hasDistraction = false;

    public bool hasDistraction
    {
    get { return _hasDistraction; } 
    }

    public bool AddtoInventory(GameObject g)
    {
        if (_hasDistraction)
            return false;

        _hasDistraction = true;
        g.SetActive(false);

        return true;
    }

    public void DropItem()
    {
        if (!_hasDistraction) return;

        _hasDistraction = false;
    }
}
