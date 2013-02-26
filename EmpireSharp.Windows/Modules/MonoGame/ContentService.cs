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
using System.IO;
using System.Linq;
using EmpireSharp.Windows.Framework.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ninject;
using Papyrus;

namespace EmpireSharp.Windows.Modules.MonoGame
{

	class ContentService : IContentService
	{

		public string ContentDirectory { get; set; }

		public RecordDatabase Database { get; private set; }

		private Dictionary<string, Texture2D> _texCache = new Dictionary<string, Texture2D>();

		[Inject]
		IShell Shell { get; set; }

		[Inject]
		ILog Log { get; set; }

		private Texture2D _errorTex;

		[Inject]
		public ContentService(IShell shell)
		{

			_errorTex = new Texture2D(((Shell)shell).GraphicsDevice, 3, 3);

			_errorTex.SetData(new Color[] {
				Color.Magenta, Color.Black, Color.Magenta,
				Color.Black, Color.Magenta, Color.Black,
				Color.Magenta, Color.Black, Color.Magenta
			});

		}

		public void LoadDatabase()
		{

			try {

				RecordDatabase.Initialize(new string[] {"EmpireSharp.Data"});

			} catch (Exception e) {

				Log.LogException(e);

			}

			var plugins = Papyrus.PluginUtilities.PluginsInDirectory(ContentDirectory);

			Database = new RecordDatabase(plugins.Select(p => p.SourceFile).ToList());

		}

		public string FullAssetPath(string assetPath)
		{

			return Path.GetFullPath(Path.Combine(ContentDirectory, "Assets", assetPath));

		}

		public Texture2D GetTexture(string assetPath)
		{

			var path = FullAssetPath(assetPath);

			Texture2D ret;

			if (_texCache.TryGetValue(path, out ret))
				return ret;

			if (File.Exists(path)) {

				try {

					using (var f = File.OpenRead(assetPath))
						ret = Texture2D.FromStream(((Shell) Shell).GraphicsDevice, f);

				} catch (Exception e) {
					
					Log.LogException(e);

				}

			}

			if (ret == null) {
				
				Log.LogWarning("Asset Not Found [{0}]", path);

				return _errorTex;

			}

			return ret;

		}


	}

}
