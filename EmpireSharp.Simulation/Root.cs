/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/
using System;
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

		private readonly IKernel _kernel;

		public Root()
		{

			_kernel = new StandardKernel();
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

			Terrain = _kernel.Get<Terrain>();
			Terrain.Init(128);

			EntityContainer = _kernel.Get<EntityContainer>();

			IsInitialised = true;

		}

		/// <summary>
		/// Steps the simulation forward one tick.
		/// </summary>
		private void Tick()
		{
			
			EntityContainer.Tick();

		}

	}

}
