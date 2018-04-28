using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreList {

    [System.Serializable]
    public class ScoreEntry
    {
        public string ID;
        public string Name;
        public string Score;
    }

    public ScoreEntry[] Top100Highscore;
	
}
