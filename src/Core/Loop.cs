using System.Collections.Concurrent;
using System.Diagnostics;  // Adds stopwatch

namespace Combatsim.Core {
	public class Loop {
		private Client.Client client;
		private int fps, ups;
		private double frameTime;
		private double updateTime;
		private double fDeltaTime, uDeltaTime;
		private Stopwatch timer = Stopwatch.StartNew();
		private long startTime;

		public Loop(Client.Client client, int fps, int ups) {
			this.client = client;
			this.fps = fps;
			this.ups = ups;

			initialize();
		}

		public void initialize() {
			frameTime = 1000 / fps;
			updateTime = 1000 / ups;
			fDeltaTime = 0;
			uDeltaTime = 0;  // frame/update delta time
			startTime = timer.ElapsedMilliseconds;
		}

		public void gameLoop() {
			long currentTime = timer.ElapsedMilliseconds;
			fDeltaTime += currentTime - startTime;
			uDeltaTime += currentTime - startTime;
			startTime = currentTime;

			if (uDeltaTime >= updateTime) {
				client.update();
				uDeltaTime -= updateTime;
			}

			if (fDeltaTime >= frameTime) {
				client.render();
				fDeltaTime -= frameTime;
			}
		}
	}
}