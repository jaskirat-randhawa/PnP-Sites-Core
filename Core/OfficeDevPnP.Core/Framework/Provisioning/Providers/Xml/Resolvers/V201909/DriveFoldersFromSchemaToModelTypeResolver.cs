﻿using OfficeDevPnP.Core.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.Resolvers
{
    /// <summary>
    /// Resolves a collection of DriveFolder items from Schema to Domain Model
    /// </summary>
    internal class DriveFoldersFromSchemaToModelTypeResolver : ITypeResolver
    {
        public string Name => this.GetType().Name;

        public bool CustomCollectionResolver => false;

        public DriveFoldersFromSchemaToModelTypeResolver()
        {
        }

        public object Resolve(object source, Dictionary<String, IResolver> resolvers = null, Boolean recursive = false)
        {
            var result = new List<Model.Drive.DriveFolder>();

            var driveItems = source.GetPublicInstancePropertyValue("Items");

            var driveFolderTypeName = $"{PnPSerializationScope.Current?.BaseSchemaNamespace}.DriveFolder, {PnPSerializationScope.Current?.BaseSchemaAssemblyName}";
            var driveFolderType = Type.GetType(driveFolderTypeName, true);

            if (null != driveItems)
            {
                foreach (var d in (IEnumerable)driveItems)
                {
                    if (driveFolderType.IsInstanceOfType(d))
                    {
                        var targetItem = new Model.Drive.DriveFolder();
                        PnPObjectsMapper.MapProperties(d, targetItem, resolvers, recursive);
                        result.Add(targetItem);
                    }
                }
            }

            return (result);
        }
    }
}
