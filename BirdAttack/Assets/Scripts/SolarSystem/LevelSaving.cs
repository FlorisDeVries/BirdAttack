using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class LevelSaving
{
    private static int levelsCleared = 1;
    private static int latestLevelCleared = 0;

    public static int LevelsCleared(){
        return levelsCleared;
    }

    public static void LevelCleared(int i){
        if(i <= latestLevelCleared) return;
        latestLevelCleared++;
        levelsCleared++;
    }
}
