namespace AA
{
    public enum EStartUpState
    {
        AppVersion,
        DownloadApp,
        MetaVersion,
        LoadMeta,
        Login,
        CreateUserData,
        LoadServerData,
        GamePlay,
    }

    public enum ESceneName
    {
        Empty,

        StartUp,
        Lobby,
        Field,
        Dungeon,
    }

    public enum ELabel
    {
        Character,
        Map,
        Texture,
        UI,
        Sprite,
        Default,
        Atlas,
    }

    public enum ECharacter
    {
        None,
        Player,
        Enemy,
        Max
    }
}