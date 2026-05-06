# Po Art Direction

**Documentation Index:** Learn about po art direction in this documentation.

---

---
title: "Art Direction for All-in-One VR Performance"
description: "Art direction techniques and visual strategies that maintain quality while meeting Meta Quest performance targets."
---

All-in-one hardware brings a transformative advantage in immersion and presence to VR, but it also demands a return to a development model that treats performance as a fundamental design decision. While a momentary dip in frame rate for a desktop or console game may go unnoticed, in VR that same frame rate dip can at best break immersion and at worst cause player discomfort.

When dealing with the strict compute, thermal and power constraints of all-in-one VR, performance is everybody's problem. While it is reasonable to rely on engineering and technical art teams to continually optimize a product throughout development, developers are most successful when they begin thinking about performance at an earlier stage, holistically across all disciplines. While performance is often a deeply technical topic, the goal in this topic is to skip the code and math and focus on high-level creative decisions that impact down-stream performance issues. The more informed your early art and design decisions are, the better positioned your engineers and technical artists will be to meet the performance demands of your title.

## Style vs. Realism

The concept of the uncanny valley is most frequently discussed and studied around its impact on animation, but it's also a useful framing for talking about the impact of visual style decisions on performance. While photorealistic games are certainly possible in all-in-one VR, developers who attempt them are in greater danger of tumbling into the uncanny valley as they try to balance the trade offs necessary to hit performance targets. For a concrete example, consider a photorealistic character, who will be expected to move, deform, and cast shadows like a real person. Degrade too many of those aspects for performance purposes and you quickly land in the uncanny valley. On the other side of the spectrum, a game that intentionally targets a stylized look is already out of the valley. If, for performance or production reasons, you need to take some action that reduces perceived realism (reduce or eliminate dynamic shadows, simplify shaders, lower bone-count, and so on), it will likely have minimal impact on player perception and enjoyment.

There are many examples of developers putting this idea to use either intentionally or as a byproduct of their style choices. While many stylized titles are cartoony in nature, it is by no means the only non-photorealistic style available. Whatever style you choose, be wary of attempting an approach that may fall short of your vision due to technical constraints. Consider embracing a strong, non-photorealistic style to anchor your app to the safe side of the uncanny valley.

This isn't to say all realistic styles are bad and should be universally avoided. There are certainly successful, visually stunning games in all-in-one VR that use a more realistic style. The key is to be aware of the performance and technical challenges a more realistic style presents, and to be scoped to deal with those challenges throughout development if that style is of crucial importance. Likewise, if photorealism isn't ultimately critical to your product, moving in a more stylized direction can relieve technical pressure and allow you to allocate development resources to other features that may better serve your customers' enjoyment.

## Choose Your Focus

Whatever style you ultimately choose, in a world of finite computing power, choosing your focus is critical. This applies to world design, character design, and even how you build your final assets. Early in development it is important to determine what your product is about, and what aspects of it are critically important to its success. Designers do this regularly, but applying the same process to art can be hugely impactful.

For example, if your game is primarily about characters and emotive storytelling, then you'll want to put more detail into facial setups and deformation, perhaps even at the expense of the environment, and spend R&D time on a hierarchical character LOD system. Conversely, if your game is about fast action and special effects, facial performance may comfortably take a back seat to particle systems, extra entities on screen, and dynamic lighting. Put your time and detail where you want the viewer to focus. Over-detailing unimportant background elements is more likely to detract from the experience than add to it. If a particular asset or effect is going to be difficult to optimize or create other technical challenges, always ask yourself whether the aspects that make it challenging are truly important to your product. If the answer is no, don't be afraid to make aggressive cuts.

## Things to Look Out For

Making decisions about how and where to focus your resources can be difficult enough, but it's often amplified when you aren't necessarily sure what things cost in the first place.

The rest of this topic is going to provide a list of things specific to all-in-one VR that you should be wary of while designing your app. Like all design choices, this list isn't an absolute guide, and doesn't mean you should never attempt the things on it under any circumstance. Rather, the items on this list generally come with large trade-offs, and that means you should consider whether they're truly important to your product, and whether you have the technical and financial resources available to spend on achieving them.

### High Triangle Counts

