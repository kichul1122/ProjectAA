namespace AA
{
    public enum EStartUpSceneState
    {
        LoadStartUpData,
        LoadPopup,
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

    public enum ECharacterState
    {
        None,
        Idle,
        Move,
        Attack,
        Jump,
    }
}