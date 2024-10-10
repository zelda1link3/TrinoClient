﻿using Newtonsoft.Json;
using System;

namespace TrinoClient.Model.Sql.Planner
{
    /// <summary>
    /// From com.facebook.presto.sql.planner.Partitioning.java (internal class ArgumentBinding)
    /// </summary> 
    public sealed class ArgumentBinding
    {
        #region Public Properties

        public Symbol Column { get; }

        public object Constant { get; }

        #endregion

        #region Constructors

        [JsonConstructor]
        public ArgumentBinding(Symbol column, object constant)
        {
            if ((column != null) == (constant != null))
            {
                throw new ArgumentException("Either column or constant must be set, not both, and both cannot be null.");
            }

            Column = column;
            Constant = constant;
        }

        #endregion

        #region Public Methods

        public bool IsConstant()
        {
            return Constant != null;
        }

        public bool IsVariable()
        {
            return Column != null;
        }

        public static ArgumentBinding ColumnBinding(Symbol column)
        {
            return new ArgumentBinding(column, null);
        }

        public static ArgumentBinding ConstantBinding(object constant)
        {
            return new ArgumentBinding(null, constant);
        }

        public ArgumentBinding Translate(Func<Symbol, Symbol> translator)
        {
            if (IsConstant())
            {
                return this;
            }
            else
            {
                return ColumnBinding(translator.Invoke(Column));
            }
        }

        #endregion
    }
}
