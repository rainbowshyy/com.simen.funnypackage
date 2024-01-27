using System;
using UnityEngine;
using UnityEngine.Video;

namespace Simen.FunnyPackage.Runtime
{
    [CreateAssetMenu(menuName = "Funny clip")]
    public class GameDevClipData : ScriptableObject
    {
        public VideoClip Clip;

        [Serializable]
        public struct SubtitleDataStruct
        {
            [TextArea] public string Content;
            public float Time;
        }

        public SubtitleDataStruct[] SubtitleData;
    }
}
