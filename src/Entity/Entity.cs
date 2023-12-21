using System.Drawing;
using System.Net.Mail;
using Combatsim.Core;
using Microsoft.VisualBasic;

namespace Combatsim.Entities {
	public class Entity {
		protected List<List<char>> map;
		protected EntityManager entityManager; 
		protected int ticks = 0;

		protected char character = 'S';
		protected Point position;

		public Entity(List<List<char>> map, EntityManager entityManager, Point position, char character=' ') {
			this.map = map;
			this.entityManager = entityManager;
			this.character = (character != ' ') ? character : character;
			this.position = position;
		}

		public virtual void update() {
			isOutOfBounds();
			tick();
		}

		public bool isOutOfBounds() {
			if (position.X < 0 || position.Y < 0 || position.X > map[0].Count || position.Y > map.Count)
				return true;
			return false;
		}

		public void tick() {
			if (ticks > 8) {
				ticks = 1;
			}
			ticks++;
		}


		// Getters and setters
		public int Ticks {
			get => ticks;
			set => ticks = value;
		}

		public char Character {
			get => character;
			set => character = value;
		}

		public Point Position{
			get => position;
			set => position = value;
		}
	}	
}