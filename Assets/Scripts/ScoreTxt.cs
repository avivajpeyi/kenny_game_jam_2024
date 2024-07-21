using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScoreTxt : StaticInstance<ScoreTxt>
{
    
    TMPro.TextMeshProUGUI scoreText;
    
    void Start()
    {
        scoreText = GetComponent<TMPro.TextMeshProUGUI>();
        scoreText.text = "Score: 00";
    }

    public void SetScore(float score)
    {
        scoreText.text = "Score: " + score.ToString("00");
        // Use DoTween to animate the score (but bring it back to 1)
        
        
        scoreText.transform.DOPunchScale(Vector3.one * 0.25f, 0.25f).OnComplete(() =>
        {
            scoreText.transform.DOScale(Vector3.one, 0.1f);
        });

    }

}
