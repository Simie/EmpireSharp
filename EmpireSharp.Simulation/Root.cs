/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/
using System;
using System.Collections.Generic;
using EmpireSharp.Simulation.Commands;
using EmpireSharp.Simulation.Entities;
using Ninject;

namespace EmpireSharp.Simulation
{

	/// <summary>
	/// Root object
	/// </summary>
	public class Root
	{

		/// <summary>
		/// True after initialisation has been completed.
		/// </summary>
		public bool IsInitialised { get; private set; }
		
		/// <summary>
		/// Currently active terrain data.
		/// </summary>
		public Terrain Terrain { get; private set; }

		/// <summary>
		/// Holds all entities in the simulation.
		/// </summary>
		public EntityContainer EntityContainer { get; private set; }

		private Time _time;

		private readonly IKernel _kernel;

		private Queue<Commands.Command> _commandQueue = new Queue<Command>(10);

		public Root()
		{

			var k = new StandardKernel();
			k.Settings.InjectNonPublic = true;
			k.Settings.InjectParentPrivateProperties = true;

			_kernel = k;
			_kernel.Bind<Time>().To<Time>().InSingletonScope();
			_kernel.Bind<Terrain>().To<Terrain>().InSingletonScope();
			_kernel.Bind<EntityContainer>().To<EntityContainer>().InSingletonScope();

		}

		/// <summary>
		/// Initialise the simulation.
		/// </summary>
		/// <remarks>Throws <c>InvalidOperationException</c> if the simulation is already initialised.</remarks>
		public void Init()
		{

			if(IsInitialised)
				throw new InvalidOperationException("Simulation is already initialised.");

			_time = _kernel.Get<Time>();

			Terrain = _kernel.Get<Terrain>();
			Terrain.Init(128);

			EntityContainer = _kernel.Get<EntityContainer>();

			EntityContainer.CreateEntity<Unit>(new FixedVector2( 10, 15));
			EntityContainer.CreateEntity<Unit>(new FixedVector2(-10, 10));

			IsInitialised = true;

		}

		/// <summary>
		/// Steps the simulation forward one tick.
		/// </summary>
		public void Tick()
		{

			_time.Tick();

			while (_commandQueue.Count > 0) {

				var command = _commandQueue.Dequeue();
				_kernel.Inject(command);

				command.Apply();

			}

			EntityContainer.Tick();

		}

		/// <summary>
		/// Queue a command for execution on the next tick.
		/// </summary>
		/// <param name="command"></param>
		public void QueueCommand(Commands.Command command)
		{

			_commandQueue.Enqueue(command);

		}

	}

}
