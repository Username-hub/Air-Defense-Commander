using JetBrains.Annotations;

namespace DefaultNamespace.SaveLoadSystem
{
    [System.Serializable]
    public class LevelSaveData
    {
        public bool firstGameStart = true;
        public LevelState[] levelStates;
        public LevelSaveData(LevelState[] levelStates)
        {
            this.levelStates = new LevelState[levelStates.Length];
            this.levelStates = levelStates;
        }

        public LevelSaveData(int length)
        {
            this.levelStates = new LevelState[length];
            this.levelStates[0] = LevelState.Open;
            for (int i = 1; i < length; i++)
            {
                levelStates[i] = LevelState.Blocked;
            }
        }

        public LevelSaveData(bool firstGameStart)
        {
            this.firstGameStart = firstGameStart;
        }

        public void LevelFinished(int levelId)
        {
            levelStates[levelId-1] = LevelState.Finished;
            if(levelId < levelStates.Length)
                levelStates[levelId] = LevelState.Open;
        }
    }

    public enum LevelState
    {
        Finished,
        Open,
        Blocked
    }
}