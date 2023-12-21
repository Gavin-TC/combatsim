using System.Security.Cryptography;

namespace Combatsim.Graphics {
	public class MapGenerator {
		public List<List<char>> generateMap(int mapWidth, int mapHeight) {
			List<List<char>> map = new List<List<char>>();
			Random random = new Random();

			// TODO: add generation of walls or something
			char[] possibleTiles = ['\'', '.', ',', ' ', ' ', ' ', ' ', ' ']; 

			for (int y = 0; y < mapHeight; y++) {
				List<char> row = new List<char>();
				
				for (int x = 0; x < mapWidth; x++) {
					row.Add(possibleTiles[random.Next(possibleTiles.Length)]);
				}
				map.Add(row);
			}
			return map;
		}
	}
}