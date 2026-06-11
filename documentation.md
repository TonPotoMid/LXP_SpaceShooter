# LXP_SpaceShooter 

Projet *LXP_SpaceShooter*. Cette branche implémente les mécaniques fondamentales du gameplay de type Space Shooter, notamment un système d'I.A. en machine à états, un spawner d'ennemis par vagues/manches sur des positions fixes, et la gestion des collisions de base.

---

##  Fonctionnalités Implémentées

### 1. Système de Vagues et Spawner (`EnnemySpawner.cs`)
Le spawner gère l'apparition des vagues d'ennemis (manches) de manière totalement automatisée via des Coroutines Unity.
* **Positions Fixes :** Les ennemis apparaissent aléatoirement parmi 10 coordonnées 3D prédéfinies sur la carte.
* **Difficulté Progressive :** Le nombre d'ennemis générés est égal au numéro de la manche actuelle (Ex: 3 ennemis à la manche 3).
* **Durée Dynamique :** La durée d'une manche est calculée dynamiquement (`Manche * 5 secondes`).
* **Nettoyage automatique :** À la fin de chaque manche, tous les ennemis encore vivants sur la carte sont automatiquement détruits (`ClearEntity()`) avant le passage à la manche suivante.

### 2. Intelligence Artificielle Évolutive (`AgentController.cs`)
Les ennemis utilisent un composant `NavMeshAgent` combiné à une **Machine à États (State Machine)** pour basculer entre deux comportements :
* **Mode Roaming (Patrouille) :** L'agent navigue aléatoirement d'un point à un autre parmi ses positions de patrouille. Une sécurité empêche l'I.A. de choisir deux fois de suite le même point.
* **Mode Chasing (Poursuite) :** Dès que le joueur entre dans la zone de détection (`distanceDetection`), l'I.A. abandonne sa patrouille et fonce sur le joueur en temps réel. Si le joueur s'échappe, l'I.A. reprend sa patrouille.

### 3. Contrôleur d'Interface Graphique (`UiController.cs`)
* Centralise la variable globale et `static` de la manche actuelle (`UiController.manche`).
* Met à jour en temps réel l'affichage du texte à l'écran à l'aide de **TextMeshPro**.

### 4. Gestion des Collisions (`GestionCollision.cs`)
* Détecte les impacts physiques physiques entre les GameObjects grâce à `OnCollisionEnter`.
* Si un objet avec le script percute le joueur (Tag `"Player"`), le joueur est instantanément détruit de la scène.

---

##  Configuration Requise dans l'Éditeur Unity

Pour que l'ensemble des scripts fonctionne correctement sans erreurs, assure-toi de configurer les éléments suivants dans ton projet :

### Configuration du Joueur (Player)
1. Sélectionne ton GameObject joueur.
2. Dans l'Inspecteur, assigne-lui le **Tag** officiel : `Player`.
3. Assure-toi qu'il possède un composant **Collider** (Box, Capsule, ou Sphere).

### Configuration du Spawner d'Ennemis
1. Crée un GameObject vide nommé `EnnemySpawner` et attache-y le script `EnnemySpawner.cs`.
2. Glisse le Prefab de ton ennemi dans la case `Ennemy Pre Fab`.
3. Ajuste la valeur du `Cooldown` (ex: `1` pour 1 seconde d'intervalle entre chaque apparition d'ennemi).

### Configuration de l'Ennemi (I.A.)
1. Ton prefab ennemi doit posséder un composant `NavMeshAgent`.
2. Attache le script `AgentController.cs`.
3. Glisse ton GameObject joueur dans la case `Player` et configure la `Distance Detection`.
4. Attache le script `GestionCollision.cs` sur le Prefab ou son visuel pour activer les dégâts au joueur.

---

##  Git & Workflow

### Ignorer les fichiers temporaires
Le projet est configuré pour ignorer les fichiers locaux et temporaires générés par Visual Studio (dossiers `.vs/`). Si tu rencontres une erreur de permission lors d'un `git add`, ferme Visual Studio et assure-toi que ton fichier `.gitignore` contient bien la règle suivante :
```text
**/[Dd]ebug/
**/.vs/
/[Mm]y project/.vs/