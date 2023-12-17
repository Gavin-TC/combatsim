using System.Security.Cryptography;

namespace Combatsim.Graphics {
	public class MapGenerator {
		public List<List<char>> generateMap(int mapWidth, int mapHeight, bool emptyMap = false) {
			List<List<char>> map = new List<List<char>>();
			Random random = new Random();

			char[] possibleTiles = ['.', ' ', ',']; 
            if (emptyMap) possibleTiles = [' ', ' ', ' '];

			for (int y = 0; y < mapHeight; y++) {
				List<char> row = new List<char>();
				
				for (int x = 0; x < mapWidth; x++) {
					row.Add(possibleTiles[random.Next(3)]);
				}
				map.Add(row);
			}
			return map;
		}
	}
}