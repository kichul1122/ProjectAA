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
        CreateNewModel,
        LoadModelFromServer,
        InitializeModel,
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
        Weapon,
        Projectile,
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

    public enum EStatAttribute
    {
        Attack,
        MaxHp,
        MoveSpeed,

        CriticalPercent,
        CriticalDamagePrercent,
    }

    public enum EStat
    {
        Attack,
        AttackPercent,

        MoveSpeed,
        MoveSpeedPercent,

        MaxHp,
        MaxHpPercent,

        CriticalPercent,
        CriticalDamagePrercent,
    }
}