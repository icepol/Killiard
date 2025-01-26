namespace pixelook
{
    public static class Events
    {
        // level progression events
        public const string GAME_STARTED = "GameStarted";
        public const string GAME_FINISHED = "GameFinished";
        public const string GAME_OVER = "GameOver";
        public const string PLAYER_DIED = "PlayerDied";
        public const string PLAYER_CONTACT_SLOW = "PlayerContactSlow";
        public const string PLAYER_CONTACT_FAST = "PlayerContactFast";
        public const string PLAYER_CONTACT_MANTINEL = "PlayerContactMantinel";
        public const string PLAYER_CONTACT_BALL = "PlayerContactBall";
        public const string POWER_UP_COLLECTED = "PowerUpCollected";
        
        public const string TABLE_READY = "TableReady";
        public const string PLAYERS_READY = "PlayersReady";
        public const string ENVIRONMENT_READY = "EnvironmentReady";
        public const string LEVEL_READY = "GameReady";
        public const string LEVEL_FINISHED = "LevelFinished";
        
        public const string LIVES_COUNT_CHANGED = "LivesCountChanged";
        public const string SCORE_CHANGED = "ScoreChanged";

        // settings events
        public const string MUSIC_SETTINGS_CHANGED = "MusicSettingsChanged";
    }
}