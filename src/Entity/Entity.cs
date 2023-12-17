using System.Drawing;
using System.Net.Mail;
using Combatsim.Core;
using Microsoft.VisualBasic;

namespace Combatsim.Entities {
	public class Entity {
		protected List<List<char>> map;
		protected EntityManager entityManager; 

		protected char character = 'S';
		protected Point position;
		protected bool queuedForDeletion = false;

		public Entity(List<List<char>> map, EntityManager entityManager, Point position, char character=' ') {
			this.map = map;
			this.entityManager = entityManager;
			this.character = (character != ' ') ? character : character;
			this.position = position;
		}

		public virtual void update() {
			if (isOutOfBounds()) {
				queuedForDeletion = true;
			}
		}

		public bool isOutOfBounds() {
			if (position.X < 0 || position.Y < 0 || position.X > map[0].Count || position.Y > map.Count)
				return true;
			return false;
		}


		// Getters and setters
		public char Character {
			get => character;
			set => character = value;
		}

		public Point Position{
			get => position;
			set => position = value;
		}

		public bool QueuedForDeletion {
			get => queuedForDeletion;
			set => queuedForDeletion = value;
		}
	}	
}