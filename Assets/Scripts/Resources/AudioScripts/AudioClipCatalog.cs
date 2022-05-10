using UnityEngine;

public class AudioClipCatalog : MonoBehaviour
{
    public enum SfxName
    {
        LandingNormal,
        JumpingNormal,
        ConstantBuzz,
        HollowWoodKnock,
        ChestOpening,
        MetalFalling,
        PortcullisOpening,
        PortcullisClosing,
        CoinCollecting,
        ContinuousPressurePlateBuzz,
        WaterSplash,
        KeyObtained,
        KeyTurning,
        MenuClicked,
    }

    public enum MusicName
    {
        The_Journey_Is_The_Treasure,
        World_Of_Nowhere,
        Call_For_Love,
        Music_Of_Something,
        Day_Of_What,
        A_Day,
        Easy_Dreams,
        Gift,
        Belong_To_Her_Place,
        Free_Tonight,
        Comfortless_Seasons,
        Good_Moves,
        Get_Out,
        Give_Her_Shadow,
        I_Dont_Care_About_Place,
        Lets_Do_This,
        Because_Of_Yesterday,
        No_One_Needs_Silence,
        On_My_Mind,
        She_Will_Try,
        Prayer_Of_My_Life,
        Remember_Your_Place,
        Story_For_Money,
    }

    public AudioClip GetSfxClip(AudioClipCatalog.SfxName _mySfxName)
    {
        AudioClip _audioClip = (AudioClip)Resources.Load("Sounds/Sfx/zapsplat_impact_thud_light_small_soft_object_001_17766");
        switch (_mySfxName)
        {
            default:
            case AudioClipCatalog.SfxName.LandingNormal:
                _audioClip = (AudioClip)Resources.Load("Sounds/Sfx/zapsplat_impact_thud_light_small_soft_object_001_17766");
                break;
            case AudioClipCatalog.SfxName.JumpingNormal:
                _audioClip = (AudioClip)Resources.Load("Sounds/Sfx/jump exported");
                break;
            case AudioClipCatalog.SfxName.ConstantBuzz:
                _audioClip = (AudioClip)Resources.Load("Sounds/Sfx/smartsound_ANIMAL_HORNET_Hum_Glass_Constant_Soft_01-trimmed");
                break;
            case AudioClipCatalog.SfxName.HollowWoodKnock:
                _audioClip = (AudioClip)Resources.Load("Sounds/Sfx/zapsplat_foley_wood_3x_knocks_hollow_70874-trimmed");
                break;
            case AudioClipCatalog.SfxName.ChestOpening:
                _audioClip = (AudioClip)Resources.Load("Sounds/Sfx/344_audio_treasure_chest_open_gold_victory_fantasy_1596-trimmed");
                break;
            case AudioClipCatalog.SfxName.MetalFalling:
                _audioClip = (AudioClip)Resources.Load("Sounds/Sfx/transportation_bicycle_fall_over_001-trimmed");
                break;
            case AudioClipCatalog.SfxName.PortcullisOpening:
                _audioClip = (AudioClip)Resources.Load("Sounds/Sfx/344_audio_drawbridge_raise_creak_slam_drop_284-trimmed");
                break;
            case AudioClipCatalog.SfxName.PortcullisClosing:
                _audioClip = (AudioClip)Resources.Load("Sounds/Sfx/344_audio_drawbridge_lower_creak_slam_drop_284-trimmed");
                break;
            case AudioClipCatalog.SfxName.CoinCollecting:
                _audioClip = (AudioClip)Resources.Load("Sounds/Sfx/jessey_drake_oldschool_COIN_COLLECT_video_retro_game_chip_set_8BIT_JD-trimmed");
                break;
            case AudioClipCatalog.SfxName.ContinuousPressurePlateBuzz:
                _audioClip = (AudioClip)Resources.Load("Sounds/Sfx/smartsound_WEAPONS_ARTILLERY_Missile_Truck_Move_Loop-trimmed");
                break;
            case AudioClipCatalog.SfxName.WaterSplash:
                _audioClip = (AudioClip)Resources.Load("Sounds/Sfx/zapsplat_multimedia_game_designed_water_splash_002_26382-trimmed");
                break;
            case AudioClipCatalog.SfxName.KeyObtained:
                _audioClip = (AudioClip)Resources.Load("Sounds/Sfx/zapsplat_multimedia_game_sound_coins_collect_several_at_once_002_40813-trimmed");
                break;
            case AudioClipCatalog.SfxName.KeyTurning:
                _audioClip = (AudioClip)Resources.Load("Sounds/Sfx/344_audio_mortice_key_turn_lock_3_1445-trimmed");
                break;
            case AudioClipCatalog.SfxName.MenuClicked:
                _audioClip = (AudioClip)Resources.Load("Sounds/Sfx/esm_perfect_clean_app_button_click_organic_simple_classic_game_click.mp3");
                break;
        }
        return _audioClip;
    }

