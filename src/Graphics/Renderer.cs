using Combatsim.Core;
using Combatsim.Entities;
using Combatsim.Graphics;

namespace Graphic.Graphics{
	public class Renderer {
		private EntityManager entityManager;
		private List<List<char>> map;

		public Renderer(EntityManager entityManager, List<List<char>> map) {
			this.entityManager = entityManager;
			this.map = map;
		}

		public void render() {
			Console.SetCursorPosition(0, 0);

			for (int y = 0; y < map.Count; y++) {
				for (int x = 0; x < map[0].Count; x++) {
					foreach (Entity entity in entityManager.Entities) {
						if (entity.Position.X == x && entity.Position.Y == y) {
							Console.Write(entity.Character);
						} else {
							Console.Write(map[y][x]);
						}
					}

				}
				Console.WriteLine();
			}
		}
	}
}