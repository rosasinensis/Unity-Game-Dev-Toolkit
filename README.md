# Unity-Game-Dev-Toolkit
A collection of C# scripts and logic systems for Unity. Uses the namespace <b>MKUtil</b>.

A private collection of scripts I've written for my games that I think are helpful for a variety of projects. Putting it up here so I can use it across my other projects, too!

<h2>Scripts</h2>
<ul>
<li><b>Pick.cs</b>
<ul>Seeded random to grab items or shuffle ILists. Supports weighted loot tables, and uses Fisher-Yates shuffling.</li></ul>
<li><b>DebugLogger.cs</b>
<ul>Puts all UnityEngine.Debug.Logs into this <b>DebugLogger</b>, so it can be easily turned on or off for showing or logging. Also keeps all messages so it can be retrieved whenever.</li></ul>
<li><b>Easy.cs</b>
<ul>Random easy math stuff, like <b>Chance</b> (90% chance), a <b>FiftyFifty</b>, an extension to IList to <b>Pick</b> a random object, an extension for the Dictionary to simplify the TryGetValue check (<b>Get</b>), and a GameObject component <b>Grab</b> to TryGetComponent. (I think later versions of Unity have this already?)</li></ul>
<li><b>Calc.cs</b>
<ul>Some QoL 3D math extensions for Vector3s and Transforms, like <b>Dot</b>, <b>DistanceTo</b>, <b>DirectionTo</b>, <b>IsWithinRange</b>, and Quaternion <b>GetLookAtRotation</b>. Also includes <b>IsFacing</b> which helps with determining thresholds to dot products.</li></ul>
<li><b>TextUtil.cs</b>
<ul>More (hopefully) performant string handling using a new class called <b>TextBuilder</b> which uses StringBuilder. Also includes a static <b>To</b> class for other string manipulating functions like Capitalization, Lower(case), Removing Spaces. Used for things like "Red Apple" and turning it into ids like "redapple".</li></ul>
<li><b>Ticker.cs</b>
<ul>Subscribe or unsubscribe events that require invocation into one manager class, which batches them by rate. Example: batches all callbacks which asks to fire at 1.0f.</li></ul>
<li><b>MatchUtil.cs</b>
<ul>Recursively checks ILists to see if another IList's item meets its criteria using <b>MatchUtil.CanSatisfyAll</b>. Used for things like making sure recipe ingredients meet some rule (correct category, specific item, with specific tags, specific quality) which has to be met by player-selected ingredients.</li></ul></ul>
<h2>Folders</h2>
  <h3>SerializableData Folder</h3>
  <ul>Data that can be used in JSON files. Includes <b>SerializableQuaternion</b>, <b>SerializableVector3</b>, and <b>RBG</b>.</ul>
  <h3>StateMachine Folder</h3>
  <ul>Extremely simple and expandable <b>StateMachine</b> system that takes <b>IStates</b> and a <b>StateController</b> which notably <b>keeps a library of all its States by some string id</b>.</ul>
