﻿/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using EmpireSharp.Windows.Framework.Services;
using EmpireSharp.Windows.Modules.MonoGame;
using Ninject;

namespace EmpireSharp.Windows
{

	public class Main
	{

		public readonly Ninject.IKernel IoC;

		public Main()
		{

			IoC = new StandardKernel();
			IoC.Settings.InjectParentPrivateProperties = true;
			IoC.Settings.InjectNonPublic = true;

			IoC.Bind<ILog>().To<LogService>().InSingletonScope();
			IoC.Bind<IContentService>().To<ContentService>().InSingletonScope();
			IoC.Bind<IShell>().To<Shell>().InSingletonScope();

		}

		public void Run()
		{
			
			IoC.Get<IShell>().Run();

		}

	}

}