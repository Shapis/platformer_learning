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
    }

    public enum MusicName
    {
        The_Journey_Is_The_Treasure,
        World_Of_Nowhere,
        Call_For_Love,
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
        }
        return _audioClip;
    }
}
