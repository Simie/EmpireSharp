using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmpireSharp.Simulation.Entities;
using Ninject;

namespace EmpireSharp.Simulation
{

	/// <summary>
	/// Contains a list of all the entities in the simulation.
	/// </summary>
	public class EntityContainer
	{

		private List<Entities.BaseEntity> _internalList;

		[Inject]
		public IKernel Kernel { get; private set; }

		public EntityContainer()
		{
			_internalList = new List<BaseEntity>(1024);
		}

		public T CreateEntity<T>(FixedVector2? initialPosition = null) where T : Entities.BaseEntity, new()
		{

			var entity = new T();

			Kernel.Inject(entity);

			if(initialPosition.HasValue)
				entity.Transform.Position = initialPosition.Value;

			_internalList.Add(entity);

			return entity;

		}

		internal void Tick()
		{

			// Reverse loop in case entities are added during the tick.
			for (int i = _internalList.Count-1; i >= 0; --i) {
				_internalList[i].Tick();
			}

		}

	}

}
