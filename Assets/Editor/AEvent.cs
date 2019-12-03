using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AEvent : EditorWindow
{
    //Donner la possibilité au GA d'ajouter plus facilement des particles, des sons ou autres dans leur animation;

    // Faire une fenètre qui joue l'animation du personnage

    //Create or Get the window


    GameObject go;
    AnimationClip clip = default;
    int clipIndex;
    AnimationClip[] goClips = new AnimationClip[0];
    string[] goClipsName;
    Animator animator;

    bool lockedGO = false;

    [MenuItem("HugoTuto/AEvent")]
    public static void Init()
    {
        AEvent window = GetWindow<AEvent>("Animation Events");
    }

    //AnimationMode == Preview

    private void OnGUI()
    {
        //Get Target GO
        if (go == null) 
        {
            EditorGUILayout.HelpBox("Select a GameObject", MessageType.Info);
            return;
        }

        //Locked Button
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        lockedGO = GUILayout.Toggle(lockedGO, EditorGUIUtility.IconContent("AssemblyLock"), EditorStyles.toolbarButton);
        GUILayout.EndHorizontal();

        //Check for errors
        if (goClips.Length < 0)
        {
            EditorGUILayout.HelpBox("Selected GameObject doesn't have AnimationClip", MessageType.Info);
            return;
        }

        else if(animator == null)
        {
            EditorGUILayout.HelpBox("No Animator detected on selected GameObject", MessageType.Info);
            return;
        }

        //Make a dropdown of all Animation clips attached to the GO animator
        clipIndex = EditorGUILayout.Popup(clipIndex, goClipsName);

        //Update active AnimationClip based of the Dropdown value
        SetActiveAnimationClip(goClips[clipIndex]);

        if (clip == null) return;

        //Debug
        EditorGUILayout.LabelField("Active Clip : " + clip.name);
    }

    void SetActiveAnimationClip(AnimationClip clip)
    {
        this.clip = clip;
    }

    private void OnSelectionChange()
    {
        //Selection Lock
        if (lockedGO) return;

        //Get Data from selected GO
        go = Selection.activeGameObject;
        if (go != null) GetGOData();

        //Repaint in case of changement
        Repaint();
    }

    void GetGOData()
    {
        //Get Animator
        go.TryGetComponent(out Animator anim);
        animator = anim;

        //Get Animator Infos
        if (animator != null)
        {
            goClips = animator.runtimeAnimatorController.animationClips;
            goClipsName = new string[goClips.Length];
            for (int i = 0; i < goClips.Length; i++)
            {
                goClipsName[i] = goClips[i].name;
            }
        }
    }

    private void Update()
    {
        if (go == null) return;

    }

}
