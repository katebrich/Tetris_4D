using System;
using TetrisControlProject.Interfaces;

namespace TetrisControlProject
{
    /// <summary>
    /// Provides serie of levels where every level is faster than the previous one.
    /// Score is getting higher as well.
    /// </summary>
    [Serializable]
    class SpeedingUpLevelProvider : ILevelProvider
    {
        private ILevelSettings[] levelSettingsList; //Array to take next levels from.
        private int lastReturnedLevel = -1; //Points into levelSettingsList on last level returned.
        private int levelsCount = 10;

        /// <summary>
        /// Tells whether the game should continue after finishing the last level.
        /// </summary>
        public bool ContinueAfterLastLevel { get; set; }

        /// <summary>
        /// A constructor.
        /// Creates array of levels.
        /// </summary>
        public SpeedingUpLevelProvider()
        {
            createLevels();
            ContinueAfterLastLevel = true;
        }

        public ILevelSettings GetNextLevelSettings()
        {
            lastReturnedLevel++;
            if (lastReturnedLevel >= levelSettingsList.Length)
            {
                if (ContinueAfterLastLevel)
                    return levelSettingsList[lastReturnedLevel--];
                else
                    return null;
            }
            else return levelSettingsList[lastReturnedLevel];
        }

        public bool HasNextLevel()
        {
            return (lastReturnedLevel + 1 < levelSettingsList.Length);
        }

        private void createLevels()
        {
            levelSettingsList = new ILevelSettings[levelsCount];
            for (int i = 0; i < levelSettingsList.Length; i++)
            {
                int x = i * 250;
                IScoreProvider scoreProvider = new BasicScoreProvider((i+2)/2);
                int pieceShiftTime = 400000 / (x + 500) + 200;
                int levelTime = 30000; //0:30

                ILevelSettings settings = new BasicLevelSettings(scoreProvider, pieceShiftTime, levelTime);
                levelSettingsList[i] = settings;
            }
        }
    }
}
