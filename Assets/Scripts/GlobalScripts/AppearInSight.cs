using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearInSight : MonoBehaviour {


    public float appearDelay, appearTime;

    Renderer render;
    bool allow = false;
    bool appeared = false;

	void Start () {
        render = GetComponent<Renderer>();
        allow = true;
        SetDisplay(0);
    }


    float delayCounter = 0;
    float appearCounter = 0;
    void Update () {
		if (allow)
        {
            if (render.isVisible && !appeared)
            {
                if (delayCounter < appearDelay)
                {
                    delayCounter += Time.deltaTime;
                }
                else
                {
                    if (appearCounter < appearTime)
                    {
                        appearCounter += Time.deltaTime;
                    }
                    else
                    {
                        appearCounter = appearTime;
                        appeared = true;
                    }

                    SetDisplay(appearCounter / appearTime);
                }
            }
        }
	}
    
    void SetDisplay (float alpha)
    {
        Color currCol = render.material.color;
        currCol.a = alpha;
        render.material.color = currCol;
    }
}
