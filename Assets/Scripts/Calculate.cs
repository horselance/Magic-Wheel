using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Calculate
{
    public static float GetSliceAngle(int sliceNo)
    {
        int wheelOffset = 0;
        int wheelOfSlice = 0;
        WheelSlice wheelSlice = GameManager.Instance.wheelSliceList[sliceNo];
        if (wheelOfSlice == 1)
        {
            wheelOffset = 7;
        }
        if (wheelOfSlice == 2)
        {
            wheelOffset = 15;

        }
        float minAngle = wheelSlice.sliceSize * 22.5f * (float)(sliceNo - wheelOffset);
        float maxAngle = minAngle + (22.5f * wheelSlice.sliceSize);
        float angle = Random.Range(minAngle, maxAngle) - 2430f;
        return angle;
    }

    public static float GetNextSliceAngle(int spinNo)
    {
        float minAngle;
        float maxAngle;
        if (spinNo == 0)
        {
            maxAngle = 360f;
            minAngle = 270f;
        }
        else
        {
            maxAngle = 90f;
            minAngle = 45f;
        }
        float angle = Random.Range(minAngle, maxAngle) - 2430f;
        return angle;
    }
   
}
