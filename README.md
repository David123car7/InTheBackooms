# InTheBackrooms Game Files

With the **InTheBackrooms Game Files** you will have acess to everything that was
used/made for the InTheBackrooms Game, exept for the 3D models that can be seen
in the gameplay video because i am not the owner of those assets.

This game was made for a project of my school and i do not intend to keep
updating it, beeing that the reason why i decided to share the game files, this
way people will be able to learn and use what i made to their own projects.

To end this introduction, be aware that even though these game files are from
a game based in the Backrooms, that does not mean that the scripts/systems
present in the game files can only be used to a game based on the backrooms so
i encourage you to read the list of contenct present in the game files.

[<img src="https://i.gjcdn.net/public-data/games/12/175/802175/screenshots/inthebackrooms-screenshot-2023-04-19---18-14-54-33-wvbkxet9.png" width="100%">](https://www.youtube.com/watch?v=kABX1e6MA1g)

# Content present in the game files:

* First Person Controller (Stamina System, Leaning System, HeadBob System, Crouching & Jumping, Sound Effects)

* Sanaty System

* Inventory System

* Flashlight System

* Death System

* Finish Level System

* Interaction System

* Entitys (Bacteria & Smiler (Spawner System, AI System, Jumpscares))

* UI (All Menus & overlays)

* Sounds (Entitys sounds, ambience sounds, random sounds (Inventory sound, use item sound, ...))

* Misc (Interactable posters/notes,  time counter (Overlay),  flickering lights, vending machine, audio recorders, decals, all materials, ambience (Sound triggers, random sounds player))

* Scenes (Map of the Level 0,  menu of the game, showcase scene (Scene to show main systems))

# SETUP

You will have three scenes that will be in the folder called Scenes, go to the
Showcase scene, where the main systems are displayed for you to test them out.

If you want to set up a new scene with all the systems, use the prefab called
GameSetup, that prefab already has all the systems connected to one another to
make everything work, except for the FirstPersonController, the
FirstPersonController prefab is in the FirstPersonController folder. **BE AWARE**
that the FPC is not in the GameSetup prefab because the FPC **cannot be a children
of any object.**

When creating a new scene with the GameSetup and then adding the FPC to the
scene, there will be variables on the inspector to be assigned in relation to
the FPC, not only in the FPC but also in other GameObjects, so use the Showcase
scene to see how things must be assigned.

# How things work? 

**Sanaty System:** The sanity system contains one script that is assigned to the
PlayerBody, that script controlls how much sanity the player loses, how much
sanity he has, and what effects will occur with the loss of sanity. The areas
of losing sanity are created by using an EmptyGameObject with a box collider
(isTrigger ON) with the tag "sanity."

**Inventory System:** With the inventory system, you will be able to store, use
and drop items. you will have access from the start to three different items
(Almond Water, batteries, and pills), but you will want to create your own
items, so I will explain how you do that.

There are two types of items, the collectible items and the consumable items,
the collectible items are items that cannot be used, you can only store them,
see their description and their image, and the consumable items are items that
can be used to “change values”, for example the Almond Water item restores
the sanity of the player, so the item will change the values of the sanity of
the player.

**To create those items**, you just need to right-click in the project window and
click **Collectible Item SO** or **Consumable Ttem SO.**
Having your new item created, you will be able to change the image, give it a
name, give it a description, and even add a sound to play when you use the
item, but most importantly, you will need to add a **Modifier**.

A Modifier it's what you will add to your **consumable item** that will change a
certain value, this way it's not the item that determines what values he will
change but the modifier itself (The modifiers are in
Inventory>Scripts>Model>Data), seeing those modifiers, you can create your own
to add to your own item. And be aware that you can add more than one modifier.

**Interaction System:** The interaction system contains one script that is assigned
to the PlayerBody, in that scripts is the code to how and what the player can
interact with. As it is now, the player can interact with items, posters, audio
recorders and vending machines, each interaction has a different layer, so for
example if you create a new item, to that item you will need to assign the
layer mask of the items, and using that logic, you can create even more
different interactions. 

**Entitys:**

**Bacteria:** The bacteria have two main systems: how it spawns and the AI.
To spawn after the game starts, the bacteria will appear after a chosen
time, and the way it works is: it is calculated a random position around
the player, and then it will only spawn on a surface that has, as is
Game Object the layer assigned Ground, so make sure that your map has
the ground with that layer assigned.

**BACTERIA AI:** The bacteria has two modes: searching and hunting. After spawning,
the bacteria will start by walking around trying to find his prey (the
player), and after finding it, it will start hunting, trying to kill the
player, if that happens the only way the player can escape is running
increasing the distance between the bacteria until the player reaches a
certain amount of distance. 

**There are two ways the bacteria will find you:** you will appear on the
bacteria's field of vision, which you can change (increasing fov and
distance), or it will hear you.

**NOTE:** Every Wall and Object that should obstruct the vision of the
bacteria must be assigned an obstructMask. 

**Smiler:** The entity smiler will only appear after all the player sanity
is gone, after that happens, everything becomes dark and the smilers
chase you.

# Be Aware 

As said before, the game files do not include the 3D models that can be seen
in the gameplay video, so there are items that do not have any model (just a
cube), you can just replace them, and you are good to go!

Although you will have two models (Almond Water and Pills Bottle) that i made
after the release of the gameplay :). 

# Links

Game: https://gamejolt.com/games/InTheBackrooms/802175
My Models: https://sketchfab.com/CrZZ
Youtube: https://www.youtube.com/@CrZ3D
