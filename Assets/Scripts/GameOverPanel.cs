using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverPanel : StaticInstance<GameOverPanel>
{
    bool isGameOver = false;
    


    public void TriggerGameOver()
    {
        // Stop all physics before running the tween
        Time.timeScale = 0;

        // DoTween to scale the panel up and make it visible
        transform.DOScale(1, 2f)
            .SetEase(Ease.OutElastic)
            .SetUpdate(true).OnComplete(
                () => isGameOver = true
            );
    }

    private void Update()
    {
        if (isGameOver && Input.anyKey)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}