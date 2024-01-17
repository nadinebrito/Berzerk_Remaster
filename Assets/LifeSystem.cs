using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{

    public List<GameObject> lifes;
    
    private void Start()
    {
        lifes = new List<GameObject>();
        
        for (int i = 0; i < transform.childCount; i++)
        {
            lifes.Add(transform.GetChild(i).gameObject);
        }
    }

    public void LossLife(int ActLife)
    {
        if (ActLife > 6)
        {
            lifes[5].gameObject.SetActive(false);
        }
        else
        {
            lifes[ActLife].gameObject.SetActive(false);
        }
        
    }
    
    public void GainLife(int ActLife)
    {
        if (ActLife > 6)
        {
            return;
        }
        lifes[ActLife-1].gameObject.SetActive(true);
    }
    
    public void NewGame()
    {
        for (int i = 0; i < 6; i++)
        {
            lifes[i].gameObject.SetActive(false);
        }

        for (int j = 0; j < 3; j++)
        {
            lifes[j].gameObject.SetActive(true);
        }
    }
}