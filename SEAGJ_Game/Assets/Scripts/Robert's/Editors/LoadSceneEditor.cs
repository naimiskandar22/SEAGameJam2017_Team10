using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
#if UNITY_EDITOR
[CustomEditor(typeof(LoadScene))]
public class LoadSceneEditor : Editor 
{
    private SerializedProperty haveDelay;
    private SerializedProperty delayBeforeLoad;
    private SerializedProperty changeSceneOnStart;
    private SerializedProperty sceneName;
    private SerializedProperty haveLoadingScreen;
    private SerializedProperty loadingAnimationDuration;
    private SerializedProperty isLoadingScreenActive;
    private SerializedProperty loadingScreen;
    private SerializedProperty loadingAnimator;
    private SerializedProperty animationName;

    private void OnEnable()
    {
        haveDelay = serializedObject.FindProperty("haveDelay");
        delayBeforeLoad = serializedObject.FindProperty("delayBeforeLoad");
        changeSceneOnStart = serializedObject.FindProperty("changeSceneOnStart");
        sceneName = serializedObject.FindProperty("sceneName");
        haveLoadingScreen = serializedObject.FindProperty("haveLoadingScreen");
        loadingAnimationDuration = serializedObject.FindProperty("loadingAnimationDuration");
        isLoadingScreenActive = serializedObject.FindProperty("isLoadingScreenActive");
        loadingScreen = serializedObject.FindProperty("loadingScreen");
        loadingAnimator = serializedObject.FindProperty("loadingAnimator");
        animationName = serializedObject.FindProperty("animationName");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.UpdateIfRequiredOrScript();

        EditorGUILayout.PropertyField(sceneName, new GUIContent("Scene Name to Load"));
        EditorGUILayout.PropertyField(haveDelay, new GUIContent("Delay before load?"));

        if(haveDelay.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(delayBeforeLoad, new GUIContent("Delay time"));
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
        }

        EditorGUILayout.PropertyField(changeSceneOnStart, new GUIContent("Timed on Object Active?"));
        EditorGUILayout.PropertyField(haveLoadingScreen, new GUIContent("Loading Screen?"));

        if (haveLoadingScreen.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(loadingAnimationDuration, new GUIContent("Loading Animation Duration"));
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(isLoadingScreenActive, new GUIContent("Loading Screen Active?"));
            
            if(isLoadingScreenActive.boolValue)
            {       
                EditorGUILayout.LabelField("Make sure there is animator for the loading animation.", EditorStyles.boldLabel);
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(loadingAnimator, new GUIContent("Loading Screen Animator"));
                EditorGUILayout.PropertyField(animationName, new GUIContent("Animation Name To Play"));
            }
            else
            {
                EditorGUILayout.LabelField("Make sure loading animation will play on active.", EditorStyles.boldLabel);
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(loadingScreen, new GUIContent("Loading Screen Object"));
            }

            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
