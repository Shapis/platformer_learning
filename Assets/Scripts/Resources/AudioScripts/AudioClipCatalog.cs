using UnityEngine;

public class AudioClipCatalog : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private AudioClip[] m_MusicClip;
    [SerializeField] private AudioClip[] m_SfxClip;


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
        AudioClip _audioClip = m_SfxClip[0];
        switch (_mySfxName)
        {
            default:
            case AudioClipCatalog.SfxName.LandingNormal:
                _audioClip = m_SfxClip[0];
                break;
            case AudioClipCatalog.SfxName.JumpingNormal:
                _audioClip = m_SfxClip[1];
                break;
            case AudioClipCatalog.SfxName.ConstantBuzz:
                _audioClip = m_SfxClip[2];
                break;
            case AudioClipCatalog.SfxName.HollowWoodKnock:
                _audioClip = m_SfxClip[3];
                break;
            case AudioClipCatalog.SfxName.ChestOpening:
                _audioClip = m_SfxClip[4];
                break;
            case AudioClipCatalog.SfxName.MetalFalling:
                _audioClip = m_SfxClip[5];
                break;
            case AudioClipCatalog.SfxName.PortcullisOpening:
                _audioClip = m_SfxClip[6];
                break;
            case AudioClipCatalog.SfxName.PortcullisClosing:
                _audioClip = m_SfxClip[7];
                break;
            case AudioClipCatalog.SfxName.CoinCollecting:
                _audioClip = m_SfxClip[8];
                break;
            case AudioClipCatalog.SfxName.ContinuousPressurePlateBuzz:
                _audioClip = m_SfxClip[9];
                break;
            case AudioClipCatalog.SfxName.WaterSplash:
                _audioClip = m_SfxClip[10];
                break;
            case AudioClipCatalog.SfxName.KeyObtained:
                _audioClip = m_SfxClip[11];
                break;
            case AudioClipCatalog.SfxName.KeyTurning:
                _audioClip = m_SfxClip[12];
                break;
            case AudioClipCatalog.SfxName.MenuClicked:
                _audioClip = m_SfxClip[13];
                break;
        }
        return _audioClip;
    }

    public AudioClip GetMusicClip(AudioClipCatalog.MusicName _myMusicName)
    {
        AudioClip _audioClip = m_MusicClip[0];
        switch (_myMusicName)
        {
            default:
            case AudioClipCatalog.MusicName.The_Journey_Is_The_Treasure:
                _audioClip = m_MusicClip[0];
                break;
            case AudioClipCatalog.MusicName.World_Of_Nowhere:
                _audioClip = m_MusicClip[1];
                break;
            case AudioClipCatalog.MusicName.Call_For_Love:
                _audioClip = m_MusicClip[2];
                break;
            case AudioClipCatalog.MusicName.Music_Of_Something:
                _audioClip = m_MusicClip[3];
                break;
            case AudioClipCatalog.MusicName.Day_Of_What:
                _audioClip = m_MusicClip[4];
                break;
            case AudioClipCatalog.MusicName.A_Day:
                _audioClip = m_MusicClip[5];
                break;
            case AudioClipCatalog.MusicName.Easy_Dreams:
                _audioClip = m_MusicClip[6];
                break;
            case AudioClipCatalog.MusicName.Gift:
                _audioClip = m_MusicClip[7];
                break;
            case AudioClipCatalog.MusicName.Belong_To_Her_Place:
                _audioClip = m_MusicClip[8];
                break;
            case AudioClipCatalog.MusicName.Free_Tonight:
                _audioClip = m_MusicClip[9];
                break;
            case AudioClipCatalog.MusicName.Comfortless_Seasons:
                _audioClip = m_MusicClip[10];
                break;
            case AudioClipCatalog.MusicName.Good_Moves:
                _audioClip = m_MusicClip[11];
                break;
            case AudioClipCatalog.MusicName.Get_Out:
                _audioClip = m_MusicClip[12];
                break;
            case AudioClipCatalog.MusicName.Give_Her_Shadow:
                _audioClip = m_MusicClip[13];
                break;
            case AudioClipCatalog.MusicName.I_Dont_Care_About_Place:
                _audioClip = m_MusicClip[14];
                break;
            case AudioClipCatalog.MusicName.Lets_Do_This:
                _audioClip = m_MusicClip[15];
                break;
            case AudioClipCatalog.MusicName.Because_Of_Yesterday:
                _audioClip = m_MusicClip[16];
                break;
        }
        return _audioClip;
    }
}