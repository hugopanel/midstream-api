>[!note] Version
>29 Mai 2024

----

**Project**
Etiquette à laquelle des données font référence mais qui ne contient pas réellement de trucs (à part des propriétés).
Il ne faut pas qu'une donnée appartiennent à un projet mais y soit liée.

```json
{
	"id": "",
	"name": "",
	"description": "",
	"beginning_date": ""
}
```

**Task**
Truc à faire.

```json
{
	"title": "",
	"description": "",
	"type": "", // Task, Bug, etc.
	"beginning_date": "",
	"end_date": "",
	"tag": "", // Front, Back, etc. pour catégoriser plus facilement mais sans structure prédéfinie
	"priority": "",
	"status": ""
}
```

Liens possibles :
```json
{
	"author": "person",
	"assigned_to": ["person", "team"],
	"relates_to": [], // Liens en général mais pas forcément bloquant, si l'utilisateur veut le préciser
	"blocks": [], // Les tâches qui dépendent de celle-ci
	"blocked_by": [] // Les tâches qui bloquent celle-ci
}
```


**File**
Fichier physique sur le disque (comme les blobs dans git).
Ex: fichiers de code, images, musiques, etc.

```json
{
	"id": "",
	"name": "",
	"path": "", // Chemin sur le disque
	"description": "", // Facultatif, peut être utile pour partager un fichier ou décrire son contenu / utilité
	"extension": "" // .pdf, .txt, .mp3
}
```


**Document**
Ensemble de sections

```json
{
	"id": "",
	"title": "",
	"content": [] // References to section / paragraphs / other type (file, task, etc.)
}
```

**Section**
Contient un ou plusieurs paragraphes et de liens vers des fichiers / tâches, etc.

```json
{
	"id": "", // A voir si nécessaire
	"title": "", // A voir pour l'implémentation et l'UX
	"content": [] // References to sections / paragraphs / other type (file, task, etc.)
}
```

**Paragraph**
Un paragraphe appartient à un document (il ne peut pas être modifié autre part que depuis ce document, sinon ça causerait des problèmes où le paragraphe pourrait perdre son sens partout où il est référencé)

**Reference**
Référence vers une donnée depuis une autre (syn. : lien, mais on préfère le mot "référence" pour éviter de confondre).

**Permission**
```json
{
	"action": "", // Name
	"description": ""
}
```

Exemple permission : ajouter une tâche

+ Liste d'autorisations pour chaque action (ex: quelqu'un peut ajouter une tâche mais pas un responsable)

**User**
```js
{
	"name": "",
	"firstname": "",
	"lastname": "",
	"email": "",
	"password": "",
	"salt": "",
	"roles": [
		// Liste des rôles
	]
}
```

**Role**
Ensemble de permissions auquel appartient des utilisateurs.
Un utilisateur peut avoir plusieurs rôles.
Chaque rôle appartient à un projet. On partage pas les mêmes rôles selon les projets. 

```js
{
	"name": "",
	"permissions": [
		// Liste de permissions
	],
	"project": "" // Project auquel appartient le rôle
}
```

**Team**
A créer ?

**Module**
```js
{
	"name": "",
	"description": "",
	"author": "", // Auteur du module
	"permissions": [
		{
			"action": "",
			...
		}
	],
	"react_path": ""
}
```

Créer une interface pour les modules