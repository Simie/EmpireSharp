/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using EmpireSharp.Game.Framework.Services;

namespace EmpireSharp.Game.Modules.MonoGame
{
	class Module : Ninject.Modules.NinjectModule
	{
		public override void Load()
		{

			Bind<IShell>().To<Shell>().InSingletonScope();
			Bind<ILog>().To<LogService>().InSingletonScope();
			Bind<IContentService>().To<ContentService>().InSingletonScope();
			Bind<IInputService>().To<InputManager>().InSingletonScope();

		}
	}
}
