using System.Drawing;
using System.Reflection;
using Combatsim.Core;
using Combatsim.Entities;

namespace Combatsim.Weapons {
	public class Firearm {
		protected List<List<char>> map;
		protected EntityManager entityManager;

		protected Point position;
		protected Point direction;
		protected int ammoCapacity;
		protected List<Bullet> ammo = new List<Bullet>();
		protected bool ammoInserted;
		protected List<Bullet> chamber = [];

		public Firearm(List<List<char>> map, EntityManager entityManager, Point position, Point direction, int ammoCapacity) {
			this.map = map;
			this.entityManager = entityManager;
			this.position = position;
			this.direction = direction;
			this.ammoCapacity = ammoCapacity;
			ammo.Capacity = this.ammoCapacity;
			chamber.Capacity = 1;

			reload();			
		}

		public void reload() {
			for (int i = 0; i < ammoCapacity - ammo.Count; i++) {
				ammo.Add(new Bullet(map, entityManager, position, direction, 25, 0.0));
			}
			// load a round into the chamber if it's empty.
			if (!(chamber.Count == 0)) {
				chamber.Add(new Bullet(map, entityManager, position, direction, 25, 0.0));		
				ammo.RemoveAt(0);
			}
		}

		public void fire() {
			chamber[0].Shot = true;
			// feed next round into chamber
			chamber.Add(new Bullet(map, entityManager, position, direction, 25, 0.0));
			ammo.RemoveAt(0);
		}
	}
}