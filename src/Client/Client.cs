using System.Drawing;
using Combatsim.Core;
using Combatsim.Entities;
using Combatsim.Graphics;
using Combatsim.Weapons;
using Graphic.Graphics;

namespace Combatsim.Client {
	public class Client { 
		private bool gameRunning = true;
		private Loop loop;
		private EntityManager entityManager;
		private List<List<char>> map;
		private Renderer renderer;

		public Client() {
			Thread inputThread = new Thread(handleInput);
			loop = new Loop(this, 10, 5);
			entityManager = new EntityManager(null);
			map = new MapGenerator().generateMap(50, 25, false);
			renderer = new Renderer(entityManager, map);

			inputThread.Start();
			Console.CursorVisible = false;
		}

		public void startGame() {
			entityManager.addEntity(new Soldier(map, entityManager, new Point(25, 12), new Point(0, 1)));

			while (gameRunning) {
				loop.gameLoop();
			}
		}

		public void render() {
			renderer.render();
		}

        public void update() {
			entityManager.updateEntities();
        }

		public void handleInput() {
			while(gameRunning) {
				ConsoleKeyInfo key = Console.ReadKey();
				if (key.Key == ConsoleKey.Q) {
					gameRunning = false;
					Environment.Exit(0);
				}
			}
		}
    }
}