# Vk Unity Faq

**Documentation Index:** Learn about VK unity faq in this documentation.

---

---
title: "Virtual Keyboard FAQ"
description: "A list of FAQ about Virtual Keyboard."
---

<oc-devui-note type="warning" heading="Virtual Keyboard Deprecation" markdown="block">
<p>Virtual Keyboard as a feature has been deprecated in favor of the system keyboard overlay in Unity.</p>

Please see [System Keyboard Overlay](/documentation/unity/unity-keyboard-overlay) for instructions on enabling the system keyboard as an overlay in Unity.
</oc-devui-note>

- **Q:** How do I use both the system keyboard and virtual keyboard?<br>
**A:** Disable the system keyboard for input fields you want to use with the virtual keyboard. For Unity input fields, this can be done using <code>< inputField >.keyboardType = (TouchScreenKeyboardType)(-1);</code> in a script.

- **Q:** Why is the keyboard not visible?<br>
**A:** Make sure **Virtual Keyboard** and **Render Model** are enabled in **OVRManager**. If they are and you still do not see a keyboard, navigate to **Project Settings** > **Meta XR** and fix any reported issues. Your headset must also be v54 and above.