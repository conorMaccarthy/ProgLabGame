using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible
{
    public string _name;
    public int _healthRestored;
    public float _moveSpeed;

    public Collectible(string name, int healthRestored, float moveSpeed)
    {
        _name = name;
        _healthRestored = healthRestored;
        _moveSpeed = moveSpeed;
    }

    public void RestoreHealth()
    {
        GameObject.Find("Character").GetComponent<CharacterStats>().Heal(_healthRestored);
    }

    public void IncreaseMoveSpeed()
    {

    }
}
