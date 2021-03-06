﻿using Kusto.Language.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace DeltaKustoLib.CommandModel
{
    /// <summary>
    /// Models <see cref="https://docs.microsoft.com/en-us/azure/data-explorer/kusto/management/drop-table-command"/>
    /// </summary>
    public class DropTableCommand : CommandBase
    {
        public EntityName TableName { get; }

        public override string CommandFriendlyName => ".drop table";

        internal DropTableCommand(EntityName tableName)
        {
            TableName = tableName;
        }

        internal static CommandBase FromCode(SyntaxElement rootElement)
        {
            var nameReference = rootElement.GetUniqueDescendant<NameReference>(
                "TableName",
                n => n.NameInParent == "TableName");

            return new DropTableCommand(new EntityName(nameReference.Name.SimpleName));
        }

        public override bool Equals(CommandBase? other)
        {
            var otherTable = other as DropTableCommand;
            var areEqualed = otherTable != null
                && otherTable.TableName.Equals(TableName);

            return areEqualed;
        }

        public override string ToScript()
        {
            return $".drop table {TableName}";
        }
    }
}