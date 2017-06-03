using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Blurlib.Physics
{
    public class ForceList : IEnumerable<Vector2>
    {
        private Dictionary<string, Vector2> _forceList;

        public Vector2 Acceleration { get; private set; }

        private float _mass;
        public float Mass
        {
            get { return _mass; }
            set
            {
                _mass = value;
                MassInverse = 1 / value;
            }
        }

        public float MassInverse { get; private set; }

        public ForceList(float mass = 0.0f)
        {
            _forceList = new Dictionary<string, Vector2>();
            Mass = mass;
            Acceleration = Vector2.Zero;
        }

        public void AddForce(string id, Vector2 force)
        {
            string _id = id.ToUpper();
            if (!_forceList.ContainsKey(_id))
            {
                _forceList.Add(_id, force);
                ComputeForces();
            }
        }

        public void RemoveForce(string id)
        {
            string _id = id.ToUpper();
            if (_forceList.ContainsKey(_id))
            {
                _forceList.Remove(_id);
                ComputeForces();
            }
        }

        public Vector2 GetForce(string id)
        {
            string _id = id.ToUpper();

            if (_forceList.TryGetValue(_id, out Vector2 to_return))
            {
                return to_return;
            }
            else
            {
                return Vector2.Zero;
            }
        }

        public void UpdateForce(string id, Vector2 force)
        {
            string _id = id.ToUpper();

            if (_forceList.ContainsKey(_id))
            {
                _forceList[_id] = force;
                ComputeForces();
            }
        }

        public void Reset()
        {
            Acceleration = Vector2.Zero;
            _forceList.Clear();
        }

        private void ComputeForces()
        {
            Acceleration = Vector2.Zero;
            foreach (Vector2 force in _forceList.Values)
            {
                Acceleration += force;
            }

            if (Mass > 0)
                Acceleration *= MassInverse;
        }

        public IEnumerator<Vector2> GetEnumerator()
        {
            return _forceList.Values.GetEnumerator() as IEnumerator<Vector2>;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
