# Unity Isdk Selector

**Documentation Index:** Learn about unity isdk selector in this documentation.

---

---
title: "Interaction SDK Selector Interface"
description: "Define custom selection mechanisms like button presses or pinch gestures for Interactors using the ISelector interface."
---

An **ISelector** defines a selection mechanism for an interaction (eg. button press, index pinch).

For instances where **Interactors** don’t themselves define a selection mechanism (such as Ray or Grab), an **ISelector** can be provided to those **Interactors**.