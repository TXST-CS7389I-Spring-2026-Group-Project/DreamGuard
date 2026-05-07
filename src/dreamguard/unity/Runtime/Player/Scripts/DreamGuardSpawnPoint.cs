using UnityEngine;

namespace DreamGuard
{
    /// <summary>
    /// Marks the player's starting position and rotation for this scene.
    ///
    /// Place one instance somewhere in each scene. DreamGuardPlayer will
    /// teleport to it automatically when the scene finishes loading.
    /// Only position and Y-rotation are applied; pitch and roll are ignored
    /// to avoid tilting the player.
    /// </summary>
    public class DreamGuardSpawnPoint : MonoBehaviour
    {
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0f, 1f, 0.5f, 0.8f);
            Gizmos.DrawWireSphere(transform.position, 0.25f);
            Gizmos.DrawRay(transform.position, transform.forward * 0.75f);
        }
#endif
    }
}
