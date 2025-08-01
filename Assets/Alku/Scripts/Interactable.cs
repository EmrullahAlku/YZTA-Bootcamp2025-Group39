using Unity.Collections;
using UnityEngine;
/// <summary>
/// Interface for objects that can be interacted with by the player.
/// </summary>
public class Interactable : MonoBehaviour
{
    /// <summary>
    /// Types of interactions supported by this object.
    /// </summary>
    public enum InteractionType
    {
        Cekmece,
        kitap,
        dosya,
        mektup,
        trash

    }
    [Tooltip("Select which interaction this object should perform")]
    public InteractionType interactionType;
    // No need to assign Animator manually; we'll fetch the component at runtime
    [Tooltip("Duration of the open/close animation")]
    // internal coroutine reference
    private Coroutine animateCoroutine;
    /// <summary>
    /// Called when the player interacts with this GameObject.
    /// </summary>
    private bool isRunning = false;
    public void Interact()
    {
        if (isRunning)
        {
            Debug.LogWarning($"{name} is in animation. Interaction ignored.");
            return;
        }
        switch (interactionType)
        {
            case InteractionType.Cekmece:
                // block further interactions until animation ends
                MoveCekmece();
                break;
            case InteractionType.kitap:
                MoveBooks();
                break;
            case InteractionType.dosya:
                OpenFiles();
                break;
            case InteractionType.mektup:
                OpenLetters();
                break;
            case InteractionType.trash:
                MoveTrash();
                break;
            default:
                Debug.LogWarning($"No interaction defined for {gameObject.name}");
                break;
        }
    }

    bool isOpen = false;
    float animDuration = 0.5f;
    private void MoveCekmece()
    {
        isRunning = true;
        // stop any ongoing movement
        if (animateCoroutine != null)
            StopCoroutine(animateCoroutine);
        // determine start and end positions (only X axis)
        Vector3 startPos = transform.position;
        float deltaX = isOpen ? 0.6f : -0.6f;
        Vector3 endPos = new Vector3(startPos.x + deltaX, startPos.y, startPos.z);
        // toggle state
        isOpen = !isOpen;
        // start smooth animation
        animateCoroutine = StartCoroutine(AnimateMove(startPos, endPos, animDuration));

    }

    // smooth position animation
    private System.Collections.IEnumerator AnimateMove(Vector3 from, Vector3 to, float duration)
    {
        // interpolate only X axis, keep Y and Z constant
        float elapsed = 0f;
        float startX = from.x;
        float endX = to.x;
        float y = from.y;
        float z = from.z;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float x = Mathf.Lerp(startX, endX, t);
            transform.position = new Vector3(x, y, z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        // ensure final position
        transform.position = new Vector3(endX, y, z);
        Debug.Log($"{name} animation complete. Position: {to}");
        // allow interactions again
        isRunning = false;
    }

    bool isBooksMoved = false;
    // state for trash movement
    private bool isTrashOpen = false;
    private void MoveBooks()
    {
        if (!isBooksMoved)
        {
            gameObject.SetActive(false);
            isBooksMoved = true;
        }
        else
        {
            gameObject.SetActive(true);
            isBooksMoved = false;
        }
    }

    // …existing code…
    private Animation anim;
    private Coroutine fileAnimCoroutine;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    private void OpenFiles()
    {
        // stop any ongoing file animation
        if (fileAnimCoroutine != null)
            StopCoroutine(fileAnimCoroutine);

        if (anim == null || anim.clip == null)
        {
            Debug.LogWarning($"No Animation clip found on {name}");
            return;
        }

        // play first half if closed, second half if already open
        fileAnimCoroutine = StartCoroutine(PlayFileHalf(isOpen));
    }

    private System.Collections.IEnumerator PlayFileHalf(bool closing)
    {
        isRunning = true;
        string clipName = anim.clip.name;
        float half = anim.clip.length / 2f;

        if (!closing)
        {
            // play from start to half
            anim[clipName].time = 0f;
            anim[clipName].speed = 1f;
            anim.Play(clipName);
            while (anim[clipName].time < half)
                yield return null;
            anim[clipName].speed = 0f;
            anim.Stop();
            isOpen = true;
        }
        else
        {
            // play from half to end
            AnimationState state = anim[clipName];
            state.time = half;
            state.speed = 1f;
            anim.Play(clipName);
            while (anim[clipName].time < anim.clip.length)
                yield return null;
            anim.Stop();
            isOpen = false;
        }

        isRunning = false;
    }

    private void OpenLetters()
    {
        // similar to OpenFiles, but with different animation logic
        if (anim == null || anim.clip == null)
        {
            Debug.LogWarning($"No Animation clip found on {name}");
            return;
        }

        if (isOpen)
        {
            anim.Play("CloseLetter");
        }
        else
        {
            anim.Play("OpenLetter");
        }
        isOpen = !isOpen;
    }

    private void MoveTrash()
    {
        Debug.Log("Moving trash can...");
        isRunning = true;
        if (animateCoroutine != null)
            StopCoroutine(animateCoroutine);
        // calculate start and end positions along X axis
        Vector3 startPos = transform.position;
        Vector3 endPos = isTrashOpen ? startPos + Vector3.left * 1.5f
                                     : startPos + Vector3.right * 1.5f;
        // toggle state
        isTrashOpen = !isTrashOpen;
        // start smooth X movement
        animateCoroutine = StartCoroutine(AnimateMove(startPos, endPos, animDuration));
    }
}