In all-in-one VR, triangle count can be much more of a limiting factor than in desktop or console development, due to the [tiled rendering pipeline](/documentation/unity/gpu-tiled) used by all-in-one VR headsets. For an estimated triangle count budget, reference the [Triangle Counts on Meta Quest](/documentation/unity/unity-perf#draw-calls-on-meta-quest) documentation for your lowest-spec headset. Keep in mind these numbers are per-frame totals, not per character. Everything from the level geometry to characters, particle effects, and even UI elements will need to fit within your triangle budget. To get an idea of per-asset budgets, start a conversation early with your technical team.

With that in mind, it's worth considering at the concept and design phase how your assets might eventually topologize. Think about how a design may look when the geometric detail is baked into normal maps. Is the silhouette unique enough to read when some of the edges are optimized away? Is a six-armed monster covered in tentacles worth the technical and performance cost it will almost certainly incur? A lot of game artists are great at creating low poly topologies for complex characters, but even they can only do so much if the initial design isn't conducive to optimization.

### Complex Facial Animation

Facial animation can bring a lot to an app, but depending on how it's implemented, it can also be quite expensive. Simple, bone-based (or blend-shape-based, if the increased memory cost can be budgeted) systems are generally pretty safe to implement. As the complexity increases through more detailed skeletons, blend-shapes or blended normals maps, the cost can go up quickly. If high-detail, subtle facial performance is critical to your title, be prepared for necessary trade-offs in other areas, and some added technical complexity. A properly designed deformation skeleton leveraging hierarchical bone LODs can help in these situations, or in cases where you need several characters on screen, but only a few with full detail facial setups at any given time.

### Open Worlds

Open worlds can feel amazing, but they can also be difficult to implement on all-in-one VR hardware. For a truly open world, you'll need to spend a lot of time both on the art and engineering side optimizing for performance, as well as implement some form of streaming system to handle the continual loading of a large, open world. Creating a system like this comes with its own technical restrictions that can impact almost every aspect of your game. For example, player movement speed will be limited by the speed at which the world can be streamed in and out of memory. Due to the challenges involved, it would be prudent to have a working prototype running on target hardware that clearly defines and embraces these limitations prior to making a large investment in such a project.

### Cloth and Hair/Fur

Simple decisions about the design of a character's clothing can have a big impact on performance. Real-time cloth physics are expensive and in general look bad on lower resolution geometry. While certain things (such as straps, small capes, sashes) can be approximated by simple joint-chain springs, anything requiring real cloth dynamics should be carefully considered. Unless extremely important, long, flowing capes or full skirts should be avoided.

The same holds true for hair and fur. Short hair and hairstyles like pony-tails and mid-length bobs that can be approximated with simple (non-colliding) vertex motion may be feasible. Long, flowing hair creates a host of challenges, both in rendering and dynamics. Hair is another area where realism versus stylization choices can have a big impact. Realistic hair is often based on custom hair shaders (generally far too complex for all-in-one VR hardware) or hair created out of layers of alpha cards (which can also be prohibitively expensive on all-in-one VR hardware). By going with a slightly stylized approach you can instead utilize sculpted, textured hair shells, which perform much better. There's nothing inherently wrong with occasional, targeted use of alpha cards where impactful. Just be aware that the cost does eventually add up.

### Dynamic Shadows

Dynamic shadows are a feature that's often taken for granted, but they can have a huge impact on performance. Dynamic shadows by their nature need to be calculated every frame, putting a large burden on the CPU and GPU, and often require re-rendering of the scene many times within a single frame.

You can gain a large performance boost by relying on static environment lighting that can be baked into lightmaps, light probes, or in some cases even a static object's diffuse texture. While a slowly swinging, flickering light bulb may create a wonderful atmospheric effect, it doesn't come free.

### Full Screen Effects

Full screen effects, such as motion blur, per-pixel outlining, real-time ambient occlusion, tone mapping, and bloom, require a lot of pixel throughput that can prove challenging for all-in-one VR hardware. These sorts of effects are all going to add significant fill-rate overhead and should almost always be avoided. Some of these effects can be achieved through other means, but the key takeaway is that full screen effects present a particular challenge to all-in-one VR hardware and should be approached with extreme caution.

### Dense Foliage

Foliage has a lot of the same inherent challenges as hair and fur. Tree building techniques that leverage a lot of alpha cards can create significant overdraw problems. Large leaves and branches that need to react to character and player movements also create a lot of extra dynamic vertices. Keeping trees and plants static and geometrically chunkier can help quite a bit. The easiest optimization, however, is to simply avoid dense foliage in your environment designs unless absolutely necessary, or at a minimum keep them walled off behind natural movement blockers to avoid the expectation of interactivity.

## Conclusion

While all-in-one VR hardware may have computational limitations when compared to consoles and desktop class systems, titles designed with the hardware's strengths and weaknesses in mind can still create some of the most breathtakingly immersive game worlds the industry has ever seen. Even with good planning, nearly all titles end up in a late-stage optimization process to reach their final performance goals, but well-planned titles leave a lot less on the cutting room floor. Hopefully this brief overview of some of those challenges from an art and design perspective will help make that process a little less daunting when the time comes.