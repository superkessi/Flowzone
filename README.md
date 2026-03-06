
<img width="1200" height="400" alt="flowzone_git_banner" src="https://github.com/user-attachments/assets/7669b7db-be58-4785-ba61-a78716ade8f5" />

<div align="center">
&nbsp;
  
**Flowzone is a challenging endless flyer, where the player glides through an atmospheric, procedurally generated world.** 

**Dodge Obstacles and collect skills and chase the high score, while the speed and challenge progressively increases.**

༄ °. ⋆༺ 𖤓 ༻⋆. ° ⟡ °. ⋆༺ ☾  ༻⋆. ° ༄

<img alt="flowzone_gameplay_gif" src="https://github.com/user-attachments/assets/2d7fb845-3cf4-4fe4-8197-c6ab54f4e87b">

༄ °. ⋆༺ 𖤓 ༻⋆. ° ⟡ °. ⋆༺ ☾  ༻⋆. ° ༄
</div>
&nbsp;

Flowzone was developed as a **10 week** long, student project at [**School4Games**](https://www.school4games.net) in 2025. 

**Engine     :** Unity 6000.0.51f1 

**Tools        :** Perforce (Version Control) 

**Team size :** 11 full time (1 producer, 3 game designer, 2 engineers, 5 artists)

Play it [here](https://s4g.itch.io/flowzone)

&nbsp;
<div align="center">
༄ °. ⋆༺ 𖤓 ༻⋆. ° ⟡ °. ⋆༺ ☾  ༻⋆. ° ༄
</div>

## My Responsibilities

I was one of [two](https://github.com/Artur-92) engineers working on Flowzone and I was primarily responsible for gameplay systems, the core architecture, as well as performance optimisation during the end of the development.

My main contributions include :
- Building the **Player controller** and the according **State Machine**
- Designed and developed the **Modular world generation** and implemented **object pooling** for the individual modules
- A lot of refactoring and getting my hands on performance optimisation

&nbsp;
<div align="center">
༄ °. ⋆༺ 𖤓 ༻⋆. ° ⟡ °. ⋆༺ ☾  ༻⋆. ° ༄
</div>

## Highlights

### Modular world generation
The world is split up into multiple reusable modules that are spawned ahead of the player as he progresses through the game.

To maintain a illosion of an endless world, additional modules are spawned along the x axis as the player moves sideways, so the world is extendet into any direction.

When the base World gernation was set, i soon ran into performance issues, when loading the tiles. To solve this problem i made use of an object pooling system. 

I also experimented with preloading the modules at startup, to reduce runtime lag spikes, when spawning new modules. 

#### Key Components
- [**Module Spawner**](https://github.com/superkessi/Flowzone/blob/main/Assets/Scripts/Modules/S_ModuleSpawner.cs)
- [**Module Pool**](https://github.com/superkessi/Flowzone/blob/main/Assets/Scripts/Modules/S_ModulePool.cs)
- [**Module Movement**](https://github.com/superkessi/Flowzone/blob/main/Assets/Scripts/Modules/S_ModuleMovement.cs) 

### Player State
Player movement is a central pillar of our game design, so i decided to make a new State Machine to keep things modular and easier to debug. 

Each movemetn behavoir is handle in its own state (like Idle, Jump start, Jump end,..), wich also made handeling the transitions bewtween the states very simple.


#### Key Compnents
- [**Player**](https://github.com/superkessi/Flowzone/blob/main/Assets/Scripts/Player/S_Player.cs)
- [**Player Base State**](https://github.com/superkessi/Flowzone/blob/main/Assets/Scripts/Player/StateMachine/S_MovementState.cs)
- [**Player States**](https://github.com/superkessi/Flowzone/tree/main/Assets/Scripts/Player/StateMachine)
