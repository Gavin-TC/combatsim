using System.Drawing;
using Combatsim.Core;
using Combatsim.Weapons;

namespace Combatsim.Entities {
	public class Soldier : Entity{
		protected Point direction;
		protected bool shot;
		protected string faction;
		protected int health = 100;

		protected enum LifeStates {
			DEAD,
			ALIVE,
			DYING,
			INJURED,
			HELPED,  // as in being helped
		};
		protected enum MoveStates {
			IDLE,
			MOVING  // moving towards a target
		};
		protected enum CombatStates {
			SHOOTING,
			HELPING,  // as in helping a casualty
			FINDTARGET,
			IDLE
		};
		protected LifeStates lifeState { get; set; }
		protected MoveStates moveState { get; set; }
		protected CombatStates combatState { get; set; }
		protected List<object> stateQueue = new List<object>();
		protected bool statesQueued = false;

		protected Point targetLocation; 
		protected Soldier targetEnemy;
		protected bool canMove = true;

		// TODO: make a list of limb classes that enable/disable
		// 		 certain functions depending on damage levels.

		public Soldier(List<List<char>> map, EntityManager entityManager, Point position, Point direction, string faction) : base(map, entityManager, position, '☺') {
			this.map = map;
			this.position = position;
			this.direction = direction;
			this.faction = faction;

			lifeState = LifeStates.ALIVE;
			moveState = MoveStates.IDLE;
			combatState = CombatStates.IDLE;
		}

        public override void update()
        {
            base.update();

			// evaluateState() every other tick, executeState() inbetween evaluting
			if (ticks % 2 == 0) {
				executeState();
			// if the soldier has no other items queued 
			} else if (ticks % 1 == 0 && stateQueue.Count == 1 && (stateQueue[0].Equals(MoveStates.MOVING) || stateQueue[0].Equals(CombatStates.SHOOTING))) {
				evaluateState();
			}
        }

		public void evaluateState() {
			if (lifeState == LifeStates.DEAD) {
				canMove = false;
				return;
			} else if (lifeState == LifeStates.ALIVE) {
				canMove = true;
			} else if (lifeState == LifeStates.DYING) {
				return;
			} else if (lifeState == LifeStates.INJURED) {
				return;
			} else if (lifeState == LifeStates.HELPED) {
				canMove = false;
				return;
			}

			// check if there's a soldier near us of a different faction
			Soldier closestEnemy = getClosestSoldier(friendly: false);
			if (closestEnemy == this)
				combatState = CombatStates.IDLE;
			else
				combatState = CombatStates.SHOOTING;
				targetEnemy = closestEnemy;
			
			Soldier closestFriendly = getClosestSoldier(friendly: true);
			switch (closestFriendly.lifeState) {
				case LifeStates.DYING:
					targetLocation = closestFriendly.Position;
					stateQueue.Add(MoveStates.MOVING);
					stateQueue.Add(CombatStates.HELPING);
					statesQueued = true;
					break;
			}
		}

		public void executeState() {
			switch (lifeState) {
				case LifeStates.DEAD:
					canMove = false;
					return;
				
				case LifeStates.ALIVE:
					canMove = true;
					return;
				
				case LifeStates.DYING:
					return;
				
				case LifeStates.INJURED:
					return;
				
				case LifeStates.HELPED:
					return;
			}
			switch (moveState) {
				case MoveStates.IDLE:
					return;
				
				case MoveStates.MOVING:
					return;
			}
			switch (combatState) {
				case CombatStates.SHOOTING:
					shootAtTarget();
					return;
				
				case CombatStates.HELPING:
					return;
				
				case CombatStates.IDLE:
					return;
			}
		}

		public Soldier getClosestSoldier(bool friendly) {
			Soldier? closestSoldier = this;
			double prevDistance = double.MaxValue;

			foreach (Soldier soldier in entityManager.Soldiers.ToList()) {
				if (friendly) {
					if (soldier.Faction == faction && soldier != this) { 
						double distance = Math.Sqrt(soldier.position.X - position.X + soldier.position.Y - position.Y);

						if (distance < prevDistance)
							closestSoldier = soldier;

						prevDistance = distance;
					}
				} else {
					if (soldier.Faction != faction && soldier != this) { 
						double distance = Math.Sqrt(soldier.position.X - position.X + soldier.position.Y - position.Y);

						if (distance < prevDistance)
							closestSoldier = soldier;

						prevDistance = distance;
					}
				}
			}
			return closestSoldier;
		}

		public void receiveHealth(int healthGiven) {
			health += healthGiven;	
		}

		public void shootAtTarget() {
			// Bullet bullet = new Bullet(map, entityManager, Position, getTargetDirection(targetEnemy.position), 25, 0.5);
			if (targetEnemy != null && targetEnemy.lifeState != LifeStates.DEAD) {
				Point targetDirection = getTargetDirection(targetEnemy.Position);
				Bullet bullet = new Bullet(map, entityManager, Position, targetDirection, 25, 0.5);
				entityManager.addEntity(bullet);
			} else {
				combatState = CombatStates.IDLE;
			}
		}

		public Point getTargetDirection(Point targetPosition) {
			double targetAngle = Math.Atan2(targetPosition.Y - position.Y, targetPosition.X - position.X);
            return new Point((int) Math.Cos(targetAngle), (int) Math.Sin(targetAngle));
        }


		// Getters and setters
		public string Faction {
			get => faction;
			set => faction = value;
		}

		public Soldier TargetEnemy {
			get => targetEnemy;
			set => targetEnemy = value;
		}
    }
}