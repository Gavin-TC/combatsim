
using System.Drawing;
using Combatsim.Core;
using Combatsim.Weapons;

namespace Combatsim.Entities {
	public class Soldier : Entity{
		// protected Firearm firearm;
		protected Point direction;
		protected bool shot;
		protected enum lifeStates {
			DEAD,
			ALIVE,
			DYING,
			INJURED,
		}
		protected enum moveStates {
			IDLE,
			MOVING
		}
		protected enum combatStates {
			SHOOTING,
			IDLE
		}

		public Soldier(List<List<char>> map, EntityManager entityManager, Point position, Point direction) : base(map, entityManager, position, '☺') {
			this.map = map;
			this.position = position;
			this.direction = direction;

			// this.firearm = firearm;	
			// firearm.fire();
			shot = true;
		}

        public override void update()
        {
            base.update();

			if (shot) {
				shoot();
			}
        }

        public void wander() {
			int randX = new Random().Next(-1, 2);
			int randY = new Random().Next(-1, 2);

			position.X += randX; 
			position.Y += randY; 
		}

		public void shoot() {
			Bullet bullet = new Bullet(map, entityManager, Position, direction, 50, 0.1);
			entityManager.addEntity(bullet);
		}
    }
}