using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private GameObject gamePanel;
    
    // Start is called before the first frame update
    void Start()
    {
        // Pause the game until the player presses a key\
        Time.timeScale = 0;
        gamePanel.transform.localScale = Vector3.zero;
        
        // Animate this panel (pulse it) (even when the time scale is 0)
        transform.DOScale(Vector3.one * 1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo) .SetUpdate(true);
    }

    
    
    // Update is called once per frame
    void StartGame()
    {
        // tween this panel to 0
        // Twee in the game panel
        DOTween.Sequence()
            .Append(transform.DOScale(Vector3.zero, 0.5f))
            .Join(gamePanel.transform.DOScale(Vector3.one, 0.5f))
            .SetUpdate(true)
            .OnComplete(() =>
            {
                Time.timeScale = 1;
                Destroy(gameObject);
            });
    }
    
    void Update()
    {
        if (Input.anyKeyDown)
        {
            StartGame();
        }
    }
}
