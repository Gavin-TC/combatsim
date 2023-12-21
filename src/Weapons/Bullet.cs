using System.Numerics;
using Combatsim.Entities;
using System.Drawing;
using System.Data;
using Combatsim.Core;

namespace Combatsim.Weapons {
	public class Bullet : Entity {
		protected Point velocity;  // velocity is calculated based on type (for now)
		protected Point direction;
		protected int damage;
		protected double accuracy;  // variation left and right per update
		protected int accuracyDirection;  // direction += (int) accuracyDirection
		protected bool shot;
		protected enum BulletType {
			AP,
			HP,
			FMJ
		}

		public Bullet(List<List<char>> map, EntityManager entityManager, Point position, Point direction, int damage, double accuracy)
			: base(map, entityManager, position, '*') {
			this.map = map;
			this.entityManager = entityManager;
			this.position = position;

			this.direction = direction;
			this.damage = damage;
			this.accuracy = accuracy;

			shot = true;
			accuracyDirection = new Random().Next(-1, 2);
			if (accuracyDirection == 0) accuracyDirection = 1;
		}

        public override void update()
        {
			base.update();
			if (shot) {
				shoot();
			}
        }

        public void shoot() {
			position.X += direction.X + (int) (accuracy * accuracyDirection);
			position.Y += direction.Y + (int) (accuracy * accuracyDirection);	
		}

		// Getters and setters
		public Point Velocity {
            get => velocity;
			set => velocity = value;
		}

		public Point Direction {
			get => direction;
			set => direction = value;
		}

		public bool Shot {
			get => shot;
			set => shot = value;
		}
	}

}