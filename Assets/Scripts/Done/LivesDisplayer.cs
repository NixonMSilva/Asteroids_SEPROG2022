using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesDisplayer : MonoBehaviour
{
    [SerializeField] GameObject[] _lives;

    public void UpdateLivesDisplay (int noLives)
    {
        int i;

        for (i = 3; i > 0; --i)
        {
            if (i > noLives)
            {
                _lives[i - 1].SetActive(false);
            }
            else
            {
                _lives[i - 1].SetActive(true);
            }   
        }
    }
}
