using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// -----------------------------------------------------------------------------------------------------------
// DESCRIPTION      : This class/component is used to load new scene.
// HOW-TO-USE       : Call StartLoadSceneSequence() to load new scene.
//                      - Call it through events (eg: button clicks).
//                      - Set changeSceneOnStart to true (in this case, the function will be called on Start()).
//                      - Refer this script and call it in your code.
// PROPERTIES       : There are some properties given for flexibility purposes.
//                      - Delay before loading screen.
//                      - Start load scene on object Start().
//                      - Activate a loading screen with loading animation played on default.
//                      - Playing a loading animation through an active object.
// ------------------------------------------------------------------------------------------------------------
public class LoadScene : MonoBehaviour
{
    #region Variables

    [SerializeField]
    [Tooltip("Do you want this to change scene on Start()?")]
    private bool changeSceneOnStart;

    [Space(6f)]
    [SerializeField]
    [Tooltip("Scene name to load.")]
    private string sceneName;

    [Space(6f)]
    [SerializeField]
    [Tooltip("Do you need delay before loading scene?")]
    private bool haveDelay;

    [SerializeField]
    [Tooltip("Delay duration before loading scene.")]
    private float delayBeforeLoad;

    [Space(6f)]
    [SerializeField]
    [Tooltip("Do you need some kind of loading screen?")]
    private bool haveLoadingScreen;

    [SerializeField]
    [Tooltip("How long the loading animation takes.")]
    private float loadingAnimationDuration;

    [SerializeField]
    [Tooltip("Is the loading screen active in hierarchy?")]
    private bool isLoadingScreenActive;

    [SerializeField]
    [Tooltip("Set your loading screen here. If there is any animation to be played, make sure it is played on active.")]
    GameObject loadingScreen;

    [SerializeField]
    [Tooltip("Set animator for your loading screen animation here.")]
    Animator loadingAnimator;

    [SerializeField]
    [Tooltip("Animation name to be played from the animator.")]
    string animationName;
    #endregion

    void Start()
    {
        if (changeSceneOnStart) // Change scene on start.
        {
            if (!haveDelay)
                StartLoadSceneSequence(); // No delay, start load scene sequence immediately.
            else
                Invoke("StartLoadSceneSequence", delayBeforeLoad); // Have delay, start load scene sequence after delay.
        }
    }

    private IEnumerator LoadSceneSequence()
    {
        if (haveLoadingScreen) // If there is loading screen..
        {
            if (isLoadingScreenActive)
                loadingAnimator.Play(animationName); // Play animation if loading screen object is already active.
            else
                loadingScreen.SetActive(true); // Active loading screen if not active.

            yield return new WaitForSeconds(loadingAnimationDuration); // Delay for the loading animation.

            SceneManager.LoadScene(sceneName); // Load scene.
        }
        else // If there is no loading screen..
        {
            SceneManager.LoadScene(sceneName); // Load scene.
        }
    }

    public void StartLoadSceneSequence()
    {
        StartCoroutine(LoadSceneSequence());
    }
}
