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
        dosya

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
    public void Interact()
    {
        switch (interactionType)
        {
            case InteractionType.Cekmece:
                MoveCekmece();
                break;
            case InteractionType.kitap:
                MoveBooks();
                break;
            case InteractionType.dosya:
                OpenFiles();
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
        // stop any ongoing movement
        if (animateCoroutine != null)
            StopCoroutine(animateCoroutine);
        // determine start and end positions
        Vector3 startPos = transform.position;
        Vector3 endPos = isOpen ? startPos + Vector3.right * 0.6f
                                : startPos + Vector3.left * 0.6f;
        // toggle state
        isOpen = !isOpen;
        // start smooth animation
        animateCoroutine = StartCoroutine(AnimateMove(startPos, endPos, animDuration));
    }

    // smooth position animation
    private System.Collections.IEnumerator AnimateMove(Vector3 from, Vector3 to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = to;
        Debug.Log($"{name} animation complete. Position: {to}");
    }

    bool isBooksMoved = false;
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
    
    private void OpenFiles()
    {
        // fetch Animator on this object
        Animator anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogWarning($"No Animator found on {name} for openFiles interaction");
            return;
        }
        if (anim != null && anim.runtimeAnimatorController != null)
        {
            var clips = anim.runtimeAnimatorController.animationClips;
            if (clips.Length > 0)
            {
                string clipName = clips[0].name;
                anim.Play(clipName);
                Debug.Log($"Playing default animation '{clipName}' on {name}");
                return;
            }
        }
        Debug.LogWarning($"No Animator or animation clips found for openFiles on {name}");
    }
}
