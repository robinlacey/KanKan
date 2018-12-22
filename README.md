[![Build Status](https://travis-ci.org/robinlacey/KanKan.svg?branch=master)](https://travis-ci.org/robinlacey/KanKan)

# Kan Kan 👯‍♂

Kan-Kan is an engine agnostic state and actions system. It's been written as a replacement to the legacy 
Quote](http://quote-game.com) state machine.


***Please note:*** KanKan is still in early development so things might change without warning. Hopefully for the better. It currently has not been optimised and uses Reflection, so performance may vary 

## What is it?

A KanKan will run a an action - known as a Karass. Karass can be combined `KarassThree = KarassOne + KarassTwo` to be run in parallel or passed into a KanKan as an Array `new []{ KarassOne, KarassTwo }` to be run sequentially.


### Goals
* To have a easily maintainable state/actions system that can handle complex behaviours.

* Avoid [spaghetti](https://en.wikipedia.org/wiki/Spaghetti_code) and [lasagne](http://wiki.c2.com/?LasagnaCode) code (See Why?) and not be tied to [God Objects](https://en.wikipedia.org/wiki/God_object) ('Managers', singletons etc)

* Built through [Test Driven Development](https://en.wikipedia.org/wiki/Test-driven_development) and ready for TDD systems. 

* Plays nice with [modern development good practices](https://en.wikipedia.org/wiki/SOLID). [DI](https://en.wikipedia.org/wiki/Dependency_injection) is passed into Karass via a factory to help with dependency inversion.
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

* A KarassFrame:
	* can have reusable logic with access to DI
	* can be passed different object types (usually as a unique constructor class/struct)
	* can receive messages from Karass
	* can be run alongside different object types
	* is created through a factory

* A KanKan:
	* Can run within any engine with a frame update method. Note: Focus has been on Unity and getting KanKan to play nice with `StartCoroutine`. Feedback on improving compatibility further is greatly appreciated.
	* Is lightweight and doesn't drop frames dropped between actions.
	* Should have no knowledge of the outside system


## How it works

### Structure of Karass


A Karass always has:  

1. Setup Action(s)
2. Array of FrameRequests
3. Teardown Action(s)


#### Setup
An array of `Action` or `void Setup(){}`

#### Teardown
An array of `Action` or `void Setup(){}`

#### FrameRequests
A FrameRequest is a tidy way of passing a RequestType object (such as a struct or class) to a IKarassFrame. Before a IKarassFrame can be run it must be:
 
1. `IKarassFrame<RequestType>` Registed with the `KarassDependancy`
2.  Have `RequestType` linked to `IKarassFrame<RequestType>` with `FrameFactory.RegisterRoute<RequestType,<IKarassFrame<RequestType>`

You can then new up a `RequestType something = new RequestType()` add some properties. The FrameRequest can be created `FrameRequest frameRequest = new FrameRequest(something)` and added to a Karass. 


#### Running order
All Setup Actions will be called before first Frame. All Teardown Actions will be called after last Frame.
If there are no Frames, Setup and Teardowns Actions will be called. 



## Example 
You might want an IKarassFrame to apply damage to a unit. We will want to change the amount of damage and, let's say, the impact message each time this is frame is called. It'll also hold information on which Unit did the damage and an array of all Units it should apply the damage to.

- 1) Create a struct to store the damage amount:

```
public struct DamageRequest
{
    public int OwnerID;
    public int[] HitUnitsID;
    public int Damage; 
    public string DamageMessage;
    
}
```
- 2) Create a IKarassFrame<DamageRequest> class. This should be as generic as possible so we can reuse it.

```
class ApplyDamage : IKarassFrame<DamageRequest>
{
    public DamageRequest RequestData { get; private set; }
    public string Message { get; }
    public bool Execute(string message, DamageRequest payload, IDependencies dependencies)
    {
        bool goToNextFrame = true;
        
        RequestData = payload;

        foreach (int hitUnitID in RequestData.HitUnitsID)
        {
            IUnit unit = dependencies.Get<IUnits>().GetUnit(hitUnitID);
            unit.ApplyDamage(RequestData.Damage, RequestData.DamageMessage);

        }
        return goToNextFrame;
    }
}
```

- 3) Register `IKarassFrame<DamageRequest>` -> `ApplyDamage `with DI and set Route `DamageRequest` -> `IKarassFrame<DamageRequest>` in `FrameFactory`. Below is a typical `Start` method (note: Start is run before any other frames)

```
private IKarassFactory _karassFactory;
private IDependencies _dependencies;
private IFrameFactory _frameFactory;

private void Setup()
{
    _dependencies = new KarassDependencies();
    _frameFactory = new FrameFactory(_dependencies); 
    _karassFactory  = new KarassFactory(_dependencies,_frameFactory);               
}
```
- 4) When adding a frame to a Karass all you need to do is create a new `DamageRequest` and make a `FrameRequest`. Note the below example has no Setup and Teardown and only one frame.

```
KarassFactory _karassFactory = new KarassFactory(_dependencies, _frameFactory);
 
DamageRequest applyDamage = new DamageRequest()
{
    HitUnitsID = new []{42,21,45},
    OwnerID = 42,
    Damage = 99,
    DamageMessage = "KAPOW!!"
};

FrameRequest applyDamageFrameRequest = new FrameRequest(applyDamage);
    
Karass applyDamageKarass = _karassFactory.Get(new List<Action>(), new List<Action>, new []{ applyDamageFrameRequest})
```

- 5) Fire up a KanKan to run the Karass:

```
KanKan kankan = new KanKan(applyDamageKarass, new KarassMessage());

kankan.MoveNext();
```

## Why?

In systems with multiple, complex, moving parts a state machine can become the glue that binds your components together. It can end up knowing everything about the system and - as it becomes more complex - becomes fragile and incredibly hard to test.
We found this when developing [Quote](http://quote-game.com). The SM was perfect for what we wanted at the beginning but, over the development duration, it grew, morphed and eventually became unmanageable. The moment we stopped attending to it daily it started to rot and - after some time away from the project - became unworkable.


### Naming
The words KanKan and Karass were taken from [Kurt Vonneguts 'Cats Cradle'](https://en.wikipedia.org/wiki/Cat%27s_Cradle).

*KanKan: the instrument that brings individuals into their karass (Cat's Cradle - Kurt Vonnegut)*

The CanCan is also a dance (👯‍♂) which ties in quite nicely...

*Karass: "We Bokononists believe that humanity is organized into teams, teams that do God's Will without ever discovering what they are doing. Such a team is called a karass by Bokonon"*`

## Getting Started
Detailed instructions coming soon. In the meantime please read the example and:

1. [Clone repo](https://help.github.com/articles/cloning-a-repository/)
2. [Compile to .dll](https://docs.microsoft.com/en-us/dotnet/core/deploying/deploy-with-cli)
3. [Import to engine](https://docs.unity3d.com/Manual/UsingDLL.html)
