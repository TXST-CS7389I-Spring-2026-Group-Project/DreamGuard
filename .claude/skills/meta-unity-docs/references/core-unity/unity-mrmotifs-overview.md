# Unity Mrmotifs Overview

**Documentation Index:** Learn about unity mrmotifs overview in this documentation.

---

---
title: "MR Motifs Overview for Unity"
description: "Reusable design blueprints for common mixed reality mechanics and interactions on Meta Quest, with sample implementations."
last_updated: "2025-05-14"
---

<box display="flex" flex-direction="column" align-items="center">
  <a href="https://github.com/oculus-samples/Unity-MRMotifs">
    <img alt="MR Motifs Banner" src="/images/unity-mrmotifs-banner.png" border-radius="12px" width="100%" />
  </a>
</box>

<p>
  <b>Motifs</b> are blueprints for recurring ideas in the Horizon OS development community. They are not full applications, but recurring aspects of applications that rely on multiple technical features and APIs. MR Motifs are created to teach best practices, inspire developers, and spark new ideas.
</p>

<h2>MR Motifs Library</h2>

<p>We're continuously building new MR content to introduce emerging features and inspire your work. Here's what's available now:</p>

<ul>
  <li><a href="/documentation/unity/unity-mrmotifs-passthrough-transitioning"><b>Passthrough Transitioning</b></a> – Seamlessly fade between Passthrough and VR</li>
  <li><a href="/documentation/unity/unity-mrmotifs-shared-activities"><b>Shared Activities in Mixed Reality</b></a> – Make people feel truly physically present with each other</li>
  <li><a href="/documentation/unity/unity-mrmotifs-instant-content-placement"><b>Instant Content Placement</b></a> – Use the Depth API to create effects not possible otherwise</li>
  <li><a href="/documentation/unity/unity-mrmotifs-colocated-experiences"><b>Colocated Experiences</b></a> – Create seamless colocated experiences with ease</li>
</ul>

<h2>Starting Scene</h2>

<p>
  All scenes can be accessed from the <code>MRMotifsHome</code> scene located at <b><code>Assets/MRMotifs/MRMotifsHome.unity</code></b>. This home scene includes the <b><code>Menu Panel</code></b> prefab and script, which displays the scene list and interaction options. The menu can be toggled with the menu button or start gesture using hands or controllers.
</p>

<h2>Optional Multiplayer Features</h2>

<p>
  The <code>MRMotifsHome</code> scene includes a GameObject called <b><code>[MR Motif] Quest Platform Setup</code></b>. It uses an <a href="/documentation/unity/ps-entitlement-check">Entitlement Check</a> from the <a href="/documentation/unity/bb-multiplayer-blocks">Multiplayer Building Blocks</a>, and a component called <b><code>InvitationAcceptanceHandlerMotif</code></b>.
</p>

<p>
  <code>InvitationAcceptanceHandlerMotif</code> manages friend invites for multiplayer motifs. It checks user entitlement and handles destination routing for invited friends. Current destinations are:
</p>

<ul>
  <li><b>mrmotif_chess</b></li>
  <li><b>mrmotif_movie</b></li>
  <li><b>mrmotif_colocationDiscovery</b></li>
  <li><b>mrmotif_spaceSharing</b></li>
</ul>

<p>
  To use this pattern in your own app, update these destinations with your own scenes and APIs. You can configure destinations in the <a href="/manage">Meta Horizon Developer Dashboard</a> under <b>Engagement → Destinations</b>.
</p>

<h2>Resources</h2>

<ul>
  <li><a href="https://github.com/oculus-samples/Unity-MRMotifs">Unity-MRMotifs samples on GitHub</a></li>
  <li><a href="/design/mr-overview">How to design Mixed Reality experiences</a></li>
  <li><a href="/resources/">Guides to help you design, develop, and distribute your VR app</a></li>
  <li><a href="https://www.youtube.com/@MetaDevelopers/videos">Meta Developers YouTube channel</a></li>
  <li><a href="/blog/">Meta Developer's Blog</a></li>
</ul>