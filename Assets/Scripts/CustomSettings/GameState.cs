namespace pixelook
{
    public static class GameState
    {
        private static int _level;
        private static int _score;
        private static int _bonusScore;
        private static int _lives;

        public static bool IsGameRunning { get; set; }
        public static bool IsGameOver { get; set; }

        public static int Lives
        {
            get => _lives;
            
            set
            {
                _lives = value;
                
                EventManager.TriggerEvent(Events.LIVES_COUNT_CHANGED);

                if (_lives == 0)
                    EventManager.TriggerEvent(Events.GAME_OVER);
            }
        }
        
        public static string DeadPlayerName { get; set; }
        
        public static void OnApplicationStarted()
        {
            Lives = 3;
            IsGameRunning = false;
            IsGameOver = false;
        }

        public static void OnGameStarted()
        {
            Lives = 3;
            IsGameRunning = true;
            IsGameOver = false;
        }
    }
}