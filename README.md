# 4482-app-1
 
Completed Ruby's Adventure: 2D Beginner

Tutorials up to and including CineMachine, as well as Visual Styling - UI/HUD and Build, Run, Distribute

https://learn.unity.com/project/ruby-s-2d-rpg?uv=2020.3

# Additions

Enemy robots do not stay in the Fixed state indefinitely, they degrade after a number of seconds (default 10), which returns them to their previous hostile behavior

Ruby has a fire rate cap on her cogs (default 1/s) to prevent spamming

Ruby has a limited number of cogs she can hold (default 12), and cannot launch any more once she runs out

To address this, ammo pickups have been added (gives 10)

To monitor the ammo system, and additional ammo slider was created (practically identical to the hp slider)

Added a Overdrive pickup using the provided mushrooms sprite. This powerup lasts for a short period of time (default 5s) and grants increased movement speed and fire rate (1.5x and 4x respectively) to Ruby, and also causes Ruby not to consume ammo during its duration. Players can tell when the Overdrive is active as the screen gets covered by a yellow overlay.




