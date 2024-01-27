using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Simen.FunnyPackage.Runtime
{
    public class WeAreGameDevelopers : MonoBehaviour
    {
        [SerializeField] private GameDevClipData[] _videoClips;

        [SerializeField] private Text _subtitleText;
        [SerializeField] private RectTransform _subtitlePanel;

        public VideoPlayer VideoPLayer { get; private set; }

        private List<int> _notPlayedVideoClips = new List<int>();
        private float _timeRemainingClip;
        private float _timeRemainingSubtitle;
        private int _currentPlaying;
        private int _currentSubtitle;

        public void Start()
        {
            this.VideoPLayer = GetComponent<VideoPlayer>();
            this.VideoPLayer.targetCamera = Camera.main;
            this.VideoPLayer.loopPointReached += (vp) => { vp.targetCameraAlpha = 0f; Time.timeScale = 1f; UpdateStatus(); };
            UpdateStatus();
        }

        private void Update()
        {
            _timeRemainingClip -= Time.deltaTime;

            if (_timeRemainingClip < 0 && this.VideoPLayer.targetCameraAlpha < 0.1f)
            {
                PlayRandom();
            }

            if (_timeRemainingSubtitle > 0)
            {
                _timeRemainingSubtitle -= Time.unscaledDeltaTime;
                if (_timeRemainingSubtitle <= 0)
                {
                    DoneSubtitle();
                }
            }
        }

        private void UpdateStatus()
        {
            _timeRemainingClip = Random.Range(20f, 50f);
            if (_notPlayedVideoClips.Count == 0)
            {
                PopulateNotPlayedList();
            }
            int rand = Random.Range(0, _notPlayedVideoClips.Count);
            _currentPlaying = _notPlayedVideoClips[rand];
            _notPlayedVideoClips.RemoveAt(rand);
        }

        private void PopulateNotPlayedList()
        {
            for(int i = 0; i < _videoClips.Length; i++)
                {
                _notPlayedVideoClips.Add(i);
            }
        }

        private void PlayRandom()
        {
            _currentSubtitle = 0;
            ShowSubtitle();

            if (Random.Range(0f,1f) > 0.8f)
            {
                _timeRemainingClip += 5f;
                _timeRemainingSubtitle += 5f;
            }
            else
            {
                this.VideoPLayer.clip = _videoClips[_currentPlaying].Clip;
                this.VideoPLayer.Play();
                this.VideoPLayer.targetCameraAlpha = 1f;
                Time.timeScale = 0f;
            }
        }

        private void ShowSubtitle()
        {
            _subtitlePanel.gameObject.SetActive(true);

            _subtitleText.text = _videoClips[_currentPlaying].SubtitleData[_currentSubtitle].Content;
            _timeRemainingSubtitle = _videoClips[_currentPlaying].SubtitleData[_currentSubtitle].Time;

            LayoutRebuilder.ForceRebuildLayoutImmediate(_subtitlePanel);
        }

        private void DoneSubtitle()
        {
            _currentSubtitle++;
            if (_currentSubtitle >= _videoClips[_currentPlaying].SubtitleData.Length || this.VideoPLayer.targetCameraAlpha < 0.1f)
            {
                _subtitlePanel.gameObject.SetActive(false);
            }
            else
            {
                ShowSubtitle();
            }
        }
    }
}
