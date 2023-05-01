using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    //スライダーが必要なので
    public Slider slider;

    //System.で宣言する事で、インスペクターから値をセットできる。
    [System.Serializable]
    public class SoundData
    {
        public string name;         //インスペクターで名前をつけてね
        public AudioClip audioClip; //受け取る音
        public float playedTime;    //前回再生した時間
        public float Volume = 0.1f;   //ボリューム管理※予定
    }

    [SerializeField]
    private SoundData[] soundDatas;

    //AudioSource（スピーカー）を同時に鳴らしたい音の数だけ用意(20は多すぎたんでとりあえず10まで減らした)
    private AudioSource[] audioSourceList = new AudioSource[10];

    //別名(name)をキーとした管理用Dictionary
    private Dictionary<string, SoundData> soundDictionary = new Dictionary<string, SoundData>();

    //一度再生してから、次再生出来るまでの間隔(現在0.2秒)
    [SerializeField]
    private float playableDistance = 0.2f;

    //やっぱりスタティックを使うと、他で定義する時とても簡単。
    public static SoundManager instance;

    private void Awake()
    {

        //シングルトンパターン
        if (instance == null)
        { 
            //auidioSourceList配列の数だけAudioSourceを自分自身に生成して配列に格納
            for (var i = 0; i < audioSourceList.Length; ++i)
            {
                audioSourceList[i] = gameObject.AddComponent<AudioSource>();
                
                Debug.Log("拾ったよ");
            }

            //soundDictionaryにセット
            foreach (var soundData in soundDatas)
            {
                soundDictionary.Add(soundData.name, soundData);
            }
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //未使用のAudioSourceの取得 全て使用中の場合はnullを返す
    private AudioSource GetUnusedAudioSource()
    {
        for (var i = 0; i < audioSourceList.Length; ++i)
        {
            if (audioSourceList[i].isPlaying == false) return audioSourceList[i];
        }
        Debug.Log("未使用のAudioSourceは見つかりませんでした");
        return null; //未使用のAudioSourceは見つかりませんでした
    }

    //指定されたAudioClipを未使用のAudioSourceで再生
    public void Play(AudioClip clip)
    {
        var audioSource = GetUnusedAudioSource();
        if (audioSource == null) return; //再生できませんでした
        audioSource.clip = clip;
        audioSource.Play();
    }

    //指定された別名で登録されたAudioClipを再生
    public void Play(string name)
    {
        if (soundDictionary.TryGetValue(name, out var soundData)) //管理用Dictionary から、別名で探索
        {
            if (Time.realtimeSinceStartup - soundData.playedTime < playableDistance) return;    //まだ再生するには早い
            soundData.playedTime = Time.realtimeSinceStartup;//次回用に今回の再生時間の保持
            Play(soundData.audioClip); //見つかったら、再生
        }
        else
        {
            Debug.LogWarning($"その別名は登録されていません:{name}");
        }
    }

    //音量調整するスライダー用
    public void SoundSliderOnValueChange(float newSliderValue)
    {


        for (var i = 0; i < audioSourceList.Length; ++i)
        {
            audioSourceList[i].volume = newSliderValue;
            Debug.Log("変更したよ");
        }
    }

}