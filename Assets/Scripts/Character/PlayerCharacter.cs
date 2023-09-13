using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ingame.Character
{
#if UNITY_EDITOR
    [CustomEditor(typeof(CharacterBase))]
#endif
    public class PlayerCharacter : CharacterBase
    {
        void Start()
        {
            base.Start();
        }

        void Update()
        {

        }
    }
}