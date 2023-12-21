using System.Dynamic;
using Combatsim.Entities;

namespace Combatsim.Core {
	public class EntityManager {
		private List<Entity> entities = new List<Entity>();
		private List<Soldier> soldiers = new List<Soldier>();
		
		public EntityManager(Entity[]? startEntities) {
			if (startEntities != null) {
				for (int i = 0; i < startEntities.Length; i++) {
					addEntity(startEntities[i]);
				}
			}
		}

		public void update() {
			foreach (Entity entity in entities.ToList()) {
				entity.update();
			}

			foreach (Soldier soldier in soldiers.ToList()) {
				soldier.update();
			}
		}

		public void addEntity(Entity entity) {
			entities.Add(entity);
		}

		public void addSoldier(Soldier soldier) {
			soldiers.Add(soldier);
		}

		public void removeEntity(Entity entity) {
			if (entities.Contains(entity)) {
				entities.Remove(entity);
			}
			Console.WriteLine($"removeEntity({entity}) is not valid because the entity doesn't exist.");
		}


		public List<Entity> Entities {
			get => entities;
			set => entities = value;
		}

		public List<Soldier> Soldiers {
			get => soldiers;
			set => soldiers = value;
		}
	}
}
