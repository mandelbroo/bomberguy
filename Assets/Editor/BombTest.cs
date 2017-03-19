using NUnit.Framework;
using UnityEngine;

namespace Assets.Editor
{
	public class BombTest {
		[Test]
		public void OppositeLeft() {
			var direction = Vector3.left;
			var result = Bomb.Opposite(direction);
			Assert.IsTrue(result == Vector3.right);
		}
		[Test]
		public void OppositeRight() {
			var direction = Vector3.right;
			var result = Bomb.Opposite(direction);
			Assert.IsTrue(result == Vector3.left);
		}
		[Test]
		public void OppositeBack() {
			var direction = Vector3.back;
			var result = Bomb.Opposite(direction);
			Assert.IsTrue(result == Vector3.forward);
		}
		[Test]
		public void OppositeForward() {
			var direction = Vector3.forward;
			var result = Bomb.Opposite(direction);
			Assert.IsTrue(result == Vector3.back);
		}
	}
}
