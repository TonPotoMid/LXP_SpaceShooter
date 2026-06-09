# Idée pour le jeu : Learning XP

Voici la structuration claire, ordonnée et catégorisée du concept de jeu, basée strictement sur vos indications.

---

## Caméra & Visuels
* **Perspective :** Jeu en 3D avec une vue du dessus (Top-down).

## Contrôles & Déplacement
* **Contrôles du joueur :** Déplacement au clavier via les touches **ZQSD**.
* **Contraintes de mouvement du joueur :** Le joueur est restreint et ne peut se déplacer que sur une route dédiée.
* **Contraintes de mouvement des ennemis :** Les ennemis ne peuvent pas se déplacer librement non plus ; ils suivent également les contraintes des routes/chemins.

## Comportement des Ennemis
* **Type unique :** Il n'existe qu'un seul type d'ennemi dans le jeu.
* **Apparition (Spawn) :** Ils apparaissent à des endroits aléatoires sur la carte.
* **Vitesse :** Ils se déplacent légèrement plus rapidement que le joueur.
* **Logique de déplacement (IA) :**
  * *Par défaut :* Ils se déplacent de manière aléatoire.
  * *En chasse :* Dès qu'ils détectent le joueur à une distance précise, ils se mettent à le traquer.
* **Mort :** Une fois détruits par le joueur (sous l'effet d'un power-up), ils **ne réapparaissent pas**.

## Collectables & États du Joueur
* **Quantité :** 4 collectables au total par manche.
* **Apparition :** Placés à des endroits aléatoires sur la carte au début de chaque manche.
* **Persistance :** Une fois qu'un collectable est ramassé, il disparaît définitivement pour le reste de la manche.
* **Effet (Power-up) :** Ramasser un collectable déclenche un état spécial :
  * Le joueur devient **invincible pendant 10 secondes**.
  * Le joueur acquiert la capacité de **détruire les ennemis instantanément** lors d'une collision.

## Structure des Manches & Progression
* **Durée :** Chaque manche dure précisément **60 secondes**.
* **Condition de transition :** À la fin du chrono des 60 secondes, le joueur est automatiquement téléporté à son point de spawn initial pour commencer la manche suivante.
* **Évolution de la difficulté :** Le nombre d'ennemis augmente de **+1 à chaque nouvelle manche**.

## Score & Économie
* **Système de points :** Aucun système de points (pas de score chiffré par action).
* **Calcul du score :** Le score est uniquement basé sur le **nombre de manches survécues**.

## Gameplay & Conditions de fin (Game Over)
* Le jeu s'arrête immédiatement lorsque le joueur est touché par un ennemi (hors état d'invincibilité).