    public AudioClip GetMusicClip(AudioClipCatalog.MusicName _myMusicName)
    {
        AudioClip _audioClip = (AudioClip)Resources.Load("Sounds/Music/The Journey Is The Treasure _Copyright Free Music_ _ chill beats _chiptune");
        switch (_myMusicName)
        {
            default:
            case AudioClipCatalog.MusicName.The_Journey_Is_The_Treasure:
                _audioClip = (AudioClip)Resources.Load("Sounds/Music/The Journey Is The Treasure _Copyright Free Music_ _ chill beats _chiptune");
                break;
            case AudioClipCatalog.MusicName.World_Of_Nowhere:
                _audioClip = (AudioClip)Resources.Load("Sounds/Music/World Of Nowhere");
                break;
            case AudioClipCatalog.MusicName.Call_For_Love:
                _audioClip = (AudioClip)Resources.Load("Sounds/Music/Call For Love");
                break;
            case AudioClipCatalog.MusicName.Music_Of_Something:
                _audioClip = (AudioClip)Resources.Load("Sounds/Music/Music Of Something");
                break;
            case AudioClipCatalog.MusicName.Day_Of_What:
                _audioClip = (AudioClip)Resources.Load("Sounds/Music/Day Of What");
                break;
            case AudioClipCatalog.MusicName.A_Day:
                _audioClip = (AudioClip)Resources.Load("Sounds/Music/A Day");
                break;
            case AudioClipCatalog.MusicName.Easy_Dreams:
                _audioClip = (AudioClip)Resources.Load("Sounds/Music/Easy Dreams");
                break;
            case AudioClipCatalog.MusicName.Gift:
                _audioClip = (AudioClip)Resources.Load("Sounds/Music/Gift");
                break;
            case AudioClipCatalog.MusicName.Belong_To_Her_Place:
                _audioClip = (AudioClip)Resources.Load("Sounds/Music/Belong To Her Place");
                break;
            case AudioClipCatalog.MusicName.Free_Tonight:
                _audioClip = (AudioClip)Resources.Load("Sounds/Music/Free Tonight");
                break;
            case AudioClipCatalog.MusicName.Comfortless_Seasons:
                _audioClip = (AudioClip)Resources.Load("Sounds/Music/Comfortless Seasons");
                break;
            case AudioClipCatalog.MusicName.Good_Moves:
                _audioClip = (AudioClip)Resources.Load("Sounds/Music/Good Moves");
                break;
            case AudioClipCatalog.MusicName.Get_Out:
                _audioClip = (AudioClip)Resources.Load("Sounds/Music/Get Out");
                break;
            case AudioClipCatalog.MusicName.Give_Her_Shadow:
                _audioClip = (AudioClip)Resources.Load("Sounds/Music/Give Her Shadow");
                break;
            case AudioClipCatalog.MusicName.I_Dont_Care_About_Place:
                _audioClip = (AudioClip)Resources.Load("Sounds/Music/I Don_t Care About Place");
                break;
            case AudioClipCatalog.MusicName.Lets_Do_This:
                _audioClip = (AudioClip)Resources.Load("Sounds/Music/Let_s Do This");
                break;
            case AudioClipCatalog.MusicName.Because_Of_Yesterday:
                _audioClip = (AudioClip)Resources.Load("Sounds/Music/Because Of Yesterday");
                break;
        }
        return _audioClip;
    }
}