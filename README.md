# The-Hive
Threading in C# final assignment

# Background
Hive is an idle game where you take control of an ant hive. The goal is to expand your  hive to be as big as possible. The game will make use of threading techniques to keep the game responsive.
To expand the hive the player will need to gather two resources: ants and nectar. Nectar can be automatically collected by ants or manually collected by nectar drops that will periodically appear on the players screen. Using nectar the player can create more ants or expand their hive. 
Nectar is collected by ants on the hive map. The hive map can be viewed by the player and shows ants moving towards nectar drops. Only a maximum of 10 ants will be visualized on the hive map, but the amount of nectar collected from each drop is dependent on the total amount of ants in the colony.
 The hive can only support a limited amount of ants, to increase the maximum amount of ants in the hive the hive needs to be expanded. After expanding 7 times, the player wins the game.

## Specifications
* Game Engine: XNA Framework MONOGame 3.8.1.303
* Programming Language: C#
* Threading techniques: mutex, tasks, semaphore, and delegates.
* Graphics: 2D graphics, sprite-based animation.
* Platforms: Windows operating system (Windows 10 and later versions).

# Concept
The Hive’s mechanics will implement the following concepts:

## Counters
There are three counters which track the number of ants, the amount of nectar, and the size of the hive. The counters will increase as the player acquires more ants, nectar, and expands the hive.
## Shops

Shops will be used to purchase ants and hive expansions with nectar. The cost of each increases as the number of ants, amount of nectar, and size of the hive go up.

## Drops
Nectar drops randomly appear on the player's screen. There are regular and golden drops. Regular drops offer a single nectar, while golden drops double the current nectar count but are much rarer.

## Hive Map

The hive map shows all owned ants and automatically collects nectar that spawns randomly on the map. The ants move towards a nectar drop and collect it. Once a nectar drop has been collected, ants with that drop as a target try to find the next nearest nectar drop. If there are none, they wait until one spawns.

# Threading techniques:
The Hive game will make use of the following threading techniques:

## Locks for ensuring nectar doesn’t get claimed twice
To ensure that two ants cannot claim the same nectar object, locks will be used to ensure that nectar can only be claimed by one ant.

## Tasks for buttons, ant finding pathfinding and ants claiming nectar
Tasks will be used for handling events triggered by button clicks. These buttons include buying from shops and claiming nectar drops.

Tasks will also be used to both orient the ants in the correct direction to their nearest nectar and for claiming the nectar. This calculation for orienting ants is fairly simple, but using tasks will allow the calculation to happen without freezing the game's UI. In addition, this approach can be expanded upon to support more complicated path-finding algorithms in the future.

## Semaphore for counters and for managing ant pathfinding. 
To make sure that the counter remains correct, a binary semaphore (a semaphore with a size of one) will be implemented for all counters. The semaphore will freeze changes to the counter until the previous calculations are all done. This will ensure that no race conditions occur when multiple threads try to access the counters at the same time. A semaphore was chosen above a mutex as mutexes cannot be used asynchronously.

The number of tasks for the hive map will also be limited by a semaphore. This way, not too many calculations for tasks will be done at the same time, keeping the requirements of running The Hive low. Ants will have to wait until there is room in the semaphore if the semaphore is full. In the settings the semaphore count can be altered.

## Async I/O for saving the settings page
In the settings page the semaphore number and ant color will be set. Saving will transport these settings to a local file which will be retrievable. Writing and reading to and from this file will be done with async I/O so the application doesn’t stall


