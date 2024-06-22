using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AudioManager
{
    public enum AudioClipBGMAddress
    {
        Main = 0, Game, MaxCount
    }
    public enum AudioClipSFXAddress
    {
        Button = 0, Target, Item,MaxCount
    }

    private Dictionary<string, AudioClip> bgmClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();
    
    private AudioSource sourceBGM;
    private AudioSource sourceSFX;
    public AudioSource SourceBGM => sourceBGM;
    public AudioSource SourceSFX => sourceSFX;

    public void Init()
    {
        GameObject obj = GameObject.Find("@Audio");
        if(obj == null)
        {
            obj = new GameObject("@Audio");
        }
        UnityEngine.Object.DontDestroyOnLoad(obj);

        GameObject bgm = new GameObject("@BGM");
        bgm.transform.SetParent(obj.transform);

        GameObject sfx = new GameObject("@SFX");
        sfx.transform.SetParent(obj.transform);

        sourceBGM = bgm.AddComponent<AudioSource>();
        sourceSFX = sfx.AddComponent<AudioSource>();

        sourceSFX.loop = false;
    }
    public void SetAudioBGMClip(AudioSource _source, AudioClipBGMAddress _bgmAddress, bool _isPlay = false)
    {
        AudioClip clip = null;
        if (bgmClips.TryGetValue(_bgmAddress.ToString(), out clip))
        {
            _source.clip = clip;
            if(_isPlay) _source.Play();
        }
        else
        {
            Addressables.LoadAssetAsync<AudioClip>(_bgmAddress.ToString()).Completed += (obj) =>
            {
                if (obj.Status == AsyncOperationStatus.Succeeded)
                {
                    bgmClips.Add(_bgmAddress.ToString(), obj.Result);

                    _source.clip = obj.Result;

                    if (_isPlay)
                        _source.Play();
                }
            };
        }
    }
    public void SetAudioSFXClip(AudioSource source, AudioClipSFXAddress _sfxAddress)
    {
        AudioClip clip = null;
        if (sfxClips.TryGetValue(_sfxAddress.ToString(), out clip))
        {
            source.PlayOneShot(clip);
            Debug.Log("파일 저장되어 있음");
        }
        else
        {
            Addressables.LoadAssetAsync<AudioClip>(_sfxAddress.ToString()).Completed += (obj) =>
            {
                if (obj.Status == AsyncOperationStatus.Succeeded)
                {
                    sfxClips.Add(_sfxAddress.ToString(), obj.Result);
                    source.PlayOneShot(obj.Result);
                    Debug.Log($"파일 저장됨 :{_sfxAddress.ToString()} ");
                }
            };
        }
    }
    public bool IsCorrectBGM(AudioClipBGMAddress _bgmAddress)
    {
        AudioClip boolCheckClip = null;

        if (sourceBGM.clip == null)
            return false;

        if (!bgmClips.TryGetValue(_bgmAddress.ToString(), out boolCheckClip))
            return false;

        return sourceBGM.clip == boolCheckClip;
    }

    public void TurnOnOffBGM(bool _turnOn) => sourceBGM.mute = _turnOn == true ? false : true;
    public void TurnOnOffSFX(bool _turnOn) => sourceSFX.mute = _turnOn == true ? false : true;
}