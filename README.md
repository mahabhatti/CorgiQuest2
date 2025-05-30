
SE 320 Final Project: Corgi Quest 

Group Members: Maha Bhatti, Callie Tucker, Jane Jung

Details:

Unity version: 6000.0.47f1

Cinemachine package required

Instructions to Run:
After cloning the repo, open Unity Hub, click Add, then select the CorgiQuest2 folder
to open the project in Unity. In the Editor, press Play to run the game; 
use arrow keys to move the corgi, E to interact with chests. To win the game, you must 
open every chest in all biomes avoiding obstacles and fighting enemies, and walk 
through the exit. 

Design Decisions and Architecture:
We mapped each core responsibility onto the MVC layers. We have models or managers for 
each element of the game. For example, we have an InventoryManager, CombatManager, PlayerController, and a Game Controller.
These classes focus on storing data and enforcing rules. The inventory manager holds the
players treasures that are found in the chests. It enforces the adding and removing of a treasures. 
Combat manager holds all combat related data and enforces healing and damaging rules. Player controller
holds the player's statistics. Game controller deals with the overall session states like starting,
ending, or pausing the game. 

The visuals in the game are laid out in unity. Some of them include the chests, the closing and 
opening of chests, the collection of items. The animations of the health bar adjusting to the 
player's statistic, the victory and loss panels are also examples of some of the visual components of the game.

The game controller acts as the controller for the entire game. It is responsible for setting up all the data, and 
initializes the UI. It also checks for if the victory and death overlays. 


Singleton: 

We applied the Singleton pattern to our core managers so there’s only one source for key game systems 
and global access to them. For example,  InventoryManager and GameController are guaranteed to have only
one instance exist. In every script we are able to call InventoryManager.Instance or GameController.Instance without 
having to pass references to them.

Observer:

Our managers broadcast state changes to let view or controller scripts react to only the events they care about. 
For example, CombatManager raises OnHealthChanged(currentHealth), and the health bar view updates accordingly. 
This helped us eliminate tight coupling and make it easy to add new reactions to any game event.

Strategy: 

The player controller is an example of a strategy pattern because it defines a common interface for different 
behaviors. It holds a reference to whichever one it needs at runtime. The controller reads input and checks collisions,
but when the player needs to move, it simply calls the Move method. That method waits for input rather and responds
immediately rather than checking for constant updates. 


