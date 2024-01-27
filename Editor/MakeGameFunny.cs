using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Simen.FunnyPackage.Editor
{
    public class MakeGameFunny : MonoBehaviour
    {
        [MenuItem("Funny/Make game funny")]
        public static void FunnyGame()
        {
            GameObject go = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Packages/com.simen.funnypackage/Assets/Prefabs/WeAreGameDevelopers.prefab", typeof(GameObject)));
        }
    }
}
