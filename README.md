# Kan Kan

Kan-Kan is a game engine agnostic state and actions system. It's been written as a replacement to the legacy 
Quote](http://quote-game.com) state machine.


***Please note:*** KanKan is still in early development so things might change without warning. Hopefully for the better. 

## What is it?

A KanKan will run a an action - known as a Karass. Karass can be combined `Karass1 + Karass2 = Karass3` to be run in parallel or passed into a KanKan as an Array `new []{Karass1,Karass2` to be run sequentially.


### Goals
* To have a easily maintainable state/actions system that can handle complex behaviours.

* Avoid [spaghetti](https://en.wikipedia.org/wiki/Spaghetti_code) and [lasagne](http://wiki.c2.com/?LasagnaCode) code (See Why?) and not be tied to [God Objects](https://en.wikipedia.org/wiki/God_object) ('Managers', singletons etc)

* Built through [Test Driven Development](https://en.wikipedia.org/wiki/Test-driven_development) and ready for TDD systems. 

* Plays nice with [modern development good practices](https://en.wikipedia.org/wiki/SOLID). [DI](https://en.wikipedia.org/wiki/Dependency_injection) is passed into Karass via a factory to help with dependency inversion. (note: strongly advise using an established/robust DI solution, not the placeholder example found in the project)
* A Karass:
	* can be run over multiple frames
	* can be combined with another Karass. eg:
`Karass quickDash = DashToPoint + PlayDashParticles + ShakeScreen`
`OnButtonPress(Player.KanKan.Run(quickDash));`
	* can be run sequentially with other Karass. eg:
`NPC.Kankan(new[]{RunToPlayer,PunchPlayer,Dance)`
	* can take messages and sync with external systems. eg:
`NPC.Kankan(RunAwayFromPlayer)`
`OnPlayerHit(Kankan.SendMessage(“punch NPC"));`


* A KanKan:
	* Can run within any game engine with a frame update method. Note: Focus has been on Unity and getting KanKan to play nice with `StartCoroutine`. Feedback on improving compatibility further is greatly appreciated.
	* Is lightweight and doesn't drop frames dropped between actions.
	* Should have no knowledge of the outside system



## How it works

### Structure of Karass


A Karass always has:  


1. Setup Action(s)
2. Array of Frames(s)
3. Teardown Action(s)


#### Setup
An array of `Action` or `void Setup(){}`

#### Teardown
An array of `Action` or `void Setup(){}`

#### Frames
A single frame is a method that takes a string parameter and returns a boolean: `Func<string,bool>` or `bool Frame(string message){} `

The string is the message, which is sent from the `KarassMessage` inside the KanKan. The return boolean tells the Karass whether to move onto the best frame.

#### Running order
All Setup Actions will be called before first Frame. All Teardown Actions will be called after last Frame.
If there are no Frames, Setup and Teardowns Actions will be called.




## Why?

In systems with multiple, complex, moving parts a state machine can become the glue that binds your components together. It can end up knowing everything about the system and - as it becomes more complex - becomes fragile and incredibly hard to test.
We found this when developing [Quote](http://quote-game.com). The SM was perfect for what we wanted at the beginning but, over the development duration, it grew, morphed and eventually became unmanageable. The moment we stopped attending to it daily it started to rot and - after some time away from the project - became unworkable.

In the end we ended up with nightmare coroutines that felt a bit like: 
>IEnumerator Punch(){
>
>yield return PlayerManager.GetPlayer().Movement.GoTo(NPCManager.GetNearestNPCTo(PlayerManager.GetPlayer().Position)
>
>AnimationManager.TriggerAnimation(PlayerManager.GetPlayer(), "Punch")
>
>ParticlesManager.Play("Kapow",PlayerManager.GetPlayer().Position)
>
>EffectsManager.ScreenShake()
>
>}`

Punch knows everything about the entire system so, if ParticleManager is changed, the *whole lot* blows up. 
[Things don't have to be like this...](https://cleancoders.com/)

### Naming
The words KanKan and Karass were taken from [Kurt Vonneguts 'Cats Cradle'](https://en.wikipedia.org/wiki/Cat%27s_Cradle).

*KanKan: the instrument that brings individuals into their karass (Cat's Cradle - Kurt Vonnegut)*


*Karass: "We Bokononists believe that humanity is organized into teams, teams that do God's Will without ever discovering what they are doing. Such a team is called a karass by Bokonon"*`

## Getting Started
Detailed instructions coming soon. In the meantime:

1. [Clone repo](https://help.github.com/articles/cloning-a-repository/)
2. [Compile to .dll](https://docs.microsoft.com/en-us/dotnet/core/deploying/deploy-with-cli)
3. [Import to engine](https://docs.unity3d.com/Manual/UsingDLL.html)
