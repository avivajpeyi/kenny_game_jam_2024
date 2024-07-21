using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HealthUi : StaticInstance<HealthUi>
{
    TMPro.TextMeshProUGUI txt;
    
    // var for heart emoji \u2665
    string heart = "\u2665";
    
    void Start()
    {
        txt = GetComponent<TMPro.TextMeshProUGUI>();
        txt.text =  heart + heart + heart;
    }

    public void SetHealth(float hp)
    {
        // set the text to hp number of hearts
        txt.text = "";
        for (int i = 0; i < hp; i++)
        {
            txt.text += heart;
        }
        
        // Use DoTween to animate the hearts
        txt.transform.DOPunchScale(Vector3.one * 0.5f, 0.5f);
        
    }
}
