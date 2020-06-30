# TowerDefence
 
This is repository for Tower Defence game on Godot engine.

# Concept

At the beginning of the level, the base has 100% hp and 0 protection. With the initial money we buy (hire) protection. Enemies go in waves, each time more and more. If the enemy reaches the base, he begins to inflict damage on it. When xp base <= 0 we lost. For killing the enemy, we get money. For money we buy a new protection. The task is to hold out all the waves so that the xn base does not become <= 0.

# Enemy

Enemies differ in speed, base damage and type of damage (near or far), attack range, and hp. The enemy dies when his health is <= 0. The enemy moves along the shortest path to the base.

# Protection

Protection can be a building or a character. They differ in speed and type of attack (against melee or ranged combat). Damage by area is possible. Protection always deals damage to the nearest target depending on the type of defense (against melee or ranged combat). In order to buy protection, you need to save money and choose protection for installation. Then click on the place where you want to install protection.
