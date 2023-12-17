using Combatsim.Entities;

namespace Combatsim.Core {
	public class EntityManager {
		private List<Entity> entities = new List<Entity>();
		
		public EntityManager(Entity[]? startEntities) {
			if (startEntities != null) {
				for (int i = 0; i < startEntities.Length; i++) {
					addEntity(startEntities[i]);
				}
			}
		}

		public void updateEntities() {
			foreach (Entity entity in entities) {
				if (entity != null) {
					entity.update();
				}
			}
		}

		public void addEntity(Entity entity) {
			entities.Add(entity);
		}


		public void removeEntity(Entity entity) {
			if (entities.Contains(entity)) {
				entities.Remove(entity);
			}
			Console.WriteLine($"removeEntity({entity}) is not valid because the entity doesn't exist.");
		}

		public List<Entity> Entities {
			get => entities;
		}
	}
}
