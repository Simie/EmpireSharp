/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using EmpireSharp.Game.Framework.Services;
using EmpireSharp.Game.Modules.MonoGame;
using Ninject;

namespace EmpireSharp.Game
{

	public class Main
	{

		public readonly Ninject.IKernel IoC;

		public Main()
		{

			IoC = new StandardKernel();
			IoC.Settings.InjectParentPrivateProperties = true;
			IoC.Settings.InjectNonPublic = true;

			IoC.Bind<IShell>().To<Shell>().InSingletonScope();

		}

		public void Run()
		{
			
			IoC.Get<IShell>().Run();

		}

	}

}
