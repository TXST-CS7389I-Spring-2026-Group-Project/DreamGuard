# Unity Customize Passthrough Surface Projected Passthrough

**Documentation Index:** Learn about unity customize passthrough surface projected passthrough in this documentation.

---

---
title: "Surface Projected Passthrough"
description: "Project passthrough onto specific surface meshes to selectively reveal the real world within defined geometries."
---

Surface-projected passthrough allows apps to specify the geometry onto which the passthrough images are projected (instead of an automatic environment depth reconstruction). For surface-projected passthrough layers, passthrough is only visible within the specified surface geometries, the rest of the layer is transparent.

Surface-projected passthrough can be used when the exact locations of real-world features are known (for example, a desk marked by the user using controllers) to avoid visual artifacts that may arise from the dynamic environment reconstruction used by regular passthrough layers. The surface geometries provided by the app should match real-world surfaces as closely as possible. If they differ significantly, users will receive conflicting depth cues, and objects may appear too small or large. On Quest Pro, such mismatches also lead to a shift between the color and luminance images, making colored objects appear in the wrong location.

There is no depth testing between the passthrough projection surface and the objects rendered in VR. This leads to surface-projected passthrough rendering either as an underlay (always occluded by virtual objects) or overlay (always occludes virtual objects).

You can add the following example script to GameObjects to render them as passthrough.

## Example Script

```cpp
public class PassthroughProjectionSurface : MonoBehaviour
{
  void Start()
  {
    GameObject ovrCameraRig = GameObject.Find("OVRCameraRig");
    OVRPassthroughLayer layer = ovrCameraRig.GetComponent<OVRPassthroughLayer>();
    layer.AddSurfaceGeometry(gameObject, false);

    // Disable the mesh renderer to avoid rendering the surface within Unity
    MeshRenderer mr = GetComponent<MeshRenderer>();
    if (mr)
    {
      mr.enabled = false;
    }
  }
}
```

Keeping a mesh renderer can be handy in the editor, but it should be removed or disabled at runtime to avoid the geometry being rendered as virtual objects.