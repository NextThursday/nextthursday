﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour {


    public MasterReferences master;
    public TextMesh tempDisplayProgress, tempDisplayDay;


    public List<string> days;


    bool allow = false;
    
    float weekCount;

    int previousDayCount = -1;

    public void StartCount ()
    {
        weekCount = 0;
        allow = true;
    }

	
	void Update () {
        if (allow && !master.controls.isTutorial)
        {

            if (weekCount >= master.controls.weekLength)
            {
                allow = false;
                End();
            }
            else
            {
                weekCount += Time.deltaTime;
                DisplayProgress(weekCount / master.controls.weekLength);

                int dayCount = Mathf.FloorToInt(weekCount / master.controls.weekLength * 7);
                if (dayCount != previousDayCount)
                {
                    previousDayCount = dayCount;
                    DisplayDay(dayCount);
                }

            }



        }		
	}

    void End ()
    {
        master.saveHandler.NextScene();
    }

    void DisplayDay (int day)
    {
        tempDisplayDay.text = "" + GetDay(day);
    }

    void DisplayProgress (float progress)
    {
        progress = Mathf.FloorToInt(progress * 100f);

        tempDisplayProgress.text = (100 - progress) + "s";
    }


    //HELPER

    string GetDay (int day)
    {
        return days[day];
    }
}
