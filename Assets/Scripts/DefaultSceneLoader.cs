#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[InitializeOnLoadAttribute]
public static class DefaultSceneLoader
{
    static DefaultSceneLoader(){
        EditorApplication.playModeStateChanged += LoadDefaultScene;
    }

    static void LoadDefaultScene(PlayModeStateChange state){
        if (state == PlayModeStateChange.ExitingEditMode) {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
        }

        if (state == PlayModeStateChange.EnteredPlayMode) {



        //-----------------EDIT THIS--------------------------------
        bool LoadDefaultSceneBool = false;
        //----------------------------------------------------------


            if (LoadDefaultSceneBool == true)
            {
            Debug.Log("-----------WARNING: Loading default Scene----------- click this message and edit 'LoadDefaultSceneBool' bool to change it, if you don't want this");
            EditorSceneManager.LoadScene (0);
            }
        }
    }
}
#endif