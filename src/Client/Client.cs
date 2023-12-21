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
			map = new MapGenerator().generateMap(100, 50);
			renderer = new Renderer(entityManager, map);

			inputThread.Start();
			Console.Title = "Combatsim";
			Console.CursorVisible = false;
			Console.SetWindowSize(100, 50);
		}

		public void startGame() {
			entityManager.addSoldier(new Soldier(map, entityManager, new Point(25, 15), new Point(1, 1), "UKR"));
			entityManager.addSoldier(new Soldier(map, entityManager, new Point(40, 20), new Point(1, 1), "RUS"));

			while (gameRunning) {
				loop.gameLoop();
			}
		}

		public void render() {
			renderer.render();
		}

        public void update() {
			entityManager.update();
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