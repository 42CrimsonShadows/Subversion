using UnityEngine;
using System.Collections; // required for Coroutines


// An enumerator used to describe the type of fade that was done for a fade event.
public enum FadeType
{
    In, Out
}

// The event handler for when the screen fade changes.
// Called once the screen is fully faded in or out.
/// <param name="sender">The camera game object calling the fade event.</param>
/// <param name="fadeType">The type of fading the occured (In or Out)</param>
public delegate void FadeChangeHandler(object sender, FadeType fadeType);


/// Fades the screen from black after a new scene is loaded.
public class ScreenFade : MonoBehaviour
{

    // How long it takes to fade.
    public float fadeTime = 3.0f;

    // The initial screen color.
    public Color fadeColor = new Color(0.01f, 0.01f, 0.01f, 1.0f);

    // The material being used for fading.
	private Material fadeMaterial = null;

    // True when the screen is being faded currently, false otherwise.
	private bool isFading = false;
    // The fade instruction that tells the co-routine to wait until end of frame.
	private readonly YieldInstruction fadeInstruction = new WaitForEndOfFrame();

    // A boolean indicating whether the screen is fading currently.
	public bool Fading
    {
        get
        {
            return isFading;
        }
    }

    // The event called when fading has changed.
	public event FadeChangeHandler FadeChanged;

    // Initialize.
    void Awake()
    {
        // create the fade material
        fadeMaterial = new Material(Shader.Find("Oculus/Unlit Transparent Color"));
    }

    // Starts the fade in
    void OnEnable()
    {
        StartCoroutine(FadeIn());
    }

    // Starts a fade in when a new level is loaded
    void LevelWasLoaded(int level)
    {
        StartCoroutine(FadeIn());
    }

    // Cleans up the fade material
    void OnDestroy()
    {
        if (fadeMaterial != null)
        {
            Destroy(fadeMaterial);
        }
    }

    // Fades the screen in or out.
    /// <param name="fadeType">The <see cref="FadeType"/> to use when fading (e.g. fade in or out)</param>
	public void Fade(FadeType fadeType)
    {
        switch (fadeType)
        {
            case FadeType.In:
                StartCoroutine(FadeIn());
                break;
            case FadeType.Out:
                StartCoroutine(FadeOut());
                break;
        }
    }

    // Fades alpha from 1.0 to 0.0
    IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;
        fadeMaterial.color = fadeColor;
        Color color = fadeColor;
        isFading = true;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            color.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            fadeMaterial.color = color;
        }
        isFading = false;
        if (FadeChanged != null)
        {
            FadeChanged(gameObject, FadeType.In);
        }
    }

    // Fades alpha from 0.0 to 1.0
    IEnumerator FadeOut()
    {
        float elapsedTime = 0.0f;
        fadeMaterial.color = fadeColor;
        Color color = fadeColor;
        isFading = true;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeTime);
            fadeMaterial.color = color;
        }
        isFading = false;
        if (FadeChanged != null)
        {
            FadeChanged(gameObject, FadeType.Out);
        }
    }

    // Renders the fade overlay when attached to a camera object
    void OnPostRender()
    {
        if (isFading)
        {
            fadeMaterial.SetPass(0);
            GL.PushMatrix();
            GL.LoadOrtho();
            GL.Color(fadeMaterial.color);
            GL.Begin(GL.QUADS);
            GL.Vertex3(0f, 0f, -12f);
            GL.Vertex3(0f, 1f, -12f);
            GL.Vertex3(1f, 1f, -12f);
            GL.Vertex3(1f, 0f, -12f);
            GL.End();
            GL.PopMatrix();
        }
    }
